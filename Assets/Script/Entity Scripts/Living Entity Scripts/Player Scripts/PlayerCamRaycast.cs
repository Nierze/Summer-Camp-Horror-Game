using UnityEngine;
using Cinemachine;

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
            /*if (hitGameObject.CompareTag("Selectable"))
            {
                UnityEngine.Debug.Log(hitGameObject.name);

                MaterialControl highlight = hitGameObject.GetComponent<MaterialControl>();
                string hitFeedback = hitGameObject.name;

                if (highlight != null)
                {
                    //Debug.Log("highlight = " + highlight);
                    //Debug.Log("hitFeedback = " + hitFeedback);

                    if (currentHighlight != highlight)
                    {
                        if (currentHighlight != null)
                        {
                            currentHighlight.DisableMaterial();
                        }
                        highlight.EnableMaterial();
                        currentHighlight = highlight;
                    }
                }
            }
            else
            {
                if (currentHighlight != null)
                {
                    currentHighlight.DisableMaterial();
                    currentHighlight = null;
                }
            }*/
            if (hitGameObject.CompareTag("Scannable"))
            {
                if (Input.GetKeyDown(KeyCode.E) && !isInspecting)
                {
                    UnityEngine.Debug.Log("Inspect Object Function");
                    originalPosition = hitGameObject.transform.position;
                    originalRotation = hitGameObject.transform.rotation;
                    hitGameObject.transform.position = new Vector3(hitGameObject.transform.position.x, hitGameObject.transform.position.y + 1f, hitGameObject.transform.position.z);
                    
                    cmVC.enabled = false;
                    cmBrain.enabled = false;
                    objToRotate = hitGameObject;
                    isInspecting = true;
                }
                else if(Input.GetKeyDown(KeyCode.E) && isInspecting)
                {
                    isInspecting = false;

                    cmVC.enabled = true;
                    cmBrain.enabled = true;
                    
                    hitGameObject.transform.position = originalPosition;
                    hitGameObject.transform.rotation = originalRotation;
                    objToRotate = null;
                }
            }
        }
        else
        {
            if (currentHighlight != null)
            {
                currentHighlight.DisableMaterial();
                currentHighlight = null;
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
}