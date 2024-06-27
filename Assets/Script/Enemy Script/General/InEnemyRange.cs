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
        //UnityEngine.Debug.Log(timer);
        if (timer >= 3f)
        {
            //UnityEngine.Debug.Log("Successive attack");
            StartCoroutine(Attack());
            timer = 0f;
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
}
