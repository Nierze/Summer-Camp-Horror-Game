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

}
