using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;


public class EnemyPathing : MonoBehaviour
{
    public NavMeshAgent agent;
    //public CapsuleCollider hostileRange;
    public GameObject hostileRange;


    void Start()
    {
        hostileRange = GetComponent<GameObject>();
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
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("Player entered the hostile range!");
        }
    }
}
