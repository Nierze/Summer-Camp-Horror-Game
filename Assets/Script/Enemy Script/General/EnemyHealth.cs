using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth = 100f;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            EnemyTakeDamage(10f);
        }

        if (Input.GetKeyDown("k"))
        {
            EnemyHealDamage(10f);
        }
    }

    void EnemyTakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    void EnemyHealDamage(float healValue)
    {
        if(currentHealth < maxHealth) currentHealth += healValue;

        if(currentHealth > maxHealth) currentHealth = maxHealth;
    }
}
