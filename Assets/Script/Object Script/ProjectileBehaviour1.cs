using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ProjectileBehaviour1 : MonoBehaviour
{
    public float speed = 10f;
    public GameObject cam;
    public float throwForce;
    public float throwUpwardForce;
    Rigidbody rb;

    public EntityStats entity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = UnityEngine.GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        //rb.velocity = transform.forward * speed;
        Vector3 forceToAdd = cam.transform.forward * throwForce + transform.up * throwUpwardForce;
        rb.AddForce(forceToAdd, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject parent = other.transform.parent.gameObject;
            EntityStats entity = parent.GetComponent<EntityStats>();
            entity.EnemyTakeDamage();
            UnityEngine.Debug.Log("Enemy took hit");
        }
    }
}
