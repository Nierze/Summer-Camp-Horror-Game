using UnityEngine;
using Cinemachine;

public class ArmsRotationHandler : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Reference to your Cinemachine virtual camera
    public Transform pivotPoint; // Reference to the pivot point of the camera

    private void LateUpdate() 
    {
        if (virtualCamera != null)
        {
            // Get the camera's rotation
            Quaternion cameraRotation = virtualCamera.State.FinalOrientation;

            pivotPoint.rotation = Quaternion.Euler(new Vector3(cameraRotation.eulerAngles.x, cameraRotation.eulerAngles.y, 0f));
        }
    }
}
