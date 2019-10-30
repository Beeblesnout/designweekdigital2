using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //turret
    public float rotateSpeed;

    //bullets
    public Transform bulletSpawner;
    public GameObject bulletPrefab;

    public float bulletForce;
    public float fireRate;

    private void Start()
    {
        InvokeRepeating("ShootBullets", 0f, fireRate);
    }

    //Update is called once per frame
    void Update()
    {
        RotatePosition();
    }

    void RotatePosition()
    {
        transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
    }

    void ShootBullets()
    {
        GameObject bullet;
        bullet = Instantiate(bulletPrefab, bulletSpawner.position, Quaternion.identity) as GameObject;

        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawner.right * bulletForce, ForceMode.Impulse);
    }

}