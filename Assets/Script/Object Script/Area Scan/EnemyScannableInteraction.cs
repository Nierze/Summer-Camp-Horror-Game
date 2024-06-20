using UnityEngine;

public class EnemyScannableInteraction : MonoBehaviour
{
    public EnemyReveal[] reveal;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scannable"))
        {
            EnemyReveal enemyRevealComponent = other.GetComponent<EnemyReveal>();

            if (enemyRevealComponent != null)
            {
                enemyRevealComponent.EReveal();
            }
        }
    }
}
