using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullProjectileBehaviour : MonoBehaviour
{
    public Rigidbody skullrb;
    [SerializeField] public float projectileSpeed;
    [SerializeField] public float projectileUpwardForce;

    public GameObject playerHealthObject;
    public EaseHealthBar targetHealthbar;

    void Start()
    {
        playerHealthObject = GameObject.Find("Health Bar");
        targetHealthbar = playerHealthObject.GetComponent<EaseHealthBar>();

        UnityEngine.Debug.Log("pho = " + playerHealthObject);
        UnityEngine.Debug.Log("thb = " + targetHealthbar);
        
        skullrb = GetComponent<Rigidbody>();
        skullrb.AddForce(Vector3.up * projectileUpwardForce, ForceMode.Impulse);
        skullrb.AddForce(skullrb.transform.forward * projectileSpeed, ForceMode.Impulse);
        //skullrb.AddForce(skullrb.transform.forward * projectileSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("player hit by enemy projectile");
            targetHealthbar.TakeDamage(10);
        }
    }
}
