using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float force;
    public Transform forceBox;
    public Transform forceOrigin;
    public ResourceEnergy resourceEnergyScript;
    public ParticleSystem particle;

    public void DoKnockback()
    {
        if (resourceEnergyScript.currentHealth < 25) return;
        var col = Physics.OverlapBox(forceBox.position, forceBox.localScale * 2, forceBox.rotation);
        foreach (var c in col)
        {
            Rigidbody rb = c.gameObject.GetComponent<Rigidbody>();
            if (rb)
            {
                print("boop");
                rb.AddForce(transform.forward * force, ForceMode.Impulse);
            }
        }
        particle.Play();
        resourceEnergyScript.DealDamage(25);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(forceBox.position, forceBox.localScale * 2);
    }
}
