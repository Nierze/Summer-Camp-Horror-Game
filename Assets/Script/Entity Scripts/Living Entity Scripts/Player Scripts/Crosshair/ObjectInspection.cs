using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectInspection : MonoBehaviour
{
    public float distance;
    public Transform playerSocket;

    public Vector3 originalPos;
    public bool onInspect = false;
    public GameObject inspected;

    public PlayerController playerScript;

    
    // Update is called once per frame
    void Update()
    {
        if (onInspect)
        {
            inspected.transform.position = Vector3.Lerp(inspected.transform.position, playerSocket.position, 0.2f);
            playerSocket.Rotate(new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0) * Time.deltaTime * 125f);
        } else if (inspected != null)
        {
            inspected.transform.SetParent(null);
            inspected.transform.position = Vector3.Lerp(inspected.transform.position, originalPos, 0.2f);
        } if (Input.GetKeyDown(KeyCode.F))
        {
            onInspect = false;
        }
    }

    IEnumerator pickupItem()
    {
        playerScript.enabled = false;
        yield return new WaitForSeconds(0.2f);
        inspected.transform.SetParent(playerSocket);
    }

    IEnumerator dropItem()
    {
        inspected.transform.rotation = Quaternion.identity;
        yield return new WaitForSeconds(0.2f);
        playerScript.enabled = true;
    }
}
