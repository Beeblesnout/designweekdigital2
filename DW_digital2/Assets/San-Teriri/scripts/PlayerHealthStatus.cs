using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthStatus : MonoBehaviour
{
    public DamagePlayer damagePlayerScript;

    public Image healthBarSlider;

    public float currentHealth;
    public float maxPlayerHealth;

     void Update()
    {
        //damagePlayerScript.currentHealth = 100;
        UpdateHpbar();
     }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("th");
            DealDamage(25);
        }
    }

    public void DealDamage(float damage)
     {
        damagePlayerScript.currentHealth -= damage;
       
     }

     private void UpdateHpbar()
     {
        //healthBarSlider.value = CalculateHealth();
        healthBarSlider.fillAmount = damagePlayerScript.currentHealth / maxPlayerHealth;
     }

     //private float CalculateHealth()
     //{
     //  return ((float)currentHealth / maxPlayerHealth);
     //}



}
