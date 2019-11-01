using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DamagePlayer : MonoBehaviour
{
    public Slider healthBarSlider;
    public float currentHealth;
    public float maxPlayerHealth;

    public UnityEvent OnDamage;
    public UnityEvent OnDeath;

    // Update is called once per frame
    void Update()
    {
        healthBarSlider.value = currentHealth / maxPlayerHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            DealDamage(25);
        }

        if (currentHealth <= 0)
        {
            OnDeath.Invoke();
        }
    }

    public void DealDamage(float damage)
    {
        currentHealth -= damage;
        OnDamage.Invoke();

        if (currentHealth <= 0)
        {
            OnDeath.Invoke();
        }
    }
}
