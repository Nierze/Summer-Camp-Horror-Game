using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewInputManager : MonoBehaviour
{

    private static NewInputManager instance;
    public static NewInputManager Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField] private float movementSpeed = 5f;






    private NewPlayerControls playerControls;
    private InputAction movementAction;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        playerControls = new NewPlayerControls();
        Cursor.visible = false;
    }


    /////////////////////////////////////////////////////////////
    /// Player Controller Enabler and Disabler

    public Vector2 GetPlayerMovement()
    {
         return playerControls.Ground.Movement.ReadValue<Vector2>();
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
