using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;




public class Notification : MonoBehaviour
{
    private GameObject ScreenOverlay;
    private GameObject NotificationTitle;
    private GameObject NotificationDescription;
    public float AnimationProgress = 0.00f;
    public string NotificationTitleString = "Placeholder Text #1";
    public string NotificationDescriptionString = "Placeholder Text Desc #2";
    // Start is called before the first frame update
    void Start()
    {
        NotificationTitleString = "Welcome to this app!";
        NotificationDescriptionString = "Download and view your files with ease.";

        NotificationTitle   = GameObject.Find("NotificationTitle");
        NotificationDescription = GameObject.Find("NotificationDesc");
        ScreenOverlay       = GameObject.Find("ScreenOverlay");
    }
    
    // Update is called once per frame
    void Update()
    {
        if (AnimationProgress < (Mathf.PI * 100.0f))
        {
            TextMeshProUGUI TitleText = NotificationTitle.GetComponent<TextMeshProUGUI>();// Slow
            TitleText.text = NotificationTitleString;// Slow

            TextMeshProUGUI DescText = NotificationDescription.GetComponent<TextMeshProUGUI>();// Slow
            DescText.text = NotificationDescriptionString;// Slow

            /*  Debug.Log(AnimationProgress);*/
            RectTransform OverlayTransform = ScreenOverlay.GetComponent<RectTransform>();// Slow

            RectTransform CurrentGameObjectRectTransform = gameObject.GetComponent<RectTransform>();// Slow

            Rect transformRect = CurrentGameObjectRectTransform.rect;
            transformRect.width = OverlayTransform.rect.width * 1.0f;

            CurrentGameObjectRectTransform.sizeDelta = new Vector2(transformRect.width, transformRect.height);

            float CalculatedNeededStartPosition = (OverlayTransform.rect.height / 2) + CurrentGameObjectRectTransform.rect.height;

            /*Debug.Log(CalculatedNeededStartPosition);*/

            float Height = (Mathf.Sin(AnimationProgress * 0.01f) / -0.01f) + CalculatedNeededStartPosition;// Kind of slow
            /*Debug.Log(Height);*/


            Vector3 NewTransform = CurrentGameObjectRectTransform.localPosition;
            NewTransform.y = Height;
            CurrentGameObjectRectTransform.localPosition = NewTransform;
            
            AnimationProgress += 1.0f * Time.deltaTime * 100.0f;
        }
    }

}
