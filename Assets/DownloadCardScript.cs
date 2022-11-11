using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DownloadCardScript : MonoBehaviour
{
    public DownloadInfo info;

    private TextMeshProUGUI TitleObject;
    private TextMeshProUGUI TypeObject;
    private TextMeshProUGUI DescriptionObject;
    private Slider SliderComponent;
    private Button ButtonComponent;
    // Start is called before the first frame update
    void Start()
    {
        TitleObject = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        TypeObject = gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        DescriptionObject = gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();

        ButtonComponent = gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Button>();
        SliderComponent = gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<Slider>();

        var ClickEvent = new Button.ButtonClickedEvent();


        ClickEvent.AddListener(() =>
        {

            //Classes
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaClass fileProviderClass = new AndroidJavaClass("androidx.core.content.FileProvider");
            AndroidJavaClass URIClass = new AndroidJavaClass("android.net.Uri");


            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", "android.intent.action.VIEW");

            AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", info.FilePath);


            object[] providerParams = new object[3];
            providerParams[0] = currentActivity;
            providerParams[1] = $"{Application.identifier}.provider";
            providerParams[2] = fileObject;


            AndroidJavaObject uriObject = fileProviderClass.CallStatic<AndroidJavaObject>("getUriForFile", providerParams);


            intentObject.Call<AndroidJavaObject>("putExtra", "android.intent.extra.FROM_STORAGE", null);
            intentObject.Call<AndroidJavaObject>("setType", "image/*");
            intentObject.Call<AndroidJavaObject>("setData", uriObject);
            intentObject.Call<AndroidJavaObject>("addFlags", 0x00000001); // FLAG_GRANT_READ_URI_PERMISSION
            intentObject.Call<AndroidJavaObject>("addFlags", 0x00000002); // FLAG_GRANT_WRITE_URI_PERMISSION
            /*intentObject.Call<AndroidJavaObject>("addCategory", "android.intent.category.APP_GALLERY");*/
            currentActivity.Call("startActivity", intentObject);
        });

        ButtonComponent.onClick = ClickEvent;
    }

    // Update is called once per frame
    void Update()
    {
        TitleObject.text = this.info.FileName;
        TypeObject.text = this.info.FileType;
        DescriptionObject.text = this.info.FileDescription;
        SliderComponent.value = this.info.Progress;
    }

    void ViewActionButton()
    {

    }
}
