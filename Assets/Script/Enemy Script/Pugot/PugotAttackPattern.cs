using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class PugotAttackPattern : MonoBehaviour
{
    public bool playerAttackDetected = false;
    public bool playerDetected = false;

    [SerializeField] public string difficulty = "easy";
    private int decision = 0;

    public GameObject player;

    public UnityEngine.AI.NavMeshAgent agent;
    public Transform target;
    public bool actionPhase = false;
    public bool enableMove = true;

    public Rigidbody rb;

    //Skull Projectile
    public GameObject skullProjectile;
    public Transform spawnPoint;
    public float projectileSpeed;

    //Camera Shake
    public CameraShake cameraShaker;

    //Sprint
    private bool isSprinting = false;
    public Vector3 sprintTargetPosition;
    [SerializeField] public float additionalDistance;

    //Deal Damage
    public bool playerInRange = false;
    public EaseHealthBar healthBar;

    //Devour Heal
    public EnemyHealth devourHeal;
    public GameObject[] trees;
    private GameObject nearestTree;
    public Vector3 targetTree;
    private float nearestDistance = Mathf.Infinity;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        trees = GameObject.FindGameObjectsWithTag("Tree");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAttackDetected && !actionPhase)
        {
            actionPhase = true;
            playerAttackDetected = false;
            enableMove = false;
            AttackDetected(difficulty);
        }

        /*if (enableMove)
        {
            agent.isStopped = false;
            DefaultMovement();
        }
        else
        {
            agent.isStopped = true;
        }*/

        if (isSprinting)
        {
            Sprint(sprintTargetPosition);
        }

    }

    void AttackDetected(string difficulty)
    {
        switch (difficulty)
        {
            case "easy":

                //decision = UnityEngine.Random.Range(3, 8);
                decision = UnityEngine.Random.Range(7, 8);

                switch (decision)
                {
                    case 1: case 2: case 3:
                        UnityEngine.Debug.Log("Pugot: Ground Slam");
                        StartCoroutine(cameraShaker.Shake(1f, .3f));
                        StartCoroutine(Cooldown(3f));
                        //DefaultMovement();
                        break;

                    case 4: case 5:
                        UnityEngine.Debug.Log("Pugot: Throw Skulls");

                        StartCoroutine(ThrowSkulls());
                    break;

                    case 6:
                        UnityEngine.Debug.Log("Pugot: Sprint");
                        transform.LookAt(target.position);
                        isSprinting = true;
                        sprintTargetPosition = target.position + agent.transform.forward * additionalDistance;
                        StartCoroutine(Cooldown(3f));
                        //DefaultMovement();
                    break;

                    case 7:
                        UnityEngine.Debug.Log("Pugot: Devour Tree");

                        targetTree = DevourTree();
                        UnityEngine.Debug.Log(targetTree);
                        StartCoroutine(Cooldown(3f));
                        //DefaultMovement();
                    break;
                }
            break;
        }
    }

    void DefaultMovement()
    {
        Vector3 newPosition = target.position;
        agent.SetDestination(newPosition);
    }

    void Sprint(Vector3 targetPosition)
    {
        agent.speed = 200;
        agent.SetDestination(targetPosition);
        
    }

    Vector3 DevourTree()
    {
        foreach(GameObject tree in trees)
        {
            float distance = Vector3.Distance(transform.position, tree.transform.position);

            if (distance < nearestDistance)
            {
                nearestTree = tree; 
                nearestDistance = distance;
            }
        }

        return nearestTree.transform.position;
    }

    /*void ThrowSkulls()
    {
        transform.LookAt(target.transform.position);
        GameObject skullObject = Instantiate(skullProjectile, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Destroy(skullObject, 5f);
    }*/

    /*private IEnumerator Sprint()
    {
        
    }*/

    private IEnumerator ThrowSkulls()
    {
        yield return new WaitForSeconds(0.5f);
        transform.LookAt(target.transform.position);
        yield return new WaitForSeconds(0.1f);

        GameObject skullObject = Instantiate(skullProjectile, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Destroy(skullObject, 5f);
        yield return StartCoroutine(Cooldown(2f));
    }

    private IEnumerator Cooldown(float duration)
    {
        UnityEngine.Debug.Log("Next actoin in cooldown for " + duration + " seconds.");
        yield return new WaitForSeconds(duration);
        agent.speed = 15f;
        isSprinting = false;
        actionPhase = false;
        enableMove = true;
    }
}
