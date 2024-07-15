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

    [SerializeField] public string difficulty;
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
    public bool isThrow = false;

    //Camera Shake
    public CameraShake cameraShaker;

    //Sprint
    private bool isSprinting = false;
    public Vector3 sprintTargetPosition;
    [SerializeField] public float additionalDistance;
    public bool inSprintAttack = false;

    //Deal Damage
    public bool playerInRange = false;
    //public EaseHealthBar healthBar;

    //Devour Heal
    public EntityStats devourHeal;
    public List<GameObject> trees;
    private GameObject nearestTree;
    public GameObject targetTree;
    private float nearestDistance = Mathf.Infinity;
    public bool isDevourTree = false;

    //HP
    //public EnemyHealth enemyHp;
    public float timer = 0f;

    //Anim
    public Animator anim;
    public double th;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        trees = new List<GameObject>(GameObject.FindGameObjectsWithTag("Tree"));

        foreach (GameObject tree in trees)
        {
            UnityEngine.Debug.Log("tree = " + tree);
        }

        devourHeal = GetComponent<EntityStats>();

        difficulty = DifficultySelector.setDifficulty;
        if (difficulty == null) difficulty = "easy";

        th = (devourHeal.MaxHP - (devourHeal.MaxHP * 0.3));
        //UnityEngine.Debug.Log("mhp = " + devourHeal.MaxHP);
        //UnityEngine.Debug.Log("th = " + th);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Run", true);
        if (playerAttackDetected && !actionPhase)
        {
            actionPhase = true;
            playerAttackDetected = false;
            enableMove = false;
            AttackDetected(difficulty);
        }

        if (!isSprinting && !isDevourTree && !isThrow)
        {
            if (enableMove)
            {
                agent.isStopped = false;
                DefaultMovement();
            }
            else
            {
                agent.isStopped = true;
                StartCoroutine(Cooldown(1f));
            }
        }

        if (isSprinting)
        {
            Sprint(sprintTargetPosition);
        }


        if (isDevourTree && actionPhase == true && trees.Count != 0)
        {
            MoveToTree(targetTree);
        }

    }

    void AttackDetected(string difficulty)
    {
        switch (difficulty)
        {
            case "easy":

                //decision = UnityEngine.Random.Range(4, 6);
                if (trees.Count != 0) decision = UnityEngine.Random.Range(1, 8);
                else
                {
                    decision = UnityEngine.Random.Range(1, 7);
                    UnityEngine.Debug.Log("No more trees.");
                }

                switch (decision)
                {
                    case 1:
                    case 2:
                    case 3:
                        UnityEngine.Debug.Log("Pugot: Ground Slam");
                        StartCoroutine(cameraShaker.Shake(1f, .3f));
                        StartCoroutine(Cooldown(3f));
                    break;

                    case 4:
                    case 5:
                        UnityEngine.Debug.Log("Pugot: Throw Skulls");
                        isThrow = true;
                        StartCoroutine(ThrowSkulls());
                    break;

                    case 6:
                        UnityEngine.Debug.Log("Pugot: Sprint");
                        transform.LookAt(target.position);
                        inSprintAttack = true;
                        isSprinting = true;
                        enableMove = false;
                        sprintTargetPosition = target.position + agent.transform.forward * additionalDistance;
                    break;

                    case 7:
                        UnityEngine.Debug.Log("curr hp = " + devourHeal.CurrentHP);
                        UnityEngine.Debug.Log("max hp = " + devourHeal.MaxHP);
                        UnityEngine.Debug.Log("th hp = " + th);
                        if (devourHeal.CurrentHP < th)
                        {
                            UnityEngine.Debug.Log("Pugot: Devour Tree");
                            targetTree = DevourTree();
                            isDevourTree = true;
                        }
                        else
                        {
                            StartCoroutine(ThrowSkulls());
                            UnityEngine.Debug.Log("Pugot: Throw Skulls (No more trees)");
                        }

                        /*UnityEngine.Debug.Log("Pugot: Devour Tree");
                        targetTree = DevourTree();
                        isDevourTree = true;
                        */
                    break;
                }
            break;

            case "normal":
                if (trees.Count != 0) decision = UnityEngine.Random.Range(1, 10);
                else
                {
                    decision = UnityEngine.Random.Range(1, 8);
                    UnityEngine.Debug.Log("No more trees.");
                }

                switch (decision)
                {
                    case 1:
                    case 2:
                    case 3:
                        UnityEngine.Debug.Log("Pugot: Ground Slam");
                        StartCoroutine(cameraShaker.Shake(1f, .3f));
                        StartCoroutine(Cooldown(3f));
                        break;

                    case 4:
                    case 5:
                        UnityEngine.Debug.Log("Pugot: Throw Skulls");
                        isThrow = true;
                        StartCoroutine(ThrowSkulls());
                        break;

                    case 6:
                    case 7:
                        UnityEngine.Debug.Log("Pugot: Sprint");
                        transform.LookAt(target.position);
                        inSprintAttack = true;
                        isSprinting = true;
                        enableMove = false;
                        sprintTargetPosition = target.position + agent.transform.forward * additionalDistance;
                    break;

                    case 8:
                    case 9:
                        UnityEngine.Debug.Log("curr hp = " + devourHeal.CurrentHP);
                        UnityEngine.Debug.Log("max hp = " + devourHeal.MaxHP);
                        UnityEngine.Debug.Log("th hp = " + th);

                        if (devourHeal.CurrentHP < th)
                        {
                            UnityEngine.Debug.Log("Pugot: Devour Tree");
                            targetTree = DevourTree();
                            isDevourTree = true;
                        }
                        else
                        {
                            StartCoroutine(ThrowSkulls());
                            UnityEngine.Debug.Log("Pugot: Throw Skulls (No more trees)");
                        }
                        break;
                }
                break;

            case "hard":
                if (trees.Count != 0) decision = UnityEngine.Random.Range(1, 11);
                else
                {
                    decision = UnityEngine.Random.Range(1, 7);
                    UnityEngine.Debug.Log("No more trees.");
                }

                switch (decision)
                {
                    case 1:
                    case 2:
                        UnityEngine.Debug.Log("Pugot: Ground Slam");
                        anim.SetBool("Run", false);
                        StartCoroutine(cameraShaker.Shake(1f, .3f));
                        StartCoroutine(Cooldown(3f));
                        break;

                    case 3:
                    case 4:
                        UnityEngine.Debug.Log("Pugot: Throw Skulls");
                        isThrow = true;
                        anim.SetBool("Run", false);
                        StartCoroutine(ThrowSkulls());
                        break;

                    case 5:
                    case 6:
                        UnityEngine.Debug.Log("Pugot: Sprint");
                        anim.SetBool("Run", false);
                        transform.LookAt(target.position);
                        inSprintAttack = true;
                        isSprinting = true;
                        enableMove = false;
                        sprintTargetPosition = target.position + agent.transform.forward * additionalDistance;
                        break;

                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        anim.SetBool("Run", false);
                        UnityEngine.Debug.Log("curr hp = " + devourHeal.CurrentHP);
                        UnityEngine.Debug.Log("max hp = " + devourHeal.MaxHP);
                        UnityEngine.Debug.Log("th hp = " + th);
                        if (devourHeal.CurrentHP < th)
                        {
                            UnityEngine.Debug.Log("Pugot: Devour Tree");
                            targetTree = DevourTree();
                            isDevourTree = true;
                        }
                        else
                        {
                            /*StartCoroutine(ThrowSkulls());
                            UnityEngine.Debug.Log("Pugot: Throw Skulls (No more trees)");*/

                            UnityEngine.Debug.Log("Pugot: Sprint");
                            transform.LookAt(target.position);
                            inSprintAttack = true;
                            isSprinting = true;
                            enableMove = false;
                            sprintTargetPosition = target.position + agent.transform.forward * additionalDistance;
                        }
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

        StartCoroutine(WaitForDestination(targetPosition));
    }

    IEnumerator WaitForDestination(Vector3 targetPosition)
    {
        while (!agentReachedDestination(targetPosition))
        {
            yield return null;
        }

        if (isDevourTree)
        {
            devourHeal.EnemyHeal();
            isDevourTree = false;
            targetTree.SetActive(false);
            trees.Remove(targetTree);
            nearestDistance = Mathf.Infinity;
            UnityEngine.Debug.Log("Tree reached");
            DefaultMovement();
            yield return StartCoroutine(Cooldown(3f));
        }
        else if(isSprinting)
        {
            DefaultMovement();
            UnityEngine.Debug.Log("Sprint Reached");
            yield return StartCoroutine(Cooldown(3f));
        }
    }

    void MoveToTree(GameObject targetTreePos)
    {
        agent.SetDestination(targetTreePos.transform.position);

        StartCoroutine(WaitForDestination(targetTreePos.transform.position));
    }

    bool agentReachedDestination(Vector3 targetPosition)
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }

    GameObject DevourTree()
    {
        GameObject nearestTree = null;
        float nearestDistance = Mathf.Infinity;

        if (trees.Count == 0)
        {
            return null;
        }

        foreach (GameObject tree in trees)
        {
            float distance = Vector3.Distance(transform.position, tree.transform.position);

            if (distance < nearestDistance)
            {
                nearestTree = tree; 
                nearestDistance = distance;
            }
        }

        return nearestTree;
    }

    private IEnumerator ThrowSkulls()
    {
        anim.SetBool("Run", false);
        yield return new WaitForSeconds(0.5f);
        transform.LookAt(target.transform.position);
        yield return new WaitForSeconds(0.1f);

        GameObject skullObject = Instantiate(skullProjectile, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Destroy(skullObject, 5f);
        yield return StartCoroutine(Cooldown(2f));
    }

    private IEnumerator Cooldown(float duration)
    {
        UnityEngine.Debug.Log("Next action in cooldown for " + duration + " seconds.");
        yield return new WaitForSeconds(duration);
        nearestDistance = Mathf.Infinity;
        agent.speed = 15f;
        isSprinting = false;
        actionPhase = false;
        enableMove = true;
        isDevourTree = false;
        isThrow = false;
        anim.SetBool("Run", true);

    }
}