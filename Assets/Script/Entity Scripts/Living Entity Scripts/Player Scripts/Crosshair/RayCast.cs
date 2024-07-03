using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    public bool canEquip;
    public bool canHold;
    public bool currentlyHolding;
    public RaycastHit hit;
    public GameObject target;
    public GameObject targetOnHold;

    void Start()
    {
        canEquip = false;
        canHold = false;
    }

    void Update()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 20))
        {
            if (hit.transform.CompareTag("CanTake"))
            {
                target = hit.transform.gameObject;
                canEquip = true;
                canHold = false;
            }
            else if (hit.transform.CompareTag("CanHold"))
            {
                target = hit.transform.gameObject;
                targetOnHold = target;
                    
                if (!currentlyHolding)
                {
                    canHold = true;
                    targetOnHold = hit.transform.gameObject;
                }
                else
                {
                    canHold = false;
                   
                }
                   
            }
            else
            {
                canEquip = false;
                canHold = false;
            }
        }
    }
}
