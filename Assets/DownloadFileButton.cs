using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;




public class DownloadFileButton : Button
{
    Camera cameraObj;
    Canvas DownloadView;
    CameraVars GlobalCameraVars;

    bool isTransitioning = false;
    protected override void Start()
    {
        cameraObj = (Camera)(GameObject.Find("Main Camera").GetComponent("Camera"));
        DownloadView = (Canvas)(GameObject.Find("DownloadView").GetComponent("Canvas"));
        GlobalCameraVars = cameraObj.gameObject.GetComponent<CameraVars>();

        ButtonClickedEvent ClickedEvent = new ButtonClickedEvent();

        ClickedEvent.AddListener(() =>
        {
            if (GlobalCameraVars.Transitioner == null)
            {
                this.isTransitioning = true;
                GlobalCameraVars.Transitioner = gameObject;
            }
        }
        );
        onClick = ClickedEvent;
    }

    public void Update()
    {
        if (isTransitioning && GlobalCameraVars.Transitioner.Equals(gameObject))
        {
            Vector3 CameraForward = DownloadView.transform.position - cameraObj.transform.position;


            Quaternion AngleToLookAt = Quaternion.LookRotation(CameraForward);
            
            if (Quaternion.Angle(cameraObj.transform.rotation, AngleToLookAt) > 0)
            {
                Quaternion LerpedAngle = Quaternion.Slerp(cameraObj.transform.rotation, AngleToLookAt, 5.0f * Time.smoothDeltaTime);
                cameraObj.transform.rotation = LerpedAngle;
            }
            else
            {
                isTransitioning = false;
                GlobalCameraVars.Transitioner = null;
            }

        }
    }
}