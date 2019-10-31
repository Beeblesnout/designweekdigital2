using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePlayer : MonoBehaviour
{
    public Image healthBarSlider;

    public Transform playerSpawn;

    public float currentHealth;
    public float maxPlayerHealth;

    // Update is called once per frame
    void Update()
    {
        UpdateHpbar();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("th");
            DealDamage(25);
        }

        if (currentHealth <= 0)
        {
            transform.position = playerSpawn.position;
            currentHealth = maxPlayerHealth;
        }
    }

    public void DealDamage(float damage)
    {
        currentHealth -= damage;

    }

    private void UpdateHpbar()
    {
        //healthBarSlider.value = CalculateHealth();
        healthBarSlider.fillAmount = currentHealth / maxPlayerHealth;
    }

    
}
