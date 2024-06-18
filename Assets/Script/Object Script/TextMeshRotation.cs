using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMeshRotation : MonoBehaviour
{
    private GameObject cameraObject;
    private Transform cameraTransform;

    void Start()
    {
        cameraObject = GameObject.FindWithTag("MainCamera");
        cameraTransform = cameraObject.transform;
    }

    void Update()
    {
        Quaternion cameraRotation = cameraTransform.rotation;
        Quaternion textRotation = Quaternion.Euler(0f, cameraRotation.eulerAngles.y, 0f);
        transform.rotation = textRotation;
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class TextMeshRotation : MonoBehaviour
// {
//     private RectTransform cameraRotation;
//     private GameObject cameraObject;
//     private Transform cameraTransform;
//     void Start()
//     {
//         cameraObject = GameObject.FindWithTag("MainCamera");
//         cameraTransform = cameraObject.transform;
//         cameraRotation = GetComponent<RectTransform>();

//     }

//     void Update()
//     {
//         Quaternion rotation = cameraTransform.rotation;
//         transform.rotation = rotation;
//     }
// }
