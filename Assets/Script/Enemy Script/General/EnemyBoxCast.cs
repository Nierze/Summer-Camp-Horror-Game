using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyBoxCast : MonoBehaviour
{
    public TiyanakAttackPattern playerDetector;
    public PugotAttackPattern playerDetector2;
    //public EnemyAttackPattern playerDetector;

    void OnDrawGizmos()
    {
        float maxDistance = 100f;
        Vector3 boxSize = new Vector3(3.0f, 3.0f, 3.0f);
        RaycastHit hit;

        bool isHit = Physics.BoxCast(transform.position, boxSize, transform.forward, out hit, transform.rotation, maxDistance);
        if (isHit)
        {
            GameObject hitGameObject = hit.transform.gameObject;
            if (hitGameObject.CompareTag("Player"))
            {
                UnityEngine.Debug.Log("player detected");
                playerDetector.playerDetected = true;
                playerDetector2.playerDetected = true;
                Gizmos.color = Color.red;
                Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
                Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, boxSize);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
            }
        }
        else
        {
            playerDetector.playerDetected = false;
            playerDetector2.playerDetected = false;
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
        }
    }
}
