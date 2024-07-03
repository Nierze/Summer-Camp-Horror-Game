using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    public RayCast rayCastScript;
    public FlashLight flashLightScript;
    public GameObject holdArea;
    public BatteryItem batteryItem;

    private GameObject heldObject;

    public bool equipped;
    public static bool slotFull;

    void Update()
    {
        Debug.Log("Slot Full: " + slotFull);
        Debug.Log("Currently Holding: " + heldObject);
        Debug.Log("Target On Hold :" + rayCastScript.targetOnHold.name);
        if (rayCastScript != null)
        {
            // Check for equipping items
            if (rayCastScript.canEquip && Input.GetKeyDown(KeyCode.F) && rayCastScript.canHold == false)
            {
                Debug.Log("Condition 2");
                EquipItem();
            }

            // Check for equipping items and is currently holding an item
            if (rayCastScript.canEquip && Input.GetKeyDown(KeyCode.F) && rayCastScript.currentlyHolding == true)
            {
                Debug.Log("Condition 3");
                heldObject = rayCastScript.targetOnHold;
                MoveHeldObject();
            }
            // Check for holding items
            if (rayCastScript.canHold && Input.GetKeyDown(KeyCode.E) && !slotFull && rayCastScript.currentlyHolding == false)
            {
                Debug.Log("Condition 4");
                rayCastScript.currentlyHolding = true;
                HoldItem();
            }

            // Check for dropping the held item
            if (Input.GetKeyDown(KeyCode.G) && slotFull)
            {
                Debug.Log("Slot Full: " + slotFull);
                slotFull = false;
                Debug.Log("Condition 5");
                Drop();
            }

            // Keep the held object in place
            if (heldObject != null)
            {
                Debug.Log("Condition 6");
                HoldItem();
                MoveHeldObject();
            }
        }
    }

    void EquipItem()
    {
        if (rayCastScript.target.name == "Sample Battery")
        {
            flashLightScript.AddBattery(25f);
            batteryItem.OnPickedUp();
        }
    }

    void HoldItem()
    {
        equipped = true;
        slotFull = true;
        HoldObject();
    }

    void HoldObject()
    {
        if (rayCastScript.targetOnHold != null && rayCastScript.canHold)
        {
            heldObject = rayCastScript.targetOnHold;
            heldObject.transform.position = holdArea.transform.position;
            heldObject.transform.rotation = holdArea.transform.rotation;
            heldObject.transform.parent = holdArea.transform;
            var rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true; 
            }
        }
    }

    void MoveHeldObject()
    {
        heldObject.transform.position = holdArea.transform.position;
        heldObject.transform.rotation = holdArea.transform.rotation;
    }

    public void Drop()
    {
        if (heldObject != null)
        {
            heldObject.transform.parent = null;
            var rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Re-enable physics
                float throwForce = 10f;
                Vector3 throwDirection = holdArea.transform.forward;
                rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
            }
            heldObject = null;
        }
        equipped = false;
        slotFull = false;
        rayCastScript.currentlyHolding = false;
    }
}
