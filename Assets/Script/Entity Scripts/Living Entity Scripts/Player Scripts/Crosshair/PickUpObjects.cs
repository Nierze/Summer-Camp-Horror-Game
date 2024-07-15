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

    private GameObject holdObject;

    public bool slotFull;
    private bool currentlyHolding;

    // Healthbar
    public EaseHealthBar easeHealthBar;
    // All Items
    public AllItems allItems;

    public SoundEffectObject soundEffectObject;

    private void Start()
    {
        slotFull = false;
        currentlyHolding = false;
    }

    void Update()
    {
        if (rayCastScript != null)
        {
            Debug.Log("hit");
            // Check for equipping items
            if (rayCastScript.canTake && Input.GetKeyDown(KeyCode.E))
            {
                EquipItem();
            }
            
            // Check for equipping items and is currently holding an item
            if (rayCastScript.canTake && Input.GetKeyDown(KeyCode.E) && currentlyHolding == true)
            {
                Debug.Log("Take while Holding an Item");
                holdObject = rayCastScript.targetToHold;
                MoveHeldObject();
            }

            // Check for holding items
            if (rayCastScript.canHold && Input.GetKeyDown(KeyCode.E) && !slotFull && !currentlyHolding)
            {
                currentlyHolding = true;
                HoldObject();
            }

            // Check for dropping the held item
            if (Input.GetKeyDown(KeyCode.F) && slotFull)
            {
                slotFull = false;
                Drop();
            }

            // Keep the held object in place
            if (holdObject != null)
            {
                HoldObject();
                MoveHeldObject();
            }
        }
    }

    void EquipItem()
    {
        if (rayCastScript.targetToTake.name == "Battery")
        {
            flashLightScript.AddBattery(25f);
            batteryItem.OnPickedUp();
            Debug.Log("Picked");
            soundEffectObject.PlaySound("Equip");
        }

        if (rayCastScript.targetToTake.name == "Medkit")
        {
            easeHealthBar.Heal(100);
            easeHealthBar.OnPickedUp("Medkit");
            soundEffectObject.PlaySound("Medkit"); // Play Medkit sound
        }

        if (rayCastScript.targetToTake.name == "Bandage")
        {
            easeHealthBar.Heal(25);
            easeHealthBar.OnPickedUp("Bandage");
            soundEffectObject.PlaySound("Equip");
        }
    }

    void HoldObject()
    {
        slotFull = true;
        rayCastScript.slotFull = true;
        if (rayCastScript.targetToHold != null && rayCastScript.canHold)
        {
            holdObject = rayCastScript.targetToHold;
            holdObject.transform.position = holdArea.transform.position;
            holdObject.transform.rotation = holdArea.transform.rotation;
            holdObject.transform.parent = holdArea.transform;

            var rb = holdObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            soundEffectObject.PlaySound(holdObject.transform.name);
            Debug.Log("Sound Effect: " + holdObject.transform.name);
        }
    }

    void MoveHeldObject()
    {
        holdObject.transform.position = holdArea.transform.position;
        holdObject.transform.rotation = holdArea.transform.rotation;
    }

    public void Drop()
    {
        if (holdObject != null)
        {
            holdObject.transform.parent = allItems.transform;
            rayCastScript.targetToHold = null;

            allItems.LayerDefault();
            var rb = holdObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Re-enable physics
                float throwForce = 10f;
                Vector3 throwDirection = holdArea.transform.forward;
                rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
            }
            holdObject = null;
        }
        slotFull = false;
        rayCastScript.slotFull = false;
        currentlyHolding = false;
    }
}
