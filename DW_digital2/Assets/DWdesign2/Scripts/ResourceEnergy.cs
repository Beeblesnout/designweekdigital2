using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ResourceEnergy : MonoBehaviour
{
    public Slider healthBarSlider;

    public float currentHealth;
    public float maxPlayerHealth;

    public float regenRate;
    public UnityEvent OnDamage;

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
        OnDamage.Invoke();
    }

    private void UpdateHpbar()
    {
        healthBarSlider.value = currentHealth / maxPlayerHealth;
    }

    public void RegenHealth()
    {
        currentHealth += regenRate;
    }
}
