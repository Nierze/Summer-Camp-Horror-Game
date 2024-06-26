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
    [SerializeField] private CharacterController controller;
    private float velocity;



    public Camera mainCamera;

    void Start()
    {
        controller = GetComponent<CharacterController>();
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
        Vector2 movement = NewInputManager.Instance.GetPlayerMovement();

        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = forward * movement.y + right * movement.x;

        if (desiredMoveDirection != Vector3.zero)
        {
            float characterFacing = Mathf.Atan2(desiredMoveDirection.x, desiredMoveDirection.z) * Mathf.Rad2Deg;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, characterFacing, ref velocity, 0.05f);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
        }

        float currentSpeed = isSprinting ? movementSpeed * sprintSpeedMultiplier : movementSpeed;
        Vector3 movementF = desiredMoveDirection * currentSpeed * Time.deltaTime;

        controller.Move(movementF);
    }
}
