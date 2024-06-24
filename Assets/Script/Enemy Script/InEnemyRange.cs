using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InEnemyRange : MonoBehaviour
{
    public TiyanakAttackPattern playerInRange;
    public EaseHealthBar targetHealthbar;

    public void Update()
    {
        //if (playerInRange.playerInRange) StartCoroutine(Attack());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("In Range, Attacks!");
            playerInRange.playerInRange = true;
            StartCoroutine(Attack());
            //targetHealthbar.TakeDamage(10);
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

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(3f);
        if(playerInRange.playerInRange) targetHealthbar.TakeDamage(10);
    }
}
