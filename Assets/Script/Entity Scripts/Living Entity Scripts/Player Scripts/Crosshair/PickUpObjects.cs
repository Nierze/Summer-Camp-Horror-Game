using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    public RayCast rayCastScript;
    public FlashLight flashLightScript;
    //public PickupItems pickupItems;
    public GameObject holdArea;
    public BatteryItem batteryItem;

    private GameObject heldObject;


    public bool equipped;
    public static bool slotFull;
    void Update()
    {
        if (rayCastScript != null && rayCastScript.canEquip == true && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(rayCastScript.target.name);
            if (rayCastScript.target.name == "Sample Battery")
            {
                flashLightScript.AddBattery(25f);
                batteryItem.OnPickedUp();
                Debug.Log("Battery");
            }
        }

        if (rayCastScript != null && rayCastScript.canHold == true && Input.GetKeyDown(KeyCode.E) && slotFull == false)
        {
            equipped = true;
            slotFull = true;
            Debug.Log("hold me");
            HoldObject();
        }

        if (heldObject != null)
        {
            MoveHeldObject();
        }


        if (Input.GetKeyDown(KeyCode.G) && slotFull == true)
        {
            Debug.Log("Throw");
            Drop();
        }
    }

    void HoldObject()
    {
        // Check if there's an object to hold (from the raycast or other logic)
        if (rayCastScript != null && rayCastScript.target != null)
        {
            heldObject = rayCastScript.target;
            // Set the object's position and rotation to the hold area
            heldObject.transform.position = holdArea.transform.position;
            heldObject.transform.rotation = holdArea.transform.rotation;
            // Make the hold area the parent of the held object
            heldObject.transform.parent = holdArea.transform;
        }
    }

    void MoveHeldObject()
    {
        // Ensure the held object stays at the hold area position and rotation
        heldObject.transform.position = holdArea.transform.position;
        heldObject.transform.rotation = holdArea.transform.rotation;
    }


    void Drop()
    {
        if (heldObject != null)
        {
            // Remove the parent (hold area) so the object is no longer held
            heldObject.transform.parent = null;

            // Calculate throw force (you can adjust this value as needed)
            float throwForce = 10f;

            // Calculate throw direction based on hold area's forward direction
            Vector3 throwDirection = holdArea.transform.forward;

            // Add force to the rigidbody of the held object to simulate throwing
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Ensure the rigidbody is not kinematic so forces affect it
                rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
            }

            // Clear the heldObject reference
            heldObject = null;
        }

        // Reset slot states
        equipped = false;
        slotFull = false;
    }
}
