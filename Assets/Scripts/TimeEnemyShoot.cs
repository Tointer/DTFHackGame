using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEnemyShoot : MonoBehaviour
{
    public float shootCooldown = 3f;
    public float bulletSpeed = 1f;
    
    public GameObject bulletObject;
    private GameObject playerObject;
    
    private float multiplier = 1f;

    void Start()
    {
        GameManager.Instance.StartFastForward += OnStartFF;
        GameManager.Instance.StopFastForward += OnStopFF;
        
        playerObject = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ShootCycle());
    }

    IEnumerator ShootCycle()
    {
        Shoot();
        var counter = 0f;
        while (true)
        {
            counter += Time.deltaTime * multiplier;
            if (counter >= shootCooldown)
            {
                Shoot();
                counter = 0;
                yield return null;
            }
            else
            {
                yield return null;
            }
        }
    }

    private void Shoot()
    {
        var bullet = Instantiate(bulletObject, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity =
            (playerObject.transform.position - transform.position).normalized * bulletSpeed;
    }
    
    private void OnDisable()
    {
        GameManager.Instance.StartFastForward -= OnStartFF;
        GameManager.Instance.StopFastForward -= OnStopFF;
    }
    
    private void OnStartFF()
    {
        multiplier = 2f;
    }

    private void OnStopFF()
    {
        multiplier = 1f;
    }
}
