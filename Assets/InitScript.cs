using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Specifically used to initialize environment,application settings/parameters.
public class InitScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Application.runInBackground = true; // Download in backgrounds.
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}

