using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    [SerializeField] private float flatHP = 100;
    [SerializeField] private float mulHP = 1f;
    [SerializeField] private float flatATK = 10;
    [SerializeField] private float mulATK = 1f;
    [SerializeField] private float dmgReduction = 0f;


    void Awake()
    {
        computeMaxHP();
    }

    /////////////////////////////////////////////////////////////////
    /// Damage Computers
    
    public void takeDamage(float damage)
    {
        float finalDamage = damage - (damage * dmgReduction);
        flatHP -= finalDamage;
    }

    public void dealDamage(float damage, EntityStats target)
    {
        float finalDamage = damage - (damage * target.DMGReduction);
        target.FlatHP -= finalDamage;
    }

    public void heal(float healAmount)
    {
        flatHP += healAmount;
    }   

    private void computeMaxHP()
    {
        flatHP = flatHP * mulHP;
    }


    /////////////////////////////////////////////////////////////////
    /// Getters and Setters
  
    // Getter and Setter for FlatHP
    public float FlatHP
    {
        get { return flatHP; }
        set { flatHP = value; }
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