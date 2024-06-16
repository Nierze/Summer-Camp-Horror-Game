using UnityEngine;
using Cinemachine;

public class ArmsRotationHandler : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Reference to your Cinemachine virtual camera

    private void LateUpdate() 
    {
        if (virtualCamera != null)
        {
            // Get the camera's rotation
            Quaternion cameraRotation = virtualCamera.State.FinalOrientation;

            // Apply the rotation ONLY to the Y-axis (vertical axis)
            transform.rotation = Quaternion.Euler(cameraRotation.eulerAngles.x, cameraRotation.eulerAngles.y, cameraRotation.eulerAngles.z); 
        }
    }
}
