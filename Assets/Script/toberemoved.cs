using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toberemoved : MonoBehaviour
{
    public TiyanakAttackPattern playerInRange;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("In Range, Attacks!");
            playerInRange.playerInRange = true;
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
