/*using UnityEngine;
using Cinemachine;

public class PlayerCamRaycast : MonoBehaviour
{
    public Camera mainCamera;
    public CinemachineVirtualCamera cmVC;

    private GameObject hitGameObject;
    public bool isInspecting = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isInspecting)
        {
            InteractRaycast();
        }
        else if (Input.GetKeyDown(KeyCode.E) && isInspecting)
        {
            StopInspecting();
        }

        if (isInspecting)
        {
            Inspecting();
        }
    }

    void InteractRaycast()
    {
        Ray interactionRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionRayHit;

        if (Physics.Raycast(interactionRay, out interactionRayHit))
        {
            hitGameObject = interactionRayHit.transform.gameObject;
            if (hitGameObject.CompareTag("Scannable"))
            {
                StartInspecting(hitGameObject);
            }
        }
    }

    void StartInspecting(GameObject obj)
    {
        isInspecting = true;
        originalPosition = obj.transform.position;
        originalRotation = obj.transform.rotation;

        // Disable the virtual camera while inspecting
        cmVC.enabled = false;

        // Move the object up slightly for inspection
        obj.transform.position += Vector3.up * 1.0f;
    }

    void StopInspecting()
    {
        isInspecting = false;

        // Restore original position and rotation of the inspected object
        if (hitGameObject != null)
        {
            hitGameObject.transform.position = originalPosition;
            hitGameObject.transform.rotation = originalRotation;
        }

        // Re-enable the virtual camera
        cmVC.enabled = true;

        hitGameObject = null;
    }

    void Inspecting()
    {
        // Rotate the inspected object based on mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotationSpeed = 1.5f;

        float rotationY = mouseX * rotationSpeed;
        float rotationX = -mouseY * rotationSpeed;

        hitGameObject.transform.Rotate(Vector3.up, rotationY, Space.World);
        hitGameObject.transform.Rotate(Vector3.right, rotationX, Space.World);
    }
}*/

using UnityEngine;
using Cinemachine;
using System.Collections;

public class PlayerCamRaycast : MonoBehaviour
{
    public Camera mainCamera;

    public MaterialControl[] highlightables;
    private MaterialControl currentHighlight = null;

    SwitchViewPerspective FPV;

    GameObject hitGameObject;

    public CinemachineBrain cmBrain;
    public CinemachineVirtualCamera cmVC;

    //Inspecting
    public bool isInspecting = false;
    Vector3 originalPosition;
    Quaternion originalRotation;
    GameObject objToRotate;

    public GameObject vMachine;
    

    void Start()
    {
        GameObject FPVCamera = GameObject.FindWithTag("MainCamera");
        cmBrain = FPVCamera.GetComponent<CinemachineBrain>();
        GameObject FPVObject = GameObject.Find("Player (FPV / TPV)");
        FPV = FPVObject.GetComponent<SwitchViewPerspective>();
    }

    void Update()
    {
        if (FPV.isFPV)
        {
            InteractRaycast();

            if (isInspecting && objToRotate != null)
            {
                Inspecting(objToRotate);
            }
        }
    }

    void InteractRaycast()
    {
        Ray interactionRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionRayHit;
        float interactionRayLength = 50.0f;

        Debug.DrawRay(interactionRay.origin, interactionRay.direction * interactionRayLength, Color.red);


        if (Physics.Raycast(interactionRay, out interactionRayHit, interactionRayLength))
        {
            hitGameObject = interactionRayHit.transform.gameObject;
            if (hitGameObject.CompareTag("Scannable"))
            {
                if (Input.GetKeyDown(KeyCode.E) && !isInspecting)
                {
                    //cmVC.enabled = false;
                    //cmBrain.enabled = false;

                    UnityEngine.Debug.Log("Inspect Object Function");
                    originalPosition = hitGameObject.transform.position;
                    originalRotation = hitGameObject.transform.rotation;

                    hitGameObject.transform.position = new Vector3(hitGameObject.transform.position.x, hitGameObject.transform.position.y + 1f, hitGameObject.transform.position.z);
                   
                    objToRotate = hitGameObject;
                    isInspecting = true;
                }
                else if(Input.GetKeyDown(KeyCode.E) && isInspecting)
                {
                    enableCam();

                    isInspecting = false;
                    
                    hitGameObject.transform.position = originalPosition;
                    hitGameObject.transform.rotation = originalRotation;

                    objToRotate = null;


                }
            }
        }
    }

    void Inspecting(GameObject obj)
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotationSpeed = 1.5f;

        float rotationY = mouseX * rotationSpeed;
        float rotationX = -mouseY * rotationSpeed;

        obj.transform.Rotate(Vector3.up, rotationY, Space.World);
        obj.transform.Rotate(Vector3.right, rotationX, Space.World);
    }

    void enableCam()
    {
        cmVC.enabled = true;
        cmBrain.enabled = true;
    }
}

/*else
{
    if (Input.GetKeyDown(KeyCode.E) && isInspecting)
    {
        isInspecting = false;
        objToRotate.transform.position = originalPosition;
        objToRotate.transform.rotation = originalRotation;
        objToRotate = null;

        cmVC.enabled = true;
        cmBrain.enabled = true;
    }
}*/