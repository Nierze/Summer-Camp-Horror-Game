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

    // Start is called before the first frame update
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
            takeDamage(10);
        }

        string healthTextValue = health.ToString();
        healthText.text = healthTextValue;

    }

    void takeDamage(float damage)
    {
        health -= damage;
    }
}
