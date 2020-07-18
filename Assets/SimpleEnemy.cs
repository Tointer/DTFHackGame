using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public float shootCooldown = 2f;
    public float bulletSpeed = 1f;
    
    public GameObject bulletObject;
    private GameObject playerObject;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ShootCycle());
    }

    IEnumerator ShootCycle()
    {
        yield return new WaitForSeconds(shootCooldown/4);
        while (true)
        {
            var bullet = Instantiate(bulletObject, transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity =
                (playerObject.transform.position - transform.position).normalized * bulletSpeed;
            yield return new WaitForSeconds(shootCooldown);
        }
    }
    
}
