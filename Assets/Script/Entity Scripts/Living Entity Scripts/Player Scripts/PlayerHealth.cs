using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != currentHealth)
        {
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
         
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
    }


    public void Heal(float amount)
    {
        currentHealth += amount; 
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
