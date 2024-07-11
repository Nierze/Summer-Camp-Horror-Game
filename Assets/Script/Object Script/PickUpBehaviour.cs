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

    void Start()
    {
        setObjectInteraction = ObjectInteractEnum.objectInteraction.pickUp.ToString();
        setEnum = GetComponent<SetObjectEnum>();
        setEnum.objectInteraction = setObjectInteraction;
        holdObject = GameObject.Find("Hold Area");
        holdArea = holdObject.GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        if (gameObject.transform.parent.gameObject == holdArea.gameObject)
        {
            UnityEngine.Debug.Log("with parent");
            isHold = true;
        }
    }

    void Update()
    {
        //working 1
        if (Input.GetKeyDown(KeyCode.E) && setEnum.enablePickUp && !inAction && !isHold && holdArea.childCount < 1)
        {
            inAction = true;
            StartCoroutine(Pick());
        }

        else if (Input.GetKeyDown(KeyCode.E) && isHold)
        {
            inAction = true;
            StartCoroutine(StoreToInventory());
        }

        if (Input.GetKeyDown(KeyCode.G) && !inAction && isHold && holdArea.childCount == 1)
        {
            inAction = true;
            StartCoroutine(Throw());
        }
    }

    private IEnumerator Pick()
    {
        //setEnum.enablePickUp = false;
        rb.isKinematic = true;
        gameObject.transform.position = new Vector3(holdArea.transform.position.x, holdArea.transform.position.y, holdArea.transform.position.z);
        gameObject.transform.SetParent(holdArea);
        isHold = true;
        //yield return StartCoroutine(Wait());
        setEnum.enablePickUp = false;
        yield return StartCoroutine(Wait());
    }

    private IEnumerator Throw()
    {
        rb.isKinematic = false;
        isHold = false;
        gameObject.transform.SetParent(null);
        rb.AddForce(holdArea.transform.forward * 3f, ForceMode.Impulse);
        yield return StartCoroutine(Wait());
    }

    private IEnumerator StoreToInventory()
    {
        setEnum.enablePickUp = false;
        StoreToInventory storeItem = GetComponent<StoreToInventory>();
        storeItem.OnPickedUp();
        yield return StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        inAction = false;
    }
}
