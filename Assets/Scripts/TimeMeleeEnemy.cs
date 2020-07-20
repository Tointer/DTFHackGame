using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMeleeEnemy : MonoBehaviour
{
    private GameObject playerObject;
    private bool active = true;

    public float acceleration = 1f;
    public float maxSpeed = 2f;
    public float multiplier = 4f;

    private float currentAcceleration;
    private float currentMaxSpeed;
    
    public Rigidbody2D rb;

    void Start()
    {
        currentAcceleration = acceleration;
        currentMaxSpeed = maxSpeed;
        
        playerObject = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(GoToPlayer());
    }

    private void Update()
    {
        if (GameManager.IsFastForwarding)
        {
            currentAcceleration = multiplier * acceleration;
           // currentMaxSpeed = multiplier * maxSpeed;
        }
        else
        {
            currentAcceleration = acceleration;
           // currentMaxSpeed = maxSpeed;
        }
        
    }

    IEnumerator GoToPlayer()
    {
        while (active)
        {
            rb.velocity = (playerObject.transform.position - transform.position).normalized * (currentAcceleration * Time.fixedDeltaTime*Time.timeScale);
            yield return null;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
            GameManager.Instance.PlayerLoose();
    }
    
    void FixedUpdate()
    {
        // if (GameManager.IsFastForwarding)
        // {
        //     rb.velocity *= multiplier;
        // }
        // else if(rb.velocity.magnitude > currentMaxSpeed)
        // {
        //     rb.velocity = rb.velocity.normalized * currentMaxSpeed;
        // }
        rb.angularVelocity = 120f;
    }
}
