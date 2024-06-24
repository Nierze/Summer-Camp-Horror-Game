using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toberemoved : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("In range!");
        }
    }
}
