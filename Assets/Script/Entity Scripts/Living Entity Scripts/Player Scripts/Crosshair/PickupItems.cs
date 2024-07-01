using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItems : MonoBehaviour
{
    // Start is called before the first frame update

    public KeyScript keyScript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCamp;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    public void PickUp()
    {
        equipped = true;
        slotFull = true;

        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;



        rb.isKinematic = true;
        coll.isTrigger = true;

        keyScript.enabled = true;
    }

    public void Drop()
    {
        equipped = false;
        slotFull = false;

        //Set parent to null
        transform.SetParent(null);

        //Make Rigidbody not kinematic and Boxllider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Bun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //AddForce
        rb.AddForce(fpsCamp.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCamp.up * dropUpwardForce, ForceMode.Impulse);

        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddForce(new Vector3(random, random, random) * 10);

        keyScript.enabled = false;
    }

    void Start()
    {

        if (!equipped)
        {
            keyScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }

        if (equipped)
        {
            keyScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (keyScript.canHold == true)
        {
            PickUp();
        }
    }
}
