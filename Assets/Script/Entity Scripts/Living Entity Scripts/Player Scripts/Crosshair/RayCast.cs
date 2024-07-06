using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    public bool canTake;
    public bool canHold;
    public bool slotFull;

    public RaycastHit hit;

    public GameObject targetToTake;
    public GameObject targetToHold;

    //public GameObject target;
    //public GameObject targetOnHold;
    public int defaultLayer; // Store the default layer of the held item


    void Start()
    {
        canTake = true;
        canHold = false;
        slotFull = false;
        defaultLayer = gameObject.layer; // Get the default layer on start
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            if (hit.transform.CompareTag("CanTake"))
            {
                targetToTake = hit.transform.gameObject;
                canTake = true;
            }
            else if (hit.transform.CompareTag("CanHold") && !slotFull)
            {
                targetToHold = hit.transform.gameObject;
                canHold = true;
            }
            else
            {
                canHold = false;
                canTake = false;
            }
        }
    }
}
