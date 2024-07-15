using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    public static StaminaSystem Instance { get; private set; }
    public float maxStamina;
    public float currentStamina;
    public float staminaRegen;

    public Slider sliderLeft;
    public Slider sliderRight;


    private float timeBeforeRegenStart = 0.7f;
    private float timeBeforeRegenEnd = 0f;
    private bool staminaIsUsed = false;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            currentStamina = maxStamina;
        }
        else
        {
            Destroy(gameObject);
        }

        currentStamina = maxStamina;
    }

    private void Update()
    {

        if (currentStamina < maxStamina)
        {
            if (staminaIsUsed && timeBeforeRegenEnd <= 0)
            {
                currentStamina += staminaRegen;
            } 
            else
            {
                timeBeforeRegenEnd -= Time.deltaTime;
            }
        }


        // In case if overflow
        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }
        
        sliderLeft.value = currentStamina;
        sliderRight.value = currentStamina;
    }

    public void useStamina(float staminaConsumed)
    {
        if (currentStamina >= staminaConsumed) {
            currentStamina -= staminaConsumed;
        } else {
            currentStamina = 0;
        }

        staminaIsUsed = true;
        timeBeforeRegenEnd = timeBeforeRegenStart;
        
    }
    
}
