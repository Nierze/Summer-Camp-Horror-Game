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

        if (gameObject.transform.parent.gameObject == holdArea.gameObject)
        {
            UnityEngine.Debug.Log(gameObject.name + " with parent/in Hold Area");
            isHold = true;
        }
    }

    void Update()
    {
        //working 1
        //if (Input.GetKeyDown(KeyCode.E) && setEnum.enablePickUp && !inAction && !isHold && holdArea.childCount < 1)
        if(Input.GetKeyDown(KeyCode.E) && !inAction && holdArea.childCount < 1 && !isHold && setEnum.enablePickUp)
        {
            inAction = true;
            //StartCoroutine(Pick());
            Pick();
        }

        else if (Input.GetKeyDown(KeyCode.E) && !inAction && holdArea.childCount == 1 && isHold)
        {
            inAction = true;
            //StartCoroutine(StoreToInventory());
            StoreToInventory();
        }

        //if (Input.GetKeyDown(KeyCode.G) && !inAction && isHold && holdArea.childCount == 1)
        if (Input.GetKeyDown(KeyCode.G) && !inAction && holdArea.childCount == 1 && isHold)
        {
            inAction = true;
            //StartCoroutine(Throw());
            Throw();
        }
    }

    //private IEnumerator Pick()
    void Pick()
    {
        setEnum.enablePickUp = false;
        rb.isKinematic = true;
        gameObject.transform.position = new Vector3(holdArea.transform.position.x, holdArea.transform.position.y, holdArea.transform.position.z);
        gameObject.transform.SetParent(holdArea);
        isHold = true;
        //yield return StartCoroutine(Wait());
        setEnum.enablePickUp = false;
        inAction = false;
        //StartCoroutine(Wait());
    }

    //private IEnumerator Throw()
    void Throw()
    {
        setEnum.enablePickUp = false;
        rb.isKinematic = false;
        isHold = false;
        gameObject.transform.SetParent(null);
        rb.AddForce(holdArea.transform.forward * 3f, ForceMode.Impulse);
        inAction = false;
        //yield return null;
        //StartCoroutine(Wait());
    }

    //private IEnumerator StoreToInventory()
    void StoreToInventory()
    {
        inventory.SetActive(true);
        setEnum.enablePickUp = false;
        StoreToInventory storeItem = GetComponent<StoreToInventory>();
        storeItem.StoreToInven();
        gameObject.SetActive(false);
        inAction = false;
        //storeItem.OnPickedUp();
        //yield return null;
        //StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        inAction = false;
    }
}
