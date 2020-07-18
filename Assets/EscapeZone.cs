using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            if (gm == null) Debug.LogError("Wrong object with \"GameManager\" tag");
            gm.PlayerWins();
        }
    }
}
