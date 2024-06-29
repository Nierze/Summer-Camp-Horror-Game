using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempEnemyStats : MonoBehaviour
{
    public float hp = 100f;
    public float attackDamage = 10f;

    void Start()
    {
        switch(DifficultySelector.setDifficulty)
        {
            case "easy":
                hp *= 1;
                attackDamage *= 1f;
            break;

            case "normal":
                hp *= 1.3f;
                attackDamage *= 1.5f;
            break;
        
            case "hard":
                hp *= 2f;
                attackDamage *= 2f;
            break;
        }

        UnityEngine.Debug.Log("hp = " + hp + " || ad = " + attackDamage);
    }
}
