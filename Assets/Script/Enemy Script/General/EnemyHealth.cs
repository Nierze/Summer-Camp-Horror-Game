using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 1f;
    public float currentHealth = 1f;

    public GameObject enemyHPBar;
    public tempEnemyEaseHealthBar enemyHP;
    public Canvas canvas;
    public EntityStats thisEntityStats;

    void Start()
    {
        //GameObject hpbar = Instantiate(enemyHPBar);
        enemyHP = GetComponent<tempEnemyEaseHealthBar>();
        enemyHPBar.transform.SetParent(canvas.transform, false);
        thisEntityStats = GetComponent<EntityStats>();

        //thisEntityStats.FlatHP = 200f;
        //enemyHP = thisEntityStats.FlatHP;
        //UnityEngine.Debug.Log(thisEntityStats.FlatHP);
        
    }

    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            // EnemyTakeDamage(10f);
            //enemyHP.TakeDamage(10f);
            thisEntityStats.takeDamage(10f);
        }

        if (Input.GetKeyDown("k"))
        {
            // EnemyHealDamage(10f);
            thisEntityStats.heal(10f);
            //enemyHP.Heal(10f);
        }
    }

    /*public void EnemyTakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    public void EnemyHealDamage(float healValue)
    {
        enemyHP.Heal(healValue);
        //if(currentHealth < maxHealth) currentHealth += healValue;

        //if(currentHealth > maxHealth) currentHealth = maxHealth;
    }*/
}
