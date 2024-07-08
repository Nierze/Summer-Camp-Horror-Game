using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InEnemyRange : MonoBehaviour
{
    public TiyanakAttackPattern playerInRange;
    public PugotAttackPattern playerInRange2;
    public EaseHealthBar targetHealthbar;
    public float timer = 0f;
    public Animator anim;

    public void Update()
    {
        timer += Time.deltaTime;
        //if(playerInRange.inLungeAttack) UnityEngine.Debug.Log("in lunge attack bool = " + playerInRange.inLungeAttack);
        if (playerInRange != null)
        {
            if (!playerInRange.inLungeAttack)
            {
                if (timer >= 3f)
                {
                    //UnityEngine.Debug.Log("Successive attack");
                    StartCoroutine(Attack());
                    timer = 0f;
                }
            }

            if (playerInRange.inLungeAttack)
            {
                //UnityEngine.Debug.Log("inEnemyRange = " + playerInRange.playerInRange);

                StartCoroutine(TiyanakLungeAttack());
            }
        }

        if (playerInRange2 != null)
        {
            if (playerInRange2.inSprintAttack)
            {
                //UnityEngine.Debug.Log("inEnemyRange = " + playerInRange2.playerInRange);

                StartCoroutine(PugotSprintAttack());
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timer = 0f;
            //UnityEngine.Debug.Log("In Range, Attacks!");
            if(playerInRange != null) playerInRange.playerInRange = true;
            if (playerInRange2 != null) playerInRange2.playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //UnityEngine.Debug.Log("Outside Range");
            if (playerInRange != null) playerInRange.playerInRange = false;
            if (playerInRange2 != null) playerInRange2.playerInRange = false;

        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(3f);

        if(playerInRange != null) if(playerInRange.playerInRange) targetHealthbar.TakeDamage(10);
        if(playerInRange2 != null) if(playerInRange2.playerInRange) targetHealthbar.TakeDamage(10);
    }

    private IEnumerator TiyanakLungeAttack()
    {
        //UnityEngine.Debug.Log("inEnemyRange = " + playerInRange.playerInRange);
        if (playerInRange != null)
        {
            if (playerInRange.playerInRange)
            {
                targetHealthbar.TakeDamage(10);
                if (playerInRange != null) playerInRange.inLungeAttack = false;
                anim.SetTrigger("HitTrigger");
            }
        }

        yield return new WaitForSeconds(0.01f);
    }

    private IEnumerator PugotSprintAttack()
    {
        //UnityEngine.Debug.Log("inEnemyRange = " + playerInRange2.playerInRange);
        if (playerInRange2 != null)
        {
            if (playerInRange2.playerInRange)
            {
                targetHealthbar.TakeDamage(10);
                if (playerInRange2 != null) playerInRange2.inSprintAttack = false;
            }
        }

        yield return new WaitForSeconds(0.01f);
    }
}
