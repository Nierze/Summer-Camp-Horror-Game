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
    public float dashSpeed = 100f;
    public float dashTime = 0.5f;
    public float radius;
    public float distance;
    private float lungeStartTime = -1f;

    //NavMesh
    public UnityEngine.AI.NavMeshAgent agent;
    public float movementSpeed = 15f;
    public GameObject playerPos;
    public GameObject hostileRange;

    //timer
    public float timer = 0f;

    //Deal Damage
    public bool playerInRange = false;
    public EaseHealthBar healthBar;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        //hostileRange = GetComponent<GameObject>();
    }


    void Update()
    {
        radius = 5f;
        distance = Vector3.Distance(transform.position, playerPos.transform.position);

        if(playerAttackDetected && !actionPhase)
        {
            actionPhase = true;
            UnityEngine.Debug.Log(actionPhase);
            playerAttackDetected = false;
            AttackDetected(difficulty);
            
        }
               
        else if(!actionPhase)
        {
            //DefaultMovement();
        }

        /*
        if (lungeStartTime >= 0f && Time.time <= lungeStartTime + dashTime)
        {
            Vector3 directionToPlayer = (playerPos.transform.position - transform.position).normalized;

            transform.position += directionToPlayer * dashSpeed * Time.deltaTime;

        }
        else
        {
            lungeStartTime = -1f;
        }*/
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
                    case 1: case 2: case 3: case 4:
                        actionPhase = true;
                        //UnityEngine.Debug.Log("Do nothing");
                        // DefaultMovement();
                        //healthBar.TakeDamage(10); //

                        StartCoroutine(LungeAttack());
                        dashSpeed = 100f; dashTime = 0.5f;
                        StartCoroutine(tempCooldown(2));
                        //DefaultMovement();
                    break;

                    // case 4:
                    //     UnityEngine.Debug.Log("Retreat");
                    //     actionPhase = true;
                    //     StartCoroutine(tempCooldown(3));
                    // break;

                    case 5:
                        //UnityEngine.Debug.Log("Normal Attack");
                        actionPhase = true;
                        //NormalAttack();
                        //healthBar.TakeDamage(10); //

                        StartCoroutine(LungeAttack());
                        dashSpeed = 100f; dashTime = 0.5f;
                        StartCoroutine(tempCooldown(2));
                        //DefaultMovement();
                    break;
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

    void Retreat(){}

    void Evade() { }

    private IEnumerator EndLunge()
    {
        yield return new WaitForSeconds(2f);
        isLunging = false;
    }

    void StartLungeAttack()
    {
        transform.LookAt(playerPos.transform.position);

        lungeStartTime = Time.time;
    }

    private IEnumerator LungeAttack()
    {
        transform.LookAt(playerPos.transform.position);
        yield return new WaitForSeconds(1f);
        float startTime = Time.time;
        //UnityEngine.Debug.Log("dist = " + Vector3.Distance(transform.position, playerPos.transform.position));
        //UnityEngine.Debug.Log("rad = " + radius);

        float dist = Vector3.Distance(transform.position, playerPos.transform.position);

        //dashSpeed = (dashSpeed * (int)(dist / 40) < 1) ? dashSpeed = 100f : dashSpeed = dashSpeed *= (int)(dist / 40);
        dashTime = (dashSpeed * (int)(dist / 40) < 1) ? dashTime = 0.5f : dashTime *= (int)(dist / 40);
        

        while (Time.time <= startTime + dashTime && (Vector3.Distance(transform.position, playerPos.transform.position) + 6f) > radius)
        {
            transform.position += transform.forward * dashSpeed * Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator tempCooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        actionPhase = false;
    }

}

/*
rb.AddForce(Vector3.forward * (dashSpeed * 10), ForceMode.Impulse);
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