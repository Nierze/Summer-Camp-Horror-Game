using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class TiyanakAttackPattern : MonoBehaviour
{
    public static bool playerAttackDetected = false;
    
    [SerializeField] public string difficulty = "easy";
    private int decision = 0;

    public Transform target;
    //private UnityEngine.AI.NavMeshAgent agent;

    public bool actionPhase = false;

    //temp lunge
    public Rigidbody rb;
    public bool isLunging = false;
    public Vector3 lungeTarget;
    public float dashSpeed;
    public float dashTime;

    //NavMesh
    public UnityEngine.AI.NavMeshAgent agent;
    public float movementSpeed = 15f;
    public GameObject playerPos;
    public GameObject hostileRange;

    //timer
    public float timer = 0f;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        hostileRange = GetComponent<GameObject>();
    }


    void Update()
    {
        // timer += Time.deltaTime;
        // if(timer >= 3f)
        // {
        //     DefaultMovement();
        //     UnityEngine.Debug.Log("no attack detected");
        //     timer = 0f;
        // }
        float radius = 5f;
        float distance = Vector3.Distance(transform.position, playerPos.transform.position);

        if(playerAttackDetected && !actionPhase)
        {
            AttackDetected(difficulty);
            actionPhase = true;
            UnityEngine.Debug.Log(actionPhase);
            playerAttackDetected = false;
        }
        
        else if (distance <= radius && !actionPhase)
        {
            //UnityEngine.Debug.Log("distance = " + distance);
            //UnityEngine.Debug.Log("radius = " + radius);
            //UnityEngine.Debug.Log("In enemy melee range");
            UnityEngine.Debug.Log("Attacks!");
            actionPhase = true;
            StartCoroutine(tempCooldown(1));
        }
        // else if(transform.position == playerPos.transform.position)
        // {
        //     UnityEngine.Debug.Log("In enemy melee range");
        // }
        
        else if(!actionPhase)
        {
            DefaultMovement();
        }

    }

    void AttackDetected(string difficulty)
    {
        switch(difficulty)
        {
            case "easy":
                decision = UnityEngine.Random.Range(1, 6);
                //actionPhase = true;
                switch(decision)
                {
                    case 1: case 2: case 3: case 5:
                        actionPhase = true;
                        UnityEngine.Debug.Log("Do nothing");
                        // DefaultMovement();
                        StartCoroutine(LungeAttack());
                        StartCoroutine(tempCooldown(3));
                    break;
                    
                    case 4: 
                        UnityEngine.Debug.Log("Normal Attack");
                        actionPhase = true;
                        //NormalAttack();
                        StartCoroutine(LungeAttack());
                        StartCoroutine(tempCooldown(3));
                    break;

                    // case 5:
                    //     UnityEngine.Debug.Log("Retreat");
                    //     actionPhase = true;
                    //     StartCoroutine(tempCooldown(3));
                    // break;
                }

            break;

            
        }

        decision = 0;
    }

    void DefaultMovement()
    {
        Vector3 newPosition = playerPos.transform.position;
        agent.SetDestination(newPosition);

    }

    void NormalAttack()
    {
        agent.destination = target.position;
    }

    // void LungeAttack()
    // {
    //     float startTime = Time.time;

    //     while (Time.time <= startTime + dashTime)
    //     {
    //         transform.position += transform.forward * dashSpeed * Time.deltaTime;
            
    //     }

    // }
        // while(Time.time < startTime + dashTime)
        // {
        //     dashSpeed * Time.deltaTime;
        // }
    void Retreat(){}

    void Evade() { }

    private IEnumerator EndLunge()
    {
        yield return new WaitForSeconds(2f);
        isLunging = false;
    }

    private IEnumerator tempCooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        actionPhase = false;
    }

    private IEnumerator LungeAttack()
    {
        float startTime = Time.time;

        while (Time.time <= startTime + dashTime)
        {
            transform.position += transform.forward * dashSpeed * Time.deltaTime;
            yield return null; // Yield to the next frame
        }

        // Optionally, perform actions after the dash is completed
        Debug.Log("Lunge attack completed.");

        // Example: Restart the coroutine or perform other actions
        // StartCoroutine(LungeAttack()); // Uncomment if you want to restart
    }
}

/*

case "normal":
                // UnityEngine.Debug.Log("difficulty is set to " + difficulty);
                decision = UnityEngine.Random.Range(1, 8);
                // UnityEngine.Debug.Log("decision = " + decision);
                switch(decision)
                {
                    case 1: case 2:
                        UnityEngine.Debug.Log("Do nothing");
                        lungeTarget = target.transform.position;
                        isLunging = true;
                        //LungeAttack();
                     break;
                    
                    case 3: case 4: 
                        UnityEngine.Debug.Log("Normal Attack");
                        //NormalAttack();
                        lungeTarget = target.transform.position;
                        isLunging = true;
                        //LungeAttack();   
                    break;


                    case 5: case 6:
                        UnityEngine.Debug.Log("Lunge Attack");
                        isLunging = true;
                        lungeTarget = target.transform.position;
                        //LungeAttack();
                        break;

                    case 7:
                        UnityEngine.Debug.Log("Retreat");
                        isLunging = true;
                        lungeTarget = target.transform.position;
                        //LungeAttack();
                        break;
                }
            break;

            case "hard":
                // UnityEngine.Debug.Log("difficulty is set to " + difficulty);
                decision = UnityEngine.Random.Range(1, 11);
                // UnityEngine.Debug.Log("decision = " + decision);
                switch(decision)
                {
                    case 1:
                        UnityEngine.Debug.Log("Do nothing");
                    break;
                    
                    case 2: case 3: case 4: 
                        UnityEngine.Debug.Log("Normal Attack");
                        NormalAttack();
                    break;

                    case 5: case 6: case 7:
                        UnityEngine.Debug.Log("Lunge Attack");
                    break;

                    case 8:
                        UnityEngine.Debug.Log("Retreat");
                    break;

                    case 9: case 10:
                        UnityEngine.Debug.Log("Evade");
                    break;
                }
            break;

*/