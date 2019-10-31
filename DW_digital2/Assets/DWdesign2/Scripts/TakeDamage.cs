﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{

    public ResourceEnergy resourceEnergyScript;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Hazard")
        {
            Debug.Log("w");
            resourceEnergyScript.DealDamage(20f);
        }
    }

}