using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockback : MonoBehaviour
{
    public ResourceEnergy resourceEnergyScript;

    [SerializeField] private float knockbackStrength;

    public GameObject knockbackObj;
    public GameObject start;
    public GameObject end;

    public float force;

    private void Update()
    {
        knockbackObj.transform.position = end.transform.position;
    }

    public void DoKnockback()
    {
        if (resourceEnergyScript.currentHealth >= 1f)
        {
            PushForward();
        }
    }

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {
            Rigidbody rb = collision.collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 direction = collision.transform.position - transform.position;
                direction.y = 0;

                rb.AddForce(direction.normalized * knockbackStrength, ForceMode.Impulse);
            }
        }
    }

    public void PushForward()
    {
        knockbackObj.transform.position = Vector3.MoveTowards(start.transform.position, end.transform.position, Time.deltaTime * force);
    }
}
