using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewInputManager : MonoBehaviour
{


    /////////////////////////////////////////////////////////////
    /// Singleton Pattern
    private static NewInputManager instance;
    public static NewInputManager Instance
    {
        get
        {
            return instance;
        }
    }

    /////////////////////////////////////////////////////////////
    /// Player Controller

    private NewPlayerControls playerControls;
    private InputAction movementAction;

    /////////////////////////////////////////////////////////////
    /// Awake Method
    private void Awake()
    {
        // Singleton Pattern
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        // Player Controller
        playerControls = new NewPlayerControls();
        Cursor.visible = false;
    }


    /////////////////////////////////////////////////////////////
    /// Player Controller Enabler and Disabler

    public Vector2 GetPlayerMovement()
    {
         return playerControls.Ground.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return playerControls.Ground.Look.ReadValue<Vector2>();
    }

    public bool GetSprint()
    {
        return playerControls.Ground.Sprint.ReadValue<float>() > 0;
    }

    public bool GetJump()
    {
        return playerControls.Ground.Jump.triggered;
    }

    public bool GetRangeAttack()
    {
        return playerControls.Ground.RangeAttack.triggered;
    }
    

    /////////////////////////////////////////////////////////////
    /// Player Controller Enabler and Disabler

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    
}
