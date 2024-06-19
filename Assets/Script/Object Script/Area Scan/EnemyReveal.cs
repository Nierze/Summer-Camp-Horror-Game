using System.Collections;
using UnityEngine;

public class EnemyReveal : MonoBehaviour
{
    public GameObject enemyAttributes;

    void Start()
    {
        // Ensure that enemyAttributes GameObject is initially inactive
        enemyAttributes.SetActive(false);
    }

    // Method to reveal enemy attributes
    public void EReveal()
    {
        // Start the coroutine to reveal and then hide enemy attributes
        StartCoroutine(HideEnemy());
    }

    // Coroutine to reveal and hide enemy attributes
    private IEnumerator HideEnemy()
    {
        // Activate enemy attributes
        enemyAttributes.SetActive(true);

        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Deactivate enemy attributes
        enemyAttributes.SetActive(false);
    }
}
