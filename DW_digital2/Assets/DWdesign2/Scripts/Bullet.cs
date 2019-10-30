using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    public float bulletLife;

    // Update is called once per frame
    void Update()
    {
        Kill();
    }

    public void Kill()
    {
        Destroy(gameObject, bulletLife);
    }
}
