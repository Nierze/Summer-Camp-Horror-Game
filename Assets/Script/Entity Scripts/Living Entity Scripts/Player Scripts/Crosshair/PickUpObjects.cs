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



    //Healthbar
    public EaseHealthBar easeHealthBar;

    //All Items
    public AllItems allItems;
    private void Start()
    {
        equipped = false;
        slotFull = false;
    }
    void Update()
    {
        //Check for Holding Item
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("Slot Full" + slotFull);
        }


        if (rayCastScript != null)
        {
            // Check for equipping items
            if (rayCastScript.canEquip && Input.GetKeyDown(KeyCode.F) && rayCastScript.canHold == false)
            {
                EquipItem();
            }

            // Check for equipping items and is currently holding an item
            if (rayCastScript.canEquip && Input.GetKeyDown(KeyCode.F) && rayCastScript.currentlyHolding == true)
            {
                heldObject = rayCastScript.targetOnHold;
                MoveHeldObject();
            }
            // Check for holding items
            if (rayCastScript.canHold && Input.GetKeyDown(KeyCode.E) && !slotFull && rayCastScript.currentlyHolding == false)
            {
                Debug.Log("Hold");
                rayCastScript.currentlyHolding = true;
                HoldItem();
            }

            // Check for dropping the held item
            if (Input.GetKeyDown(KeyCode.G) && slotFull)
            {
                slotFull = false;
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
        if (rayCastScript.target.name == "Sample Battery")
        {
            flashLightScript.AddBattery(25f);
            batteryItem.OnPickedUp();
        }

        if (rayCastScript.target.name ==  "Medkit")
        {
            easeHealthBar.Heal(100);
            easeHealthBar.OnPickedUp("Medkit");
        }

        if (rayCastScript.target.name == "Bandage")
        {
            easeHealthBar.Heal(25);
            easeHealthBar.OnPickedUp("Bandage");
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

            Debug.Log("held Object" + heldObject.name);

            //if (heldObject.name == "Shovel")
            //{
            //    holdArea.transform.rotation = Quaternion.Euler(-19.9f, -11.4f, 28.1f);
            //}
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
            //holdArea.transform.position = new Vector3(1.039999f, -0.5999999f, 4.200001f);
            //holdArea.transform.rotation = Quaternion.Euler(0, -0, 0);


            heldObject.transform.parent = allItems.transform;
            allItems.LayerDefault();
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
