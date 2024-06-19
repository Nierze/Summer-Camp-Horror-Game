using UnityEngine;

public class EnemyScannableInteraction : MonoBehaviour
{
    // Array of EnemyReveal components attached to objects tagged as "Scannable"
    public EnemyReveal[] reveal;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scannable"))
        {
            // Get the EnemyReveal component from the collider's GameObject
            EnemyReveal enemyRevealComponent = other.GetComponent<EnemyReveal>();

            // Check if the EnemyReveal component exists
            if (enemyRevealComponent != null)
            {
                // Call the EReveal method of the EnemyReveal component
                enemyRevealComponent.EReveal();
            }
        }
    }
}
