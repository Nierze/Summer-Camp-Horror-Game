using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    private Vector3 playerVelocity;
    [SerializeField]
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private InputManager inputManager;
    private Transform cameraTransform;

    [SerializeField]
    private float sprintSpeedMultiplier = 2f;
    private bool isRunning = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
        playerSpeed = 20.0f;
       
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
                
        if (Input.GetKey(KeyCode.LeftShift) && !isRunning)
        {
            Run();
        }      
        else
        {
            Walk();
        }


        //if (isRunning) Run();
        //else Walk();
                

       /*** if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
       ***/

        // Changes the height position of the player..
        if (inputManager.PlayerJumpedThisFrame() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }

    void Walk()
    {
        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        //UnityEngine.Debug.Log("Is Walking");
    }

    void Run()
    {
        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * (playerSpeed * sprintSpeedMultiplier));

        //UnityEngine.Debug.Log("Is Running");
    }
}
