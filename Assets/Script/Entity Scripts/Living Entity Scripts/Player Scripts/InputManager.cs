using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance
    {
        get
        {
            return instance;
        }
    }

    private PlayerControls playerControls;

    private void Awake()
    {
        if (instance !=null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        playerControls = new PlayerControls();
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool GetSprint()
    {
        return playerControls.Player.Sprint.ReadValue<float>() > 0;
    }

    public bool GetDefend()
    {
        return playerControls.Player.Defend.ReadValue<float>() > 0;
    }


    public bool GetAttack()
    {
        return playerControls.Player.Attack.triggered;
    }

    // public bool OnJump()
    // {
    //     return playerControls.Player.Jump.triggered;
    // }


}
