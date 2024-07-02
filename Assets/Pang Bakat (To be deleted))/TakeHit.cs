using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            UnityEngine.Debug.Log("took damage");
            //takeDamage(10f);
            //UnityEngine.Debug.Log("host hp = " + CurrentHP);
        }
    }
}
