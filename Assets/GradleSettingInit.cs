using System.Collections;
using System.Collections.Generic;
using System.IO;
/*using UnityEditor.Android;*/
using UnityEngine;

/*public class GradleSettingInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/
/*public class GradleSettingInit : IPostGenerateGradleAndroidProject
{
    public int callbackOrder
    {
        get
        {
            return 0;
        }
    }

    void IPostGenerateGradleAndroidProject.OnPostGenerateGradleAndroidProject(string path)
    {
        Debug.Log("Bulid path : " + path);
        string gradlePropertiesFile = path + "/gradle.properties";
        if (File.Exists(gradlePropertiesFile))
        {
            File.Delete(gradlePropertiesFile);
        }
        StreamWriter writer = File.CreateText(gradlePropertiesFile);
        writer.WriteLine("org.gradle.jvmargs=-Xmx4096M");
        writer.WriteLine("android.useAndroidX=true");
        writer.WriteLine("android.enableJetifier=true");
        writer.Flush();
        writer.Close();

    }
}*/