using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class RayCast : MonoBehaviour
{
    public bool canTake;
    public bool canHold;
    public bool slotFull;

    public RaycastHit hit;

    public GameObject targetToTake;
    public GameObject targetToHold;

    public Canvas holdItem;
    public Text holdItemText;
    public Canvas useItem;
    public Text useItemText;


    //public GameObject target;
    //public GameObject targetOnHold;
    public int defaultLayer; // Store the default layer of the held item

    public ObjectInteract objectInteract;

    public Vector3 raycastDirection;
    float raycastDistance = 10f;

    public ObjectInspection objectInspection;
    void Start()
    {
        canTake = true;
        canHold = false;
        slotFull = false;

        holdItem.enabled = false;
        useItem.enabled = false;

        defaultLayer = gameObject.layer; // Get the default layer on start
    }

    void Update()
    {
        RaycastHit hit;
        UnityEngine.Debug.DrawRay(transform.position, raycastDirection * raycastDistance, Color.green);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            if (hit.transform.CompareTag("CanTake"))
            {
                useItem.enabled = true;
                useItemText.text = hit.transform.name;


                targetToTake = hit.transform.gameObject;
                canTake = true;
            }
            else if (hit.transform.CompareTag("CanHold") && !slotFull)
            {
                holdItem.enabled = true;
                holdItemText.text = hit.transform.name;


                targetToHold = hit.transform.gameObject;
                canHold = true;
            }
            else if (hit.transform.CompareTag("CanObserve") && !slotFull)
            {

                    if (Input.GetKeyDown(KeyCode.E))
                {
                    objectInteract.isExamining = true;

                    if (objectInteract.isExamining)
                    {
                        objectInteract.examinedObject = hit.transform;
                        objectInteract.originalPositions[objectInteract.examinedObject] = objectInteract.examinedObject.position;
                        objectInteract.originalRotations[objectInteract.examinedObject] = objectInteract.examinedObject.rotation;
                    }
                }

            }
            else
            {
                canHold = false;
                canTake = false;

                holdItem.enabled = false;
                useItem.enabled = false;


            }
        }
    }

}


