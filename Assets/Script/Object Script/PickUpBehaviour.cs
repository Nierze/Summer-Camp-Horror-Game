using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehaviour : MonoBehaviour
{
    public string setObjectInteraction;
    public SetObjectEnum setEnum;
    GameObject holdObject;
    Transform holdArea;
    Rigidbody rb;

    public bool isHold = false;
    public bool inAction = false;

    public GameObject inventory;
    public FixedInventoryManager inventoryManager;

    void Start()
    {
        setObjectInteraction = ObjectInteractEnum.objectInteraction.pickUp.ToString();
        setEnum = GetComponent<SetObjectEnum>();
        setEnum.objectInteraction = setObjectInteraction;
        holdObject = GameObject.Find("Hold Area");
        holdArea = holdObject.GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        //FixedInventoryManager invManager = GameObject.Find("kfw - InventoryManager").GetComponent<FixedInventoryManager>();
        //inventory = invManager.inventoryTab;

        inventory = GameObject.Find("kfw - Inventory");
        inventoryManager = GameObject.Find("kfw - InventoryManager").GetComponent<FixedInventoryManager>();

        if (gameObject.transform.parent.gameObject == holdArea.gameObject)
        {
            //UnityEngine.Debug.Log(gameObject.name + " with parent/in Hold Area");
            isHold = true;
        }
    }

    void Update()
    {
        //working 1
        //if (Input.GetKeyDown(KeyCode.E) && setEnum.enablePickUp && !inAction && !isHold && holdArea.childCount < 1)
        inventoryManager.CheckMaxInventory();
        if (!inventoryManager.maxInventory)
        {
            if (Input.GetKeyDown(KeyCode.E) && !inAction && holdArea.childCount < 1 && !isHold && setEnum.enablePickUp)
            {
                inAction = true;
                Pick();
                //inventoryManager.CheckMaxInventory();
                //if (!inventoryManager.maxInventory) Pick();
                //else UnityEngine.Debug.Log("Pick Item Inven Update: cannot pick, max inventory");
            }

            else if (Input.GetKeyDown(KeyCode.E) && !inAction && holdArea.childCount == 1 && isHold)
            {
                inAction = true;
                StoreToInventory();
                //inventoryManager.CheckMaxInventory();
                //if(!inventoryManager.maxInventory) StoreToInventory();
                //else UnityEngine.Debug.Log("StoreToInventory to Inven Update: cannot store, max inventory");
            }
        }
        

        //if (Input.GetKeyDown(KeyCode.G) && !inAction && isHold && holdArea.childCount == 1)
        if ((Input.GetKeyDown(KeyCode.G) && holdArea.childCount == 1 ) || inventoryManager.maxInventory) //&& !inAction && holdArea.childCount == 1 && isHold
        {
            inAction = true;
            Throw();
        }
    }

    void Pick()
    {
        setEnum.enablePickUp = false;

        rb.isKinematic = true;
        gameObject.transform.position = new Vector3(holdArea.transform.position.x, holdArea.transform.position.y, holdArea.transform.position.z);
        gameObject.transform.SetParent(holdArea);
        isHold = true;

        setEnum.enablePickUp = false;
        inAction = false;
    }

    void StoreToInventory()
    {
        inventoryManager.InventoryTabManager(true);

        setEnum.enablePickUp = false;

        inventoryManager.GetNearestEmpty();

        StoreToInventory storeItem = GetComponent<StoreToInventory>();
        storeItem.StoreToInven();

        inAction = false;
        gameObject.SetActive(false);
    }

    void Throw()
    {
        setEnum.enablePickUp = false;
        rb.isKinematic = false;
        isHold = false;
        gameObject.transform.SetParent(null);
        rb.AddForce(holdArea.transform.forward * 3f, ForceMode.Impulse);
        
        inAction = false;
    }

}
