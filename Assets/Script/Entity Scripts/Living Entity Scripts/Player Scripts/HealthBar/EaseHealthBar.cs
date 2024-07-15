using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EaseHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHealth = 100f;
    public float health;
    private float lerpSpeed = 0.05f;
    public TextMeshProUGUI healthText;
    public bool isDead = false;

    //Health Kits 
    public GameObject Medkit;
    public GameObject Bandage;
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }
        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed);
        }
        // Sample Take Damage
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }

        string healthTextValue = health.ToString();
        healthText.text = healthTextValue;

    }

    public void TakeDamage(float damage)
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
        Debug.Log("Health" + amount);
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void OnPickedUp(string healthKit)
    {
        if (healthKit == "Medkit")
        {
            Destroy(Medkit);
        }    
        if (healthKit == "Bandage")
        {
            Destroy(Bandage);
        }
    }
}
