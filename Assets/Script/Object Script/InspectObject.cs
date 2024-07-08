using System.Collections.Generic;
using UnityEngine;

public class InspectObject : MonoBehaviour
{
    public GameObject offset;
    private PlayerController _playerInput;
    GameObject targetObject;

    public bool isExamining = false;

    public Canvas _canva;

    public GameObject tableObject;

    private Vector3 lastMousePosition;

    private Transform examinedObject;

    private Dictionary<Transform, Vector3> originalPositions = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform, Quaternion> originalRotations = new Dictionary<Transform, Quaternion>();

    void Start()
    {
        _canva.enabled = false;
        targetObject = GameObject.Find("Player");
        _playerInput = targetObject.GetComponent<PlayerController>(); // Corrected typo here
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Corrected KeyCode usage
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("CanObserve"))
                {
                    ToggleExamination();

                    if (isExamining)
                    {
                        examinedObject = hit.transform;
                        originalPositions[examinedObject] = examinedObject.position;
                        originalRotations[examinedObject] = examinedObject.rotation;
                    }
                }
            }
        }

        if (CheckUserClose())
        {
            if (isExamining)
            {
                _canva.enabled = false;
                Examine();
                StartExamination();
            }
            else
            {
                _canva.enabled = false;
                nonExamine(); // Corrected method name
                StopExamination(); // Corrected method name
            }
        }
        else
        {
            _canva.enabled = false;
        }
    }

    public void ToggleExamination()
    {
        isExamining = !isExamining;
    }

    void StartExamination()
    {
        lastMousePosition = Input.mousePosition;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _playerInput.enabled = false;
    }

    void StopExamination()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _playerInput.enabled = true;
    }

    void Examine()
    {
        if (examinedObject != null)
        {
            examinedObject.position = Vector3.Lerp(examinedObject.position, offset.transform.position, 0.2f); // Corrected usage to Vector3.Lerp

            Vector3 deltaMouse = Input.mousePosition - lastMousePosition; // Corrected delta calculation
            float rotationSpeed = 1.0f;
            examinedObject.Rotate(deltaMouse.x * rotationSpeed * Vector3.up, Space.World);
            examinedObject.Rotate(deltaMouse.y * rotationSpeed * Vector3.left, Space.World);
            lastMousePosition = Input.mousePosition;
        }
    }

    void nonExamine() // Corrected method name to comply with naming conventions
    {
        if (examinedObject != null)
        {
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

    bool CheckUserClose()
    {
        float distance = Vector3.Distance(targetObject.transform.position, tableObject.transform.position);

        return (distance < 2);
    }
}
