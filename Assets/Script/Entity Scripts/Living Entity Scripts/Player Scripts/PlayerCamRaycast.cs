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

    CinemachineBrain cmBrain;
    public CinemachineVirtualCamera cmVC;
    //Inspecting
    public bool isInspecting = false;
    Vector3 originalPosition;
    Quaternion originalRotation;
    GameObject objToRotate;
    public GameObject vMachine;
    Vector3 vMacOriginalPosition;
    Quaternion vMacOriginalRotation;

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
            if (!isInspecting) InteractRaycast();
            else
            {
                if (Input.GetKeyDown(KeyCode.E) && isInspecting)
                {
                    isInspecting = false;
                    objToRotate.transform.position = originalPosition;
                    objToRotate.transform.rotation = originalRotation;
                    vMachine.transform.position = vMacOriginalPosition;
                    vMachine.transform.rotation = vMacOriginalRotation;
                    objToRotate = null;

                    cmVC.enabled = true;
                    cmBrain.enabled = true;
                }
            }
            
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
                    cmVC.enabled = false;
                    cmBrain.enabled = false;

                    UnityEngine.Debug.Log("Inspect Object Function");
                    originalPosition = hitGameObject.transform.position;
                    originalRotation = hitGameObject.transform.rotation;
                    vMacOriginalPosition = vMachine.transform.position;
                    vMacOriginalRotation = vMachine.transform.rotation;
                    hitGameObject.transform.position = new Vector3(hitGameObject.transform.position.x, hitGameObject.transform.position.y + 1f, hitGameObject.transform.position.z);
                   
                    objToRotate = hitGameObject;
                    isInspecting = true;

                    
                }
                else if(Input.GetKeyDown(KeyCode.E) && isInspecting)
                {
                    isInspecting = false;
                    
                    hitGameObject.transform.position = originalPosition;
                    hitGameObject.transform.rotation = originalRotation;
                    vMachine.transform.position = vMacOriginalPosition;
                    vMachine.transform.rotation = vMacOriginalRotation;
                    objToRotate = null;

                    cmVC.enabled = true;
                    cmBrain.enabled = true;
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

    /*private IEnumerator OffCam()
    {
        cmVC.enabled = false;
        cmBrain.enabled = false;
        yield return null;
    }

    private IEnumerator OnCam()
    {
        yield return null;
        cmVC.enabled = true;
        cmBrain.enabled = true;
        
    }*/
}