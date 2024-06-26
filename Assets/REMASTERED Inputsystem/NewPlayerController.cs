using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    // [SerializeField] private Transform CameraFollow;

    /////////////////////////////////////////
    /// Player Speed variables
    [SerializeField] private float movementSpeed = 5f;
    private float velocity;

    public Camera mainCamera;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Debug.Log(mainCamera.transform.forward);
        Debug.Log(mainCamera.transform.right);
    }

    void Move()
    {
        Vector2 movement = NewInputManager.Instance.GetPlayerMovement();

        /////////////////////////////////////////
        /// Calculate the movement direction relative to the camera
        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = forward * movement.y + right * movement.x;

        /////////////////////////////////////////
        /// Player Rotation
        if (desiredMoveDirection != Vector3.zero)
        {
            float characterFacing = Mathf.Atan2(desiredMoveDirection.x, desiredMoveDirection.z) * Mathf.Rad2Deg;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, characterFacing, ref velocity, 0.05f);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
        }

        /////////////////////////////////////////
        /// Player Movement
        transform.position += desiredMoveDirection * movementSpeed * Time.deltaTime;
    }
}
