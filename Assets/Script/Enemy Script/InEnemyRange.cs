using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InEnemyRange : MonoBehaviour
{
    public TiyanakAttackPattern playerInRange;
    public EaseHealthBar targetHealthbar;
    public float timer = 0f;

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 3f)
        {
            UnityEngine.Debug.Log("Successive attack");
            StartCoroutine(Attack());
            timer = 0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timer = 0f;
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

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(3f);
        if(playerInRange.playerInRange) targetHealthbar.TakeDamage(10);
    }
}
