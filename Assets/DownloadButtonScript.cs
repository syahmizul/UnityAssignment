using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System;

public class ContentDisposition
{
    private static readonly Regex regex = new Regex(
        "^([^;]+);(?:\\s*([^=]+)=((?<q>\"?)[^\"]*\\k<q>);?)*$",
        RegexOptions.Compiled
    );

    private readonly string fileName;
    private readonly StringDictionary parameters;
    private readonly string type;

    public ContentDisposition(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            throw new ArgumentNullException("s");
        }
        Match match = regex.Match(s);
        if (!match.Success)
        {
            throw new FormatException("input is not a valid content-disposition string.");
        }
        var typeGroup = match.Groups[1];
        var nameGroup = match.Groups[2];
        var valueGroup = match.Groups[3];

        int groupCount = match.Groups.Count;
        int paramCount = nameGroup.Captures.Count;

        this.type = typeGroup.Value;
        this.parameters = new StringDictionary();

        for (int i = 0; i < paramCount; i++)
        {
            string name = nameGroup.Captures[i].Value;
            string value = valueGroup.Captures[i].Value;

            if (name.Equals("filename", StringComparison.InvariantCultureIgnoreCase))
            {
                this.fileName = value;
            }
            else
            {
                this.parameters.Add(name, value);
            }
        }
    }
    public string FileName
    {
        get
        {
            return this.fileName;
        }
    }
    public StringDictionary Parameters
    {
        get
        {
            return this.parameters;
        }
    }
    public string Type
    {
        get
        {
            return this.type;
        }
    }
}

public class DownloadButtonScript : Button
{
    Notification notifyScript;
    TMP_InputField  URLTextComponent;
    TMP_InputField  DescriptionTextComponent;
    TMP_Dropdown    ContentTypeDropdown;
    // Start is called before the first frame update

    public static string GetDownloadFolder()
    {
        string[] temp = (Application.persistentDataPath.Replace("Android", "")).Split(new string[] { "//" }, System.StringSplitOptions.None);

        return (temp[0] + "/Download");
    }

    protected override void Start()
    {
        notifyScript = GameObject.Find("ScreenOverlay").GetComponentInChildren<Notification>();
        URLTextComponent = GameObject.Find("URLInputBox").GetComponent<TMP_InputField>();
        DescriptionTextComponent = GameObject.Find("DescriptionBox").GetComponent<TMP_InputField>();
        ContentTypeDropdown = GameObject.Find("ContentTypeDropdown").GetComponent<TMP_Dropdown>();
    }

    IEnumerator Download()
    {
        string FileName = System.IO.Path.GetFileName(URLTextComponent.text);


        /*using (var HeaderRequest = new UnityWebRequest(URLTextComponent.text, UnityWebRequest.kHttpVerbHEAD))*/
        using (var uwr = new UnityWebRequest(URLTextComponent.text, UnityWebRequest.kHttpVerbGET))
        {
            DownloadInfo infoInstance = new DownloadInfo();

            infoInstance.FileDescription = DescriptionTextComponent.text;
            infoInstance.FileType = ContentTypeDropdown.options[ContentTypeDropdown.value].text;

            if (infoInstance.FileType.Equals("Image"))
            {
                FileName += ".png";
            }

            infoInstance.FileName = FileName;
            Debug.Log(GetDownloadFolder());
            string DownloadPath = Path.Combine(GetDownloadFolder(), FileName);

            infoInstance.FilePath = DownloadPath;

            uwr.downloadHandler = new DownloadHandlerFile(DownloadPath, false);
            uwr.SendWebRequest();
            /*HeaderRequest.SendWebRequest();*/
            ViewListScript instance = GameObject.Find("ViewListView").GetComponent<ViewListScript>();
            

            /*while (!HeaderRequest.isDone)
            {
                yield return null;
            }

            if (HeaderRequest.GetResponseHeaders() != null && HeaderRequest.GetResponseHeaders().Count > 0)
            {
                string ContentDispositionString = HeaderRequest.GetResponseHeader("Content-Disposition");
                if (ContentDispositionString != null)
                {
                    FileName = new ContentDisposition(ContentDispositionString).FileName;
                }
                infoInstance.FileSize = HeaderRequest.GetResponseHeader("Content-Length");
            }
*/


            
            instance.AddDownloadInfo(infoInstance);

            if (uwr.result == UnityWebRequest.Result.InProgress)
            {
                infoInstance.Status = "In progress";

                notifyScript.AnimationProgress = 0.0f;
                notifyScript.NotificationTitleString = "Download started";
                notifyScript.NotificationDescriptionString = $"Your file {FileName} download has started.";

            }

            while (!uwr.isDone)
            {
                /*Debug.Log(uwr.downloadProgress);*/
                infoInstance.Progress = uwr.downloadProgress;
                yield return null;
            }

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.DataProcessingError || uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                infoInstance.Progress = 0.0f;
                infoInstance.Status = "Failed";
                notifyScript.AnimationProgress = 0.0f;
                notifyScript.NotificationTitleString = "Download failed";
                notifyScript.NotificationDescriptionString = $"There was an error with your download.";
                yield return null;
            }

            if (uwr.result == UnityWebRequest.Result.Success)
            {
                infoInstance.Progress = 1.0f;
                infoInstance.Status = "Finished";
                notifyScript.AnimationProgress = 0.0f;
                notifyScript.NotificationTitleString = "Download success";
                notifyScript.NotificationDescriptionString = $"Your file {FileName} download has finished. It is saved at : {DownloadPath}";
                yield return null;
            }


        }
        yield return null;
        
    }

    
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData); // Call Original
        StartCoroutine(Download());
        
    }
    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}
