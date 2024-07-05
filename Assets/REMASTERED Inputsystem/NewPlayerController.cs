using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    /////////////////////////////////////////
    /// Player Movement variables

    [Header("Player Movement")]
    [SerializeField] private float movementSpeed = 20f;
    [SerializeField] private float sprintSpeedMultiplier = 2f;
    [SerializeField] private float jumpPower = 1f;
    private float rotateVelocity;

    /////////////////////////////////////////
    /// Animation variables
    private float animationSpeed;
    private float animationSpeedVelocity;
    [SerializeField] private float animationSmoothTime = 0.2f;

    /////////////////////////////////////////
    /// Character components
    private CharacterController controller;
    private Animator animator;
    public Camera mainCamera;

    /////////////////////////////////////////
    /// Test variables
    private Vector3 velocity;
    [SerializeField] private float gravity = -9.81f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetBool("Grounded", true);
    }

    // Update is called once per frame
    void Update()
    {

        //////////////////////////////////////////////////////
        /// Jump handler
        HandleJump();


        //////////////////////////////////////////////////////
        /// Movement handler
        bool isSprinting = NewInputManager.Instance.GetSprint();

            
        Move(isSprinting);
        


        //////////////////////////////////////////////////////
        /// Gravity handler
        ApplyGravity();

        //////////////////////////////////////////////////////
        /// Debug stuffs
        Debug.Log(checkGrounded());
    }

    void Move(bool isSprinting)
    {
        Vector2 movement = NewInputManager.Instance.GetPlayerMovement();

        //////////////////////////////////////////////////////
        /// Variables used to move player in camera direction
        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        //////////////////////////////////////////////////////
        /// Rotate player to face the direction of movement
        Vector3 desiredMoveDirection = forward * movement.y + right * movement.x;

        if (desiredMoveDirection != Vector3.zero)
        {
            float characterFacing = Mathf.Atan2(desiredMoveDirection.x, desiredMoveDirection.z) * Mathf.Rad2Deg;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, characterFacing, ref rotateVelocity, 0.05f);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
        }

        //////////////////////////////////////////////////////
        /// Sprint handler
        float totalSpeed = isSprinting ? movementSpeed * sprintSpeedMultiplier : movementSpeed;

        //////////////////////////////////////////////////////
        /// Move player + computed speed
        Vector3 movementF = desiredMoveDirection * totalSpeed * Time.deltaTime;

        //////////////////////////////////////////////////////
        /// Apply gravity to movement
        movementF.y = velocity.y * Time.deltaTime;

        // Move player
        controller.Move(movementF);

        //////////////////////////////////////////////////////
        /// Animation handler
        float maxSpeed = movementSpeed * sprintSpeedMultiplier;
        float targetAnimationSpeed = controller.velocity.magnitude / maxSpeed;

        // Animation smoothing
        animationSpeed = Mathf.SmoothDamp(animationSpeed, targetAnimationSpeed, ref animationSpeedVelocity, animationSmoothTime);
        animator.SetFloat("Magnitude", animationSpeed);
    }

    void HandleJump()
    {
        if (checkGrounded() && velocity.y < 0)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Grounded", true);
            animator.SetBool("FreeFall", false);
            //velocity.y = -1f; // Small downward velocity when grounded
        }

        if (checkGrounded() && NewInputManager.Instance.GetJump())
        {
            velocity.y = Mathf.Sqrt(jumpPower * -2f * gravity);
            animator.SetBool("Jump", true);
            animator.SetBool("Grounded", false);
            animator.SetBool("FreeFall", true);
        }
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    bool checkGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, controller.height / 2 + 0.1f);
    }
}
