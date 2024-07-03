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
    public Vector3 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = UnityEngine.GameObject.FindGameObjectWithTag("MainCamera");
        direction = cam.transform.forward;
    }

    void Update()
    {
        rb.velocity = direction * speed;
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
