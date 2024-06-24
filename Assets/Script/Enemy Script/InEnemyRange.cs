using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InEnemyRange : MonoBehaviour
{
    public TiyanakAttackPattern playerInRange;
    public EaseHealthBar targetHealthbar;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("In Range, Attacks!");
            playerInRange.playerInRange = true;
            targetHealthbar.TakeDamage(10);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("Outside Range");
            playerInRange.playerInRange = false;
        }
    }
}
