using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideUI : MonoBehaviour
{
    public GameObject ui;
    private bool startPause = true;
   
    void Start()
    {
        ui.SetActive(false);
    }
    
    void Update()
    {
        if (!startPause) return;
        if (!Input.anyKey) return;
        ui.SetActive(true);
        startPause = false;
    }
}
