// using Cinemachine;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class FlashlightFollowScript : MonoBehaviour
// {
//     [SerializeField]
//     private float horizontalSpeed = 10f;
//     [SerializeField]
//     private float verticalSpeed = 10f;
//     [SerializeField]
//     private float clampAngle = 80f;
//     [SerializeField]
//     private float mouseSensitivity = 20f;


//     private InputManager inputManager;
//     private float rotation_x;
//     private float rotation_y;
//     private Vector2 deltaInput;

//     void Start()
//     {
//         inputManager = InputManager.Instance;
//     }
//     public void FixedUpdate()
//     {
//             deltaInput = inputManager.GetMouseDelta();
//             rotation_x += deltaInput.y * verticalSpeed * Time.deltaTime;
//             rotation_y += deltaInput.x * horizontalSpeed * Time.deltaTime;
//             rotation_y = Mathf.Clamp(rotation_y, -clampAngle, clampAngle);
//             gameObject.GetComponent<Transform>().localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, rotation_y, 0), mouseSensitivity * Time.deltaTime);
  
//         }
//     }

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightFollowScript : MonoBehaviour
{
    [SerializeField]
    private float horizontalSpeed = 10f;
    [SerializeField]
    private float verticalSpeed = 10f;
    [SerializeField]
    private float clampAngle = 80f;
    [SerializeField]
    private float mouseSensitivity = 20f;

    private InputManager inputManager;
    private float rotationX = 0f;
    private float rotationY = 0f;
    private Vector2 deltaInput;

    void Start()
    {
        inputManager = InputManager.Instance;
        Vector3 rotation = transform.localRotation.eulerAngles;
        rotationX = rotation.x;
        rotationY = rotation.y;
    }

    void FixedUpdate()
    {
        deltaInput = inputManager.GetMouseDelta();

        rotationX -= deltaInput.y * verticalSpeed * Time.deltaTime;
        rotationY += deltaInput.x * horizontalSpeed * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, localRotation, mouseSensitivity * Time.deltaTime);
    }
}
