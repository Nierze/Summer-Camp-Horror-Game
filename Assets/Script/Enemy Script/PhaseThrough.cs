using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseThrough : MonoBehaviour
{
    public GameObject player; 

    void Start()
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>());
    }

}