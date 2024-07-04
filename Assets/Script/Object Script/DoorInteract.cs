using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using Unity.AI.Navigation;
using UnityEngine.AI;
using UnityEngine;


public class DoorInteract : MonoBehaviour
{
    public NavMeshSurface navSurface;
    public NavMeshSurface sampleSurface;

    public Transform doorRotation;
    public GameObject openHitbox;
    public bool state;

    public float rotationSpeed;
    private bool playerInRange = false;
    private Quaternion targetRotation;

    private Vector3 temp;

    public float duration = 1.0f;

    void Start()
    {
        //sampleSurface.enabled = false;
        sampleSurface.BuildNavMesh();

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
            if (Input.GetKeyDown("f"))
            {
                sampleSurface.enabled = true;
                if (!state)
                {
                    StartCoroutine(RotateDoor(new Vector3(0, 120, 0)));
                    state = true;
                }
                else
                {
                    StartCoroutine(RotateDoor(new Vector3(0, 0, 0)));
                    state = false;
                }
            }
        }
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
    
    private IEnumerator RotateDoor(Vector3 targetRotation)
    {
        Quaternion startRotation = doorRotation.rotation;
        Quaternion endRotation = Quaternion.Euler(targetRotation);
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            doorRotation.rotation = Quaternion.Lerp(startRotation, endRotation, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        sampleSurface.BuildNavMesh();
        navSurface.BuildNavMesh();
        doorRotation.rotation = endRotation;
    }
}
