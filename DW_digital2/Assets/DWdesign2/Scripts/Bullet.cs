using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 1;
    public float bulletLife;

    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        bulletLife -= Time.deltaTime;
        if (bulletLife < 0) Destroy(gameObject);
    }
}
