using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //turret
    public float rotateRange;
    public float rotateSpeed;
    public Transform headPivot;
    float defaultRot;

    //bullets
    public Transform bulletSpawner;
    public GameObject bulletPrefab;

    public float bulletForce;
    public float fireRate;

    private void Start()
    {
        InvokeRepeating("ShootBullets", 0f, fireRate);
        defaultRot = headPivot.rotation.eulerAngles.y;
    }

    void Update()
    {
        headPivot.rotation = Quaternion.AngleAxis(defaultRot + Mathf.Sin(Time.time * rotateSpeed) * rotateRange, Vector3.up);
    }

    void ShootBullets()
    {
        Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
    }

}