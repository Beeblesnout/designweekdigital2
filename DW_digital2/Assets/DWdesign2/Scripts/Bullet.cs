﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 1;
    public float bulletLife;
    float life;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        life -= Time.deltaTime;
        if (life < 0) Destroy(gameObject);
    }
}
