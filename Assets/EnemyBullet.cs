using System;
using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float lifeDuration = 10f;

    private void Start()
    {
        StartCoroutine(LifeCycle());
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
