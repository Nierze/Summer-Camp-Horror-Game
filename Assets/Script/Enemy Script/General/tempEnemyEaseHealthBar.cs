using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
public class tempEnemyEaseHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    //public float maxHealth;
    //public float health;
    private float lerpSpeed = 0.05f;
    public TextMeshProUGUI healthText;
    public bool isDead = false;

    public EntityStats host;

    public GameObject projectile;

    ////////////////////////////////////
    /// 
    public float currentHPPercentage;

    // Start is called before the first frame update
    void Start()
    {
        //maxHealth = host.FlatHP;
        //health = maxHealth;

        currentHPPercentage = (host.CurrentHP / host.MaxHP ) * 100;
        
    }

    // Update is called once per frame
    void Update()
    {
        projectile = UnityEngine.GameObject.FindGameObjectWithTag("Projectile");
        currentHPPercentage = (host.CurrentHP / host.MaxHP) * 100;
        if (healthSlider.value != currentHPPercentage)
        {
            healthSlider.value = currentHPPercentage;
        }
        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, currentHPPercentage, lerpSpeed);
        }

        if (Input.GetKeyDown("l"))
        {

            host.takeDamage(10f);
            UnityEngine.Debug.Log("host takes damage");
            UnityEngine.Debug.Log("host hp = " + host.CurrentHP);

        }

        if (Input.GetKeyDown("k"))
        {
            host.heal(10f);
            UnityEngine.Debug.Log("host heals damage");
            UnityEngine.Debug.Log("host hp = " + host.CurrentHP);

        }

    }

    /*public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            isDead = true;
        }
    }

    public void Heal(float amount)
    {
        health += amount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }*/
}
