using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //turret
    public float rotateRange;
    public float rotateSpeed;
    public Transform headPivot;

    //bullets
    public Transform bulletSpawner;
    public GameObject bulletPrefab;

    public float bulletForce;
    public float fireRate;

    private void Start()
    {
        InvokeRepeating("ShootBullets", 0f, fireRate);
    }

    void Update()
    {
        headPivot.rotation = Quaternion.AngleAxis(Mathf.Sin(Time.time * rotateSpeed) * rotateRange, Vector3.up);
    }

    void ShootBullets()
    {
        Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
    }

}