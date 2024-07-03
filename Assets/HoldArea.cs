using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldArea : MonoBehaviour
{
    // Start is called before the first frame update
    PickUpObjects pickUpObjects;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure all child objects maintain the same position and rotation as the hold area
        foreach (Transform child in transform)
        {
            child.localPosition = Vector3.zero;
            child.localRotation = Quaternion.identity;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            pickUpObjects.Drop();
        }
    }
}
