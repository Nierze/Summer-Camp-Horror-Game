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
    }

    void Update()
    {
        if (!isHold && !inAction)
        {
            if (Input.GetKeyDown(KeyCode.E) && setEnum.enablePickUp && !isHold)
            {
                inAction = true;
                StartCoroutine(Pick());
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                inAction = true;
                StartCoroutine(Throw());
            }
        }

    }

    private IEnumerator Pick()
    {
        setEnum.enablePickUp = false;
        rb.isKinematic = true;
        gameObject.transform.position = new Vector3(holdArea.transform.position.x, holdArea.transform.position.y, holdArea.transform.position.z);
        gameObject.transform.SetParent(holdArea);
        isHold = true;
        yield return StartCoroutine(Wait());
    }

    private IEnumerator Throw()
    {
        rb.isKinematic = false;
        gameObject.transform.SetParent(null);
        rb.AddForce(transform.forward * -3f, ForceMode.Impulse);
        isHold = false;
        yield return StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        inAction = false;
    }
}
