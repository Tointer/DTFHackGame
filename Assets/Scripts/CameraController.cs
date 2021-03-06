﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private new Camera camera;
    public float zoomOutSpeed = 0.025f;
    public float forwardZoomOutSpeed = 0.25f;
    public Transform screenBoundaries;
    
    private Transform playerClock;
    private Transform playerClockHand;
    private float startingSize;
    

    void Start()
    {
        playerClock = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerClock>().transform;
        playerClockHand = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ClockHand>().transform;
        camera = Camera.main;
        startingSize = camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            ZoomOut(forwardZoomOutSpeed);
        }
        else
        {
            ZoomOut(zoomOutSpeed);
        }
    }

    private void ZoomOut(float value)
    {
        camera.orthographicSize += value * Time.deltaTime;
        playerClock.localScale = new Vector3(camera.orthographicSize/startingSize, camera.orthographicSize/startingSize, 0);
        playerClockHand.localScale = new Vector3(camera.orthographicSize/startingSize, camera.orthographicSize/startingSize, 0);
        
        screenBoundaries.localScale = new Vector3(camera.orthographicSize/startingSize, camera.orthographicSize/startingSize, 0);
    }
}
