using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    // [SerializeField] private Transform CameraFollow;

    /////////////////////////////////////////
    /// Player Speed variables
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float sprintSpeedMultiplier = 2f;
    private float velocity;



    public Camera mainCamera;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isSprinting = NewInputManager.Instance.GetSprint();
        Move(isSprinting);

        

        // Debug.Log(mainCamera.transform.forward.normalized);
        // Debug.Log(mainCamera.transform.right.normalized);
    }

    void Move(bool isSprinting)
    {
        // Get input from the player
        Vector2 movement = NewInputManager.Instance.GetPlayerMovement();

        /////////////////////////////////////////
        /// Move player in the camera direction
        
        // Create new vectors for the forward and side directions
        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;

        // Set the y value of both to 0 so the player doesn't move up or down
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
        transform.position += (!isSprinting) ? desiredMoveDirection * movementSpeed * Time.deltaTime 
        : desiredMoveDirection * movementSpeed * sprintSpeedMultiplier * Time.deltaTime;
    }
}
