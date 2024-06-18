using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiyanakAttackPattern : MonoBehaviour
{
    public static bool playerAttackDetected = false;
    
    [SerializeField] public string difficulty = "easy";
    private int decision = 0;

    public Transform target;
    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerAttackDetected)
        {
            AttackDetected(difficulty);
            // UnityEngine.Debug.Log(playerAttackDetected);
            playerAttackDetected = false;
        }
        
    }

    void AttackDetected(string difficulty)
    {
        switch(difficulty)
        {
            case "easy":
                // UnityEngine.Debug.Log("difficulty is set to " + difficulty);
                decision = Random.Range(1, 6);
                // UnityEngine.Debug.Log("decision = " + decision);
                switch(decision)
                {
                    case 1: case 2: case 3:
                        UnityEngine.Debug.Log("Do nothing");
                    break;
                    
                    case 4: 
                        UnityEngine.Debug.Log("Normal Attack");
                        NormalAttack();
                    break;

                    case 5:
                        UnityEngine.Debug.Log("Retreat");
                    break;
                }

            break;

            case "normal":
                // UnityEngine.Debug.Log("difficulty is set to " + difficulty);
                decision = Random.Range(1, 8);
                // UnityEngine.Debug.Log("decision = " + decision);
                switch(decision)
                {
                    case 1: case 2:
                        UnityEngine.Debug.Log("Do nothing");
                    break;
                    
                    case 3: case 4: 
                        UnityEngine.Debug.Log("Normal Attack");
                        NormalAttack();
                    break;

                    case 5: case 6:
                        UnityEngine.Debug.Log("Lunge Attack");
                    break;

                    case 7:
                        UnityEngine.Debug.Log("Retreat");
                    break;
                }
            break;

            case "hard":
                // UnityEngine.Debug.Log("difficulty is set to " + difficulty);
                decision = Random.Range(1, 11);
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
        }

        decision = 0;
    }

    void NormalAttack()
    {
        agent.destination = target.position;
    }

    void LungeAttack(){}

    void Retreat(){}

    void Evade(){}
}
