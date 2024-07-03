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
        if (rayCastScript != null)
        {
            // Debug the raycast target
            if (rayCastScript.targetOnHold != null)
            {
                Debug.Log(rayCastScript.targetOnHold.name);
            }

            // Check for equipping items
            if (rayCastScript.canEquip && Input.GetKeyDown(KeyCode.F))
            {
                EquipItem();
            }

            // Check for holding items
            if (rayCastScript.canHold && Input.GetKeyDown(KeyCode.E) && !slotFull)
            {
                HoldItem();
            }

            // Check for dropping the held item
            if (Input.GetKeyDown(KeyCode.G) && slotFull)
            {
                Drop();
            }

            // Keep the held object in place
            if (heldObject != null)
            {
                HoldItem();
                MoveHeldObject();
            }
        }
    }

    void EquipItem()
    {
        Debug.Log("Equip Item");
        if (rayCastScript.target.name == "Sample Battery")
        {
            flashLightScript.AddBattery(25f);
            batteryItem.OnPickedUp();
            Debug.Log("Battery Equipped");
        }
    }

    void HoldItem()
    {
        Debug.Log("Hold Item");
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
        else
        {
            Debug.Log("Not working");
        }
    }

    void MoveHeldObject()
    {
        heldObject.transform.position = holdArea.transform.position;
        heldObject.transform.rotation = holdArea.transform.rotation;
    }

    public void Drop()
    {
        Debug.Log("Drop");
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
    }
}
