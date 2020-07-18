using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEnemyBullet : MonoBehaviour
{
    public float lifeDuration = 15f;
    private Vector3 startVelocity;
    public float multiplier = 6f;

    [SerializeField] private Rigidbody2D rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startVelocity = rb.velocity;
        if (GameManager.IsFastForwarding)
        {
            rb.velocity *= multiplier;
        }
        
        GameManager.Instance.StartFastForward += OnStartFF;
        GameManager.Instance.StopFastForward += OnStopFF;
        StartCoroutine(LifeCycle());
    }

    private void OnDisable()
    {
        GameManager.Instance.StartFastForward -= OnStartFF;
        GameManager.Instance.StopFastForward -= OnStopFF;
    }

    private void OnStartFF()
    {
        rb.velocity *= multiplier;
    }

    private void OnStopFF()
    {
        rb.velocity = startVelocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().PlayerLoose();
        }
    }

    IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(lifeDuration);
        Destroy(gameObject);
    }
}
