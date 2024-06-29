using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    public GameObject enemyHPBar;
    public tempEnemyEaseHealthBar enemyHP;
    public Canvas canvas;

    void Start()
    {
        GameObject hpbar = Instantiate(enemyHPBar);
        enemyHP = hpbar.GetComponent<tempEnemyEaseHealthBar>();;
        hpbar.transform.SetParent(canvas.transform, false);
    }

    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            // EnemyTakeDamage(10f);
            enemyHP.TakeDamage(10f);
        }

        if (Input.GetKeyDown("k"))
        {
            // EnemyHealDamage(10f);
            enemyHP.Heal(10f);
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
