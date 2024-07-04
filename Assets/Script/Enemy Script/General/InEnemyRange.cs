using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InEnemyRange : MonoBehaviour
{
    public TiyanakAttackPattern playerInRange;
    public PugotAttackPattern playerInRange2;
    public EaseHealthBar targetHealthbar;
    public float timer = 0f;

    public void Update()
    {
        timer += Time.deltaTime;
        //if(playerInRange.inLungeAttack) UnityEngine.Debug.Log("in lunge attack bool = " + playerInRange.inLungeAttack);
        if (!playerInRange.inLungeAttack)
        {
            if (timer >= 3f)
            {
                //UnityEngine.Debug.Log("Successive attack");
                StartCoroutine(Attack());
                timer = 0f;
            }
        }

        if(playerInRange.inLungeAttack)
        {
            UnityEngine.Debug.Log("inEnemyRange = " + playerInRange.playerInRange);
            
            StartCoroutine(TiyanakLungeAttack());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timer = 0f;
            //UnityEngine.Debug.Log("In Range, Attacks!");
            playerInRange.playerInRange = true;
            playerInRange2.playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //UnityEngine.Debug.Log("Outside Range");
            playerInRange.playerInRange = false;
            playerInRange2.playerInRange = false;

        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(3f);
        if(playerInRange.playerInRange) targetHealthbar.TakeDamage(10);
        if (playerInRange2.playerInRange) targetHealthbar.TakeDamage(10);
    }

    private IEnumerator TiyanakLungeAttack()
    {
        UnityEngine.Debug.Log("inEnemyRange = " + playerInRange.playerInRange);
        if (playerInRange.playerInRange)
        {
            targetHealthbar.TakeDamage(10);
            playerInRange.inLungeAttack = false;
        }

        yield return new WaitForSeconds(0.01f);
        
    }
}
