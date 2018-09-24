using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ClickToUseDll : MonoBehaviour
{

    [DllImport("dotNETClassLibraryUsingUWPAPIs")]
    extern static string Notify(string toastTitle, string toastContent);

    [DllImport("dotNETClassLibraryUsingUWPAPIs")]
    extern static string NotifyWithDelay(string toastTitle, string toastContent, int delayinMilliseconds);
    

    [DllImport("dotNETClassLibraryUsingUWPAPIs")]
    extern static string UpdatePrimaryTile(string text, int durationSeconds);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            string output = Notify("Toast title",
                 "Unity toast sent at " + System.DateTime.Now.ToLongTimeString());
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            string output = NotifyWithDelay("Schedule Toast title",
                "Unity toast sent at " + System.DateTime.Now.ToLongTimeString(), 5000);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            string output = UpdatePrimaryTile("Updated at " 
                + System.DateTime.Now.ToLongTimeString(), 20);
        }
    }
}
