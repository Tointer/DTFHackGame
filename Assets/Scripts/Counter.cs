using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Counter : MonoBehaviour
{
    private List<ICount> tickSubscribers;
    public float speed = 1f;
    public float fastForwardSpeed = 2f;
    private float currentSpeed;
    private float totalNumberOfTicks = 60f;
    private bool active = true;

    private void Start()
    {
        currentSpeed = speed;
        tickSubscribers = Find<ICount>();
        StartCoroutine(Count());
    }

    private void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            currentSpeed = fastForwardSpeed;
            return;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            currentSpeed = speed;
        }
    }

    private IEnumerator Count()
    {
        yield return null;
        float counter = 0;
        float tickCounter = 0;
        while (tickCounter <= totalNumberOfTicks)
        {
            if(!active) break;
            counter += Time.deltaTime * currentSpeed;
            if (counter >= 1)
            {
                NotifyAll();
                counter = 0;
                tickCounter++;
                yield return null;
            }
            else
            {
                yield return null;
            }
        }

        StopCounter();
    }

    private void StopCounter()
    {
        if(active == false) return;

        active = false;
        foreach (var obj in tickSubscribers)
        {
            obj.EndOfCount();
        }
    }
    

    private void NotifyAll()
    {
        foreach (var obj in tickSubscribers)
        {
            obj.Tick();
        }
    }

    private static List<T> Find<T>(  )
    {
        var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        return rootGameObjects.SelectMany(rootGameObject => rootGameObject.GetComponentsInChildren<T>()).ToList();
    }
}
