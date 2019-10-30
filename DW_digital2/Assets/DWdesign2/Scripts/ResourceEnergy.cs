using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceEnergy : MonoBehaviour
{
    public Image healthBarSlider;

    public float currentHealth;
    public float maxPlayerHealth;

    public float regenRate;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateHpbar();
        Invoke("RegenHealth", 0f);

        if (Input.GetKeyDown(KeyCode.Q) && currentHealth >= 0)
        {
            DealDamage(25);
        }
        if (currentHealth > maxPlayerHealth)
        {
            CancelInvoke();
        }
        
    }

    public void DealDamage(float damage)
    {
        currentHealth -= damage;

    }

    private void UpdateHpbar()
    {
        healthBarSlider.fillAmount = currentHealth / maxPlayerHealth;
    }

    public void RegenHealth()
    {
        currentHealth += regenRate;
    }
}
