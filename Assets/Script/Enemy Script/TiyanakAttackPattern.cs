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
    private UnityEngine.AI.NavMeshAgent agent;

    public bool actionPhase = false;

    //temp lunge
    public Rigidbody rb;
    public bool isLunging = false;
    public Vector3 lungeTarget;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if(playerAttackDetected && !actionPhase)
        {
            AttackDetected(difficulty);
            actionPhase = true;
            UnityEngine.Debug.Log(actionPhase);
            playerAttackDetected = false;
        }

        //if (isLunging) { LungeAttack(); StartCoroutine(EndLunge());  }

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
                    case 1: case 2: case 3:
                        actionPhase = true;
                        UnityEngine.Debug.Log("Do nothing"); //default movement
                        StartCoroutine(tempCooldown());
                    break;
                    
                    case 4: 
                        UnityEngine.Debug.Log("Normal Attack");
                        actionPhase = true;
                        NormalAttack();
                        StartCoroutine(tempCooldown());
                    break;

                    case 5:
                        UnityEngine.Debug.Log("Retreat");
                        actionPhase = true;
                        StartCoroutine(tempCooldown());
                    break;
                }

            break;

            
        }

        decision = 0;
    }

    void DefaultMovement()
    {

    }

    void NormalAttack()
    {
        agent.destination = target.position;
    }

    void LungeAttack()
    {
        transform.position = Vector3.Lerp(transform.position, lungeTarget, Time.deltaTime);
    }

    void Retreat(){}

    void Evade() { }

    private IEnumerator EndLunge()
    {
        yield return new WaitForSeconds(2f);
        isLunging = false;
    }

    private IEnumerator tempCooldown()
    {
        yield return new WaitForSeconds(3f);
        actionPhase = false;
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