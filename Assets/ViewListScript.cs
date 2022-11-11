using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Stores the metadata of the file itself.
public class DownloadInfo
{
    public string FileName;
    public string FileDescription;
    public string FilePath;
    public string FileSize;
    public string Status;
    public string FileType;
    public float  Progress = 0.0f;
}

public class DownloadInfoUI
{
    public int Index = 0;
    public DownloadInfo info;
}
public class ViewListScript : MonoBehaviour
{
    private List<DownloadInfoUI> downloadInfos;

    public GameObject preFab;
    private GameObject ScrollViewGameObj;
    private ScrollRect ScrollViewScrollRect;

    public void AddDownloadInfo(DownloadInfo info)
    {
        DownloadInfoUI infoui = new DownloadInfoUI();
        infoui.Index = ScrollViewScrollRect.content.transform.childCount;

        infoui.info = info;


        GameObject DownloadCard = UnityEngine.Object.Instantiate(preFab);
        RectTransform DownloadCardTransform = DownloadCard.GetComponent<RectTransform>();
        DownloadCardTransform.SetParent(ScrollViewScrollRect.content);

        DownloadCardTransform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        DownloadCardTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        DownloadCardTransform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

        DownloadCard.GetComponent<DownloadCardScript>().info = info;

        downloadInfos.Add(infoui);
    }

    // Start is called before the first frame update
    void Start()
    {

        downloadInfos = new List<DownloadInfoUI>();

        ScrollViewGameObj = GameObject.Find("Scroll View");
        ScrollViewScrollRect = ScrollViewGameObj.GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    /*void Update()
    {


    }*/
}
