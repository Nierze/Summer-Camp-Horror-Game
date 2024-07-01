using UnityEngine;

public class PlayerCamRaycast : MonoBehaviour
{
    public Camera mainCamera;

    void Update()
    {
        InteractRaycast();
    }

    void InteractRaycast()
    {
        Ray interactionRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionRayHit;
        float interactionRayLength = 50.0f;

        Debug.DrawRay(interactionRay.origin, interactionRay.direction * interactionRayLength, Color.red);
    }
}