using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ClickToUseDll : MonoBehaviour
{

    [DllImport("dotNETClassLibraryUsingUWPAPIs")]
    extern static string Notify(string toastTitle, string toastContent);

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
    }
}
