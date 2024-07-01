using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    // Start is called before the first frame update\
    public bool canEquip;
    public bool canHold;
    public RaycastHit hit;
    public GameObject target;
    void Start()
    {
        canEquip = false;
        canHold = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 20))
        {
            if (hit.transform.CompareTag("CanTake"))
            {
                target = hit.transform.gameObject;
                Debug.Log(hit.transform.name);
                canEquip = true;
                canHold = false;
            }

            else if (hit.transform.CompareTag("CanHold"))
            {
                Debug.Log(hit.transform.name);
                target = hit.transform.gameObject;
                Debug.Log(target.name);
                canHold = true;
                canEquip = false;
            }

            else
            {
                canEquip = false; 
                canHold = false;
            }
        }
    }
}
