using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiktikAttackPattern : MonoBehaviour
{
    [SerializeField] public string difficulty;
    private int decision = 0;

    public bool playerAttackDetected = false;
    public bool playerDetected = false;

    public GameObject player;

    public UnityEngine.AI.NavMeshAgent agent;
    public Transform target;
    public bool actionPhase = false;
    public bool enableMove = true;

    public Rigidbody rb;

    //Tounge Hook
    public GameObject toungeHookSpawnpoint;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        difficulty = DifficultySelector.setDifficulty;
        if (difficulty == null) difficulty = "easy";
    }

    void Update()
    {
        //DefaultMovement();
        if (playerAttackDetected && !actionPhase)
        {
            actionPhase = true;
            playerAttackDetected = false;
            enableMove = false;
            AttackDetected(difficulty);
        }
    }

    void AttackDetected(string difficulty)
    {
        switch (difficulty)
        {
            case "easy":
                decision = UnityEngine.Random.Range(1, 2);

                switch (decision)
                {
                    case 1:
                        ToungeWhip();
                    break;

                    case 2:
                        ToungeHook();
                    break;

                    case 3:

                    break;
                }

            break;
        }
    }

    void ToungeWhip()
    {
        UnityEngine.Debug.Log("Tiktik: Tounge Whip");
        StartCoroutine(Cooldown(3f));
    }

    void ToungeHook()
    {
        UnityEngine.Debug.Log("Tiktik: Tounge Hook");
        StartCoroutine(Cooldown(3f));

    }

    void DefaultMovement()
    {
        Vector3 newPosition = target.position;
        agent.SetDestination(newPosition);
    }

    IEnumerator Cooldown(float duration)
    {
        UnityEngine.Debug.Log("Next action in cooldown for " + duration + " seconds.");
        yield return new WaitForSeconds(duration);
        actionPhase = false;
        enableMove = true;
    }

}
