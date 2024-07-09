using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using Cinemachine;

public class ObjectInteract : MonoBehaviour
{
    public GameObject offset;
    public PlayerInput _playerInput;
    public GameObject targetObject;
    public bool isExamining = false;

    public Canvas _canva;

    public GameObject tableObject;

    private Vector3 lastMousePosition;

    public Transform examinedObject; // Store the currently examined object


    //List of position and rotation of the interactble objects 
    public Dictionary<Transform, Vector3> originalPositions = new Dictionary<Transform, Vector3>();
    public Dictionary<Transform, Quaternion> originalRotations = new Dictionary<Transform, Quaternion>();

    //public GameObject cinemachine;
    public CinemachineBrain cinemachineBrain;

    public PlayerController _playerController;
    void Start()
    {
        isExamining = false;
        _canva.enabled = false;
        _playerInput = targetObject.GetComponent<PlayerInput>();


    }

    void Update()
    {
        // it performs a raycast from the camera to the mouse position and checks if it hits an object tagged as "Object."
        // If it does, it toggles the examination state and stores the examined object's original position and rotation.

        //if (Input.GetKeyDown(KeyCode.E) && isExamining)
        if(Input.GetKeyDown(KeyCode.E) && isExamining)
        {
            Debug.Log("is Examining");
            isExamining = false;
            //cinemachineBrain.enabled = (cinemachineBrain.enabled) ? false : true;
            if (cinemachineBrain.enabled)
            {
                cinemachineBrain.enabled = false;
                _playerController.enabled = false;
            }
            else
            {
                cinemachineBrain.enabled = true;
                _playerController.enabled = true;
                examinedObject = null;
            }
        }

        if (isExamining)
        {
            /*_canva.enabled = false;

            Examine(); StartExamination();*/
            //_canva.enabled = false;
            Rigidbody rb = examinedObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            NonExamine(); StopExamination();
        }
        else
        {
            //NonExamine(); StopExamination();
            Rigidbody rb = examinedObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            Examine(); StartExamination();
        }

    }
    public void ToggleExamination()
    {
        isExamining = !isExamining;

    }

    // This method is called when the player starts examining an object. It locks the cursor,
    // makes it visible, and disables the PlayerInput component to prevent player movement during examination.

    void StartExamination()
    {

        lastMousePosition = Input.mousePosition;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _playerInput.enabled = false;
    }

    //This method is called when the player stops examining an object. It locks the cursor again,
    //hides it, and re-enables the PlayerInput component to allow player movement.

    void StopExamination()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _playerInput.enabled = true;
    }


    // This method is called when the player is examining an object.
    // It moves the examined object towards the offset object and allows the player to rotate it based on mouse movement.

    void Examine()
    {
        examinedObject.transform.position = new Vector3(examinedObject.transform.position.x, examinedObject.transform.position.y + 0.5f, examinedObject.transform.position.z);
        if (examinedObject != null)
        {
            examinedObject.position = Vector3.Lerp(examinedObject.position, offset.transform.position, 0.2f);

            Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
            float rotationSpeed = 0.3f;
            examinedObject.Rotate(deltaMouse.x * rotationSpeed * Vector3.up, Space.World);
            examinedObject.Rotate(deltaMouse.y * rotationSpeed * Vector3.left, Space.World);
            lastMousePosition = Input.mousePosition;
        }
    }

    //This method is called when the player is not examining an object.
    //It resets the position and rotation of the examined object to its original values stored in the dictionaries.

    void NonExamine()
    {
        if (examinedObject != null)
        {
            // Reset the position and rotation of the examined object to its original values
            if (originalPositions.ContainsKey(examinedObject))
            {
                examinedObject.position = Vector3.Lerp(examinedObject.position, originalPositions[examinedObject], 0.2f);
            }
            if (originalRotations.ContainsKey(examinedObject))
            {
                examinedObject.rotation = Quaternion.Slerp(examinedObject.rotation, originalRotations[examinedObject], 0.2f);
            }
        }
    }


    // This method calculates the distance between the player(targetObject) and 
    // an object called tableObject.If the distance is less than 2 units, it returns true, indicating that the player is close to the object.
    bool CheckUserClose()
    {
        // Calculate the distance between the two GameObjects
        float distance = Vector3.Distance(targetObject.transform.position, tableObject.transform.position);
        Debug.Log("Distance: " + distance);
        // Check if they are close based on the threshold
        return (distance < 30);

    }

}