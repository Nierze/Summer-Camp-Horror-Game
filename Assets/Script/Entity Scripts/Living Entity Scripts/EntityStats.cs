using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    [SerializeField] private float maxHP;
    [SerializeField] private float baseHP = 100;
    [SerializeField] private float currentHP;
    [SerializeField] private float mulHP = 1f;
    [SerializeField] private float flatATK = 10;
    [SerializeField] private float mulATK = 1f;
    [SerializeField] private float dmgReduction = 0f;
 


    void Awake()
    {
        UnityEngine.Debug.Log("entity stats diff = " + DifficultySelector.setDifficulty);
        switch (DifficultySelector.setDifficulty)
        {
            case "easy":
                mulHP = 0.75f;
            break;

            case "normal":
                mulHP = 1f;
            break;

            case "hard":
                mulHP = 2f;
            break;

            default:
                mulHP = 1f;
            break;
        }
        maxHP = baseHP * mulHP;
        currentHP = maxHP;
    }

    /////////////////////////////////////////////////////////////////
    /// Damage Computers
    
    public void takeDamage(float damage)
    {
        float finalDamage = damage - (damage * dmgReduction);
        currentHP -= finalDamage;
    }

    public void dealDamage(float damage, EntityStats target)
    {
        float finalDamage = damage - (damage * target.DMGReduction);
        target.currentHP -= finalDamage;
    }

    public void heal(float healAmount)
    {
        currentHP += healAmount;
        if (currentHP > maxHP) currentHP = maxHP;
    }   

    private void computeMaxHP()
    {
        currentHP = currentHP * mulHP;
    }


    /////////////////////////////////////////////////////////////////
    /// Getters and Setters
  
    // Getter and Setter for BaseHP
    public float BaseHP
    {
        get { return baseHP; }
        set { baseHP = value; }
    }

    // Getter and Setter for CurrentHP
    public float CurrentHP
    {
        get { return currentHP; }
        set { currentHP = value; }
    }

    // Getter and Setter for MaxHP
    public float MaxHP
    {
        get { return maxHP; }
        set { maxHP = value; }
    }

    // Getter and Setter for MulHP
    public float MulHP
    {
        get { return mulHP; }
        set { mulHP = value; }
    }

    // Getter and Setter for FlatATK
    public float FlatATK
    {
        get { return flatATK; }
        set { flatATK = value; }
    }

    // Getter and Setter for MulATK
    public float MulATK
    {
        get { return mulATK; }
        set { mulATK = value; }
    }

    // Getter and Setter for DMGReduction
    public float DMGReduction
    {
        get { return dmgReduction; }
        set { dmgReduction = value; }
    }
}