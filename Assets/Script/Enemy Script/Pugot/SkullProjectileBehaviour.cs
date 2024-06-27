using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullProjectileBehaviour : MonoBehaviour
{
    public Rigidbody skullrb;
    [SerializeField] public float projectileSpeed;
    [SerializeField] public float projectileUpwardForce;

    void Start()
    {
        skullrb = GetComponent<Rigidbody>();
        skullrb.AddForce(Vector3.up * projectileUpwardForce, ForceMode.Impulse);
        skullrb.AddForce(skullrb.transform.forward * projectileSpeed, ForceMode.Impulse);
        //skullrb.AddForce(skullrb.transform.forward * projectileSpeed);
    }

}
