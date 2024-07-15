using UnityEngine;
using Cinemachine;
using System.Collections;

public class PlayerCamRaycast : MonoBehaviour
{
    public Camera mainCamera;

    //public MaterialControl[] highlightables;
    //private MaterialControl currentHighlight = null;

    SwitchViewPerspective FPV;

    GameObject hitGameObject;

    public CinemachineBrain cmBrain;
    public CinemachineVirtualCamera cmVC;

    //Inspecting
    public bool isInspecting = false;
    Vector3 originalPosition;
    Quaternion originalRotation;
    GameObject objToRotate;

    //last raycast hit object
    private GameObject lastHitGameObject = null;

    //public GameObject vMachine;
    

    void Start()
    {
        //mainCamera = GameObject.Find("FPV").transform.Find("Main Camera").GetComponent<Camera>();
        //mainCamera;

        mainCamera = Camera.main;

        cmBrain = GameObject.Find("FPV").transform.Find("Main Camera").GetComponent<CinemachineBrain>();
        cmVC = GameObject.Find("FPV").transform.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();

        //vMachine = GameObject.Find("FPV").transform.Find("Virtual Camera");

        GameObject FPVObject = GameObject.Find("Player (FPV _ TPV)");
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

    /*void InteractRaycast()
    {
        Ray interactionRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionRayHit;
        float interactionRayLength = 50.0f;

        Debug.DrawRay(interactionRay.origin, interactionRay.direction * interactionRayLength, Color.red);


        if (Physics.Raycast(interactionRay, out interactionRayHit, interactionRayLength))
        {
            hitGameObject = interactionRayHit.transform.gameObject;
            SetObjectEnum hitEnum = hitGameObject.GetComponent<SetObjectEnum>();
            if (hitGameObject.CompareTag("Scannable"))
            {
                switch (hitEnum.objectInteraction)
                {
                    case "open":
                        hitEnum.enableOpen = true;
                    break;

                    case "pickUp":
                        hitEnum.enablePickUp = true;
                    break;

                    // default:
                    //     if (Input.GetKeyDown(KeyCode.E) && !isInspecting)
                    //     {
                    //         cmVC.enabled = false;
                    //         cmBrain.enabled = false;

                    //         UnityEngine.Debug.Log("Inspect Object Function");
                    //         originalPosition = hitGameObject.transform.position;
                    //         originalRotation = hitGameObject.transform.rotation;

                    //         hitGameObject.transform.position = new Vector3(hitGameObject.transform.position.x, hitGameObject.transform.position.y + 1f, hitGameObject.transform.position.z);

                    //         objToRotate = hitGameObject;
                    //         isInspecting = true;
                    //     }
                    //     else if (Input.GetKeyDown(KeyCode.E) && isInspecting)
                    //     {
                    //         enableCam();

                    //         isInspecting = false;

                    //         hitGameObject.transform.position = originalPosition;
                    //         hitGameObject.transform.rotation = originalRotation;

                    //         objToRotate = null;


                    //     }
                    // break;
                }

            }
        }
    }*/

    void InteractRaycast()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray interactionRay = mainCamera.ScreenPointToRay(screenCenter); //Input.mousePosition
        RaycastHit interactionRayHit;
        float interactionRayLength = 50.0f;

        Debug.DrawRay(interactionRay.origin, interactionRay.direction * interactionRayLength, Color.red);

        if (Physics.Raycast(interactionRay, out interactionRayHit, interactionRayLength))
        {
            hitGameObject = interactionRayHit.transform.gameObject;
            SetObjectEnum hitEnum = hitGameObject.GetComponent<SetObjectEnum>();

            if (hitGameObject.CompareTag("Scannable"))
            {
                if (lastHitGameObject != null && lastHitGameObject != hitGameObject)
                {
                    SetObjectEnum lastHitEnum = lastHitGameObject.GetComponent<SetObjectEnum>();
                    if (lastHitEnum != null)
                    {
                        lastHitEnum.enableOpen = false;
                        lastHitEnum.enablePickUp = false;
                    }
                }

                switch (hitEnum.objectInteraction)
                {
                    case "open":
                        hitEnum.enableOpen = true;
                        break;

                    case "pickUp":
                        hitEnum.enablePickUp = true;
                        break;

                    default:
                        /*if (Input.GetKeyDown(KeyCode.E) && !isInspecting)
                        {
                            cmVC.enabled = false;
                            cmBrain.enabled = false;

                            UnityEngine.Debug.Log("Inspect Object Function");
                            originalPosition = hitGameObject.transform.position;
                            originalRotation = hitGameObject.transform.rotation;

                            hitGameObject.transform.position = new Vector3(hitGameObject.transform.position.x, hitGameObject.transform.position.y + 1f, hitGameObject.transform.position.z);

                            objToRotate = hitGameObject;
                            isInspecting = true;
                        }
                        else if (Input.GetKeyDown(KeyCode.E) && isInspecting)
                        {
                            enableCam();

                            isInspecting = false;

                            hitGameObject.transform.position = originalPosition;
                            hitGameObject.transform.rotation = originalRotation;

                            objToRotate = null;
                        }*/
                        break;
                }

                lastHitGameObject = hitGameObject;
            }
        }
        else
        {
            if (lastHitGameObject != null)
            {
                SetObjectEnum lastHitEnum = lastHitGameObject.GetComponent<SetObjectEnum>();
                if (lastHitEnum != null)
                {
                    lastHitEnum.enableOpen = false;
                    lastHitEnum.enablePickUp = false;
                }
                lastHitGameObject = null;
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

/*
 if (hitEnum == null)
                {
                    if (Input.GetKeyDown(KeyCode.E) && !isInspecting)
                    {
                        cmVC.enabled = false;
                        cmBrain.enabled = false;

                        UnityEngine.Debug.Log("Inspect Object Function");
                        originalPosition = hitGameObject.transform.position;
                        originalRotation = hitGameObject.transform.rotation;

                        hitGameObject.transform.position = new Vector3(hitGameObject.transform.position.x, hitGameObject.transform.position.y + 1f, hitGameObject.transform.position.z);

                        objToRotate = hitGameObject;
                        isInspecting = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.E) && isInspecting)
                    {
                        enableCam();

                        isInspecting = false;

                        hitGameObject.transform.position = originalPosition;
                        hitGameObject.transform.rotation = originalRotation;

                        objToRotate = null;


                    }
                }
                else
*/