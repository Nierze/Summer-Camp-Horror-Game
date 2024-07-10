using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPVPlayerControl : MonoBehaviour
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

    private InputManager inputManager;
    private Transform cameraTransform;

    [SerializeField]
    private float sprintSpeedMultiplier = 2f;
    private bool isRunning = false;

    public GameObject followTransform;

    public float rotationPower = 3f;

    private float inputMagnitude;

    private Animator animator;

    public float playerCurrentSpeed;

    public TiyanakAttackPattern playerInRange;
    public PugotAttackPattern playerInRange2;
    public TiktikAttackPattern playerInRange3;

    PlayerCamRaycast camRaycast;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
        camRaycast = GetComponent<PlayerCamRaycast>();
        playerSpeed = 20f;

        controller = GetComponent<CharacterController>();
        followTransform = GameObject.Find("POV");

        playerInRange = (GameObject.Find("Tiyanak")) ? GameObject.Find("Tiyanak").GetComponent<TiyanakAttackPattern>() : null;
        playerInRange2 = (GameObject.Find("Pugot")) ? GameObject.Find("Pugot").GetComponent<PugotAttackPattern>() : null;
        playerInRange3 = (GameObject.Find("Tiktik")) ? GameObject.Find("Tiktik").GetComponent<TiktikAttackPattern>() : null;

        if (playerInRange == null) UnityEngine.Debug.Log("Tiyanak doesn't exist");
        if (playerInRange2 == null) UnityEngine.Debug.Log("Pugot doesn't exist");
        if (playerInRange3 == null) UnityEngine.Debug.Log("Tiktik doesn't exist");

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
        if (!camRaycast.isInspecting)
        {
            if (inputManager.GetSprint())
            {
                Run();
            }
            else
            {
                Walk();
            }
        }


        /////////////////////////////////////////
        // Attack Handler

        /*if (inputManager.GetAttack())
        {
            //playerInRange.playerAttackDetected = true;
            //playerInRange2.playerAttackDetected = true;
            //UnityEngine.Debug.Log("Attacked");
        }*/

        /////////////////////////////////////////
        // Defend Handler
        if (inputManager.GetDefend())
        {
            //UnityEngine.Debug.Log("Defended");
        }


        playerCurrentSpeed = controller.velocity.magnitude;
    }

    void Walk()
    {
        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = -1f;
        controller.Move(move * Time.deltaTime * playerSpeed);
        UpdateFollowTransformRotation();
    }

    void Run()
    {
        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = -1f;
        controller.Move(move * Time.deltaTime * (playerSpeed * sprintSpeedMultiplier));
        UpdateFollowTransformRotation();
    }

    void UpdateFollowTransformRotation()
    {
        Vector2 look = inputManager.GetMouseDelta();
        followTransform.transform.rotation *= Quaternion.AngleAxis(look.x * rotationPower, Vector3.up);
        followTransform.transform.rotation *= Quaternion.AngleAxis(-look.y * rotationPower, Vector3.right);

        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTransform.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        followTransform.transform.localEulerAngles = angles;
    }


}
