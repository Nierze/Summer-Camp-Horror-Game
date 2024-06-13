using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;


public class EnemyPathing : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject hostileRange;
    public float movementSpeed;

    void Start()
    {
        hostileRange = GetComponent<GameObject>();
        movementSpeed = 5.0f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(movePosition, out var hitInfo))
            {
                agent.SetDestination(hitInfo.point);
            }
        }

        agent.speed = movementSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("Player entered the hostile range!");
            //movementSpeed *= 2;
        }
        
    }
}
