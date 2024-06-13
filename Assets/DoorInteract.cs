using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    public Transform doorRotation;
    public GameObject openHitbox;
    public bool state;
    public float rotationSpeed = 1f;

    private Quaternion targetRotation;

    void Start()
    {
        doorRotation = GetComponent<Transform>();
        doorRotation.rotation = Quaternion.Euler(0, 0, 0);
        state = false;

        if (state)
        {
            targetRotation = Quaternion.Euler(0, -240, 0);
        }
        else
        {
            targetRotation = doorRotation.rotation;
        }
    }

    void Update()
    {
        //if (state)
        //{
        //    doorRotation.rotation = Quaternion.Euler(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
        //}
        /*
        if (state)
        {
            targetRotation = Quaternion.Euler(0, -240, 0);
        }
        else
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
        }

        doorRotation.rotation = Quaternion.RotateTowards(doorRotation.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        */
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("Open");
            doorRotation.rotation = Quaternion.Euler(new Vector3(0, -240, 0));
            state = true;
        }
    }

}
