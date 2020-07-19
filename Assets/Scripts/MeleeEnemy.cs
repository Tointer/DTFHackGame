using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    private GameObject playerObject;
    private bool active = true;

    public float acceleration = 1f;
    public float maxSpeed = 2f;
    
    public Rigidbody2D rb;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(GoToPlayer());
    }

    IEnumerator GoToPlayer()
    {
        while (active)
        {
            rb.AddForce((playerObject.transform.position - transform.position).normalized * (acceleration * Time.fixedDeltaTime*Time.timeScale));
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
        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        rb.angularVelocity = 120f;
    }
}
