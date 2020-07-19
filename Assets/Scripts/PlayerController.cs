using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform direction;
    public float speed = 1f;
    

    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            rb.velocity = (direction.position - transform.position).normalized * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnDisable()
    {
        rb.simulated = false;
    }
}
