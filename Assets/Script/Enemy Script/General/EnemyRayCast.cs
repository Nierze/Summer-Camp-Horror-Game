using UnityEngine;

public class EnemyRayCast : MonoBehaviour
{
    //Ray
    private Ray enemyRay;
    private RaycastHit enemyRayHit;
    private float interactionRayLength = 50.0f;

    //not yet used
    public GameObject thisEnemy;

    void Start()
    {
        thisEnemy = GetComponent<GameObject>();
    }

    
    void Update()
    {
        EnemyInteractRaycast();
    }

    void EnemyInteractRaycast()
    {
        enemyRay = new Ray(transform.position, transform.forward);
        Debug.DrawRay(enemyRay.origin, enemyRay.direction * interactionRayLength, Color.red);

        if (Physics.Raycast(enemyRay, out enemyRayHit, interactionRayLength))
        {
            GameObject hitGameObject = enemyRayHit.transform.gameObject;
            if (hitGameObject.CompareTag("Player"))
            {
                
            }
        }

    }
}
