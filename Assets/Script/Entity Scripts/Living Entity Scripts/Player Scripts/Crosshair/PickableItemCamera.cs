using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItemCamera : MonoBehaviour
{
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Camera component attached to this GameObject
        cam = GetComponent<Camera>();

        // Set near and far clipping planes to prevent clipping through walls
        cam.nearClipPlane = 0.1f; // Adjust as necessary
        cam.farClipPlane = 10f;   // Adjust as necessary

        // Optionally, adjust culling mask to only render necessary layers
        cam.cullingMask = LayerMask.GetMask("Default", "Pickable"); // Adjust as necessary
    }

    // Update is called once per frame
    void Update()
    {

    }
}
