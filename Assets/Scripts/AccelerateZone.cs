using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerateZone : MonoBehaviour
{
    public Transform direction;
    public float acceleration = 2f;
    private bool isDragged;

    private IEnumerator MovePlayer(Transform playerTransform)
    {
        while (isDragged)
        {
            var dir = direction.transform.position - transform.position;
            playerTransform.Translate(dir.normalized * Time.deltaTime * acceleration);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isDragged = true;
            StartCoroutine(MovePlayer(other.transform));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isDragged = false;
        }
    }
}
