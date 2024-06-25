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
    public bool enableMove = true;
    //temp lunge
    public Rigidbody rb;
    public bool isLunging = false;
    public Vector3 lungeTarget;
    public Vector3 jump;
    public float jumpForce = 12.0f;
    public float dashSpeed = 100f;
    public float dashTime = 0.5f;
    public float radius;
    public float distance;
    private float lungeStartTime = -1f;
    public bool playerDetected = false;

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

        //jump = new Vector3(0.0f, 2.0f, 0.0f);

        //hostileRange = GetComponent<GameObject>();
    }


    void Update()
    {
        radius = 5f;
        distance = Vector3.Distance(transform.position, playerPos.transform.position);

        if (playerAttackDetected && !actionPhase)
        {
            actionPhase = true;
            playerAttackDetected = false;
            enableMove = false;
            UnityEngine.Debug.Log(actionPhase);
            AttackDetected(difficulty);
            
        }
        
        if (enableMove)
        {
            //transform.LookAt(playerPos.transform.position);
            agent.isStopped = false;
            //DefaultMovement();
        }
        else
        {
            agent.isStopped = true;
        }

    }

    void AttackDetected(string difficulty)
    {
        switch (difficulty)
        {
            case "easy":
                /*if(!playerDetected) decision = UnityEngine.Random.Range(1, 6);
                else decision = UnityEngine.Random.Range(5, 6);*/

                decision = UnityEngine.Random.Range(5, 6);

                dashSpeed = 100f; dashTime = 0.5f;
                
                switch (decision)
                {
                    case 1: case 2: case 3: case 4:
                        UnityEngine.Debug.Log("runs towards the player");
                        //healthBar.TakeDamage(10); //

                        //StartCoroutine(LungeAttack());
                        
                        //DefaultMovement();
                        StartCoroutine(tempCooldown(1));
                        //DefaultMovement();

                        
                        break;

                    // case 4:
                    //     UnityEngine.Debug.Log("Retreat");
                    //     actionPhase = true;
                    //     StartCoroutine(tempCooldown(3));
                    // break;

                    case 5:
                        UnityEngine.Debug.Log("dashes towards the player");
                        //healthBar.TakeDamage(10); //

                        StartCoroutine(LungeAttack());
                    break;
                }

            break;
            
        }
        decision = 0;
    }

    void DefaultMovement()
    {
        transform.LookAt(playerPos.transform.position);
        Vector3 newPosition = playerPos.transform.position;
        agent.SetDestination(newPosition);

    }

    void NormalAttack()
    {
        agent.destination = target.position;
    }

    void Retreat(){}

    void Evade() { }

    private IEnumerator LungeAttack()
    {
        yield return new WaitForSeconds(1f);
        transform.LookAt(playerPos.transform.position);
        yield return new WaitForSeconds(0.5f);

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

        yield return StartCoroutine(tempCooldown(1));
    }

    private IEnumerator tempCooldown(float duration)
    {
        UnityEngine.Debug.Log("in cooldown");
        yield return new WaitForSeconds(duration);
        actionPhase = false;
        enableMove = true;

        //UnityEngine.Debug.Log("start count to 5 seconds");
        //yield return new WaitForSeconds(5f);
        //UnityEngine.Debug.Log("moving after 5 seconds");
        //DefaultMovement();
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