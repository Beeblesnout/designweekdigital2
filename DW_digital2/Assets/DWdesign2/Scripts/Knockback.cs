using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float force;
    public float cost;
    public Transform forceBox;
    public ResourceEnergy resourceEnergyScript;
    public ParticleSystem particle;

    public void DoKnockback()
    {
        if (resourceEnergyScript.currentHealth < cost) return;
        var col = Physics.OverlapBox(forceBox.position, forceBox.localScale * 2, forceBox.rotation);
        foreach (var c in col)
        {
            Rigidbody rb = c.gameObject.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddForce(transform.forward * force, ForceMode.Impulse);
            }
        }
        particle.Play();
        resourceEnergyScript.DealDamage(cost);
    }
}
