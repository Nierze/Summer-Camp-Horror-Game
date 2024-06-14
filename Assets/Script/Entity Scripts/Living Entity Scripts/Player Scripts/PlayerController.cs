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
    private float jumpHeight = 20.0f;
    private float gravityValue = -9.81f;

    [SerializeField]
    private Light light;


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
        light.transform.position = cameraTransform.position;
        playerSpeed = 20.0f;
       
    }

    void Update()
    {
        // groundedPlayer = controller.isGrounded;
        // if (groundedPlayer && playerVelocity.y < 0)
        // {
        //     playerVelocity.y = 0f;
        // }
        

        /////////////////////////////////////////
        // Movement Handler
        if (inputManager.GetSprint())
        {
            Run();
        }      
        else
        {
            Walk();
        }

        /////////////////////////////////////////
        // Attack Handler

        if (inputManager.GetAttack())
        {
            UnityEngine.Debug.Log("Attacked");
        }

        /////////////////////////////////////////
        // Defend Handler
        if (inputManager.GetDefend())
        {
            UnityEngine.Debug.Log("Defended");
        }



    }

    void Walk()
    {
        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

    }

    void Run()
    {
        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * (playerSpeed * sprintSpeedMultiplier));

    }
}
