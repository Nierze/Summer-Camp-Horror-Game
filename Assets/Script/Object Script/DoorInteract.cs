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

    public float rotationSpeed;
    private bool playerInRange = false;
    private Quaternion targetRotation;

    private Vector3 temp;
    void Start()
    {
        //doorRotation = GetComponent<Transform>();
        rotationSpeed = 45f;
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
        if (playerInRange)
        {
            if (Input.GetKeyDown("f") && !state)
            {
                //UnityEngine.Debug.Log("Open");
                doorRotation.rotation = Quaternion.Euler(new Vector3(0, 120, 0));
                state = true;
            }
            else
            {
                if (Input.GetKeyDown("f"))
                {
                    //UnityEngine.Debug.Log("Close");
                    doorRotation.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    state = false;
                }
            }
        }
        //doorRotation.rotation *= Quaternion.Euler(new Vector3(0, rotationSpeed * Time.deltaTime, 0));

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    bool CloseDoor(bool state)
    {
        while (!state)
        {
            
        }

        state = true;
        return state;
    }
}
