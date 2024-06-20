using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class OutlineSelection : MonoBehaviour
{
    public MaterialControl[] highlightables;

    void Update()
    {
        InteractRaycast();
    }

    void InteractRaycast()
    {
        Vector3 cameraPosition = transform.position;
        Vector3 forwardDirection = transform.forward;

        Ray interactionRay = new Ray(cameraPosition, forwardDirection);
        RaycastHit interactionRayHit;
        float interactionRayLength = 50.0f;

        Vector3 interactionRayEndpoint = forwardDirection * interactionRayLength;
        UnityEngine.Debug.DrawLine(cameraPosition, interactionRayEndpoint, Color.red);

        bool hitFound = Physics.Raycast(interactionRay, out interactionRayHit, interactionRayLength);
        if (hitFound)
        {
            
            GameObject hitGameobject = interactionRayHit.transform.gameObject;
            if (hitGameobject.CompareTag("Selectable"))
            {
                MaterialControl highlight = hitGameobject.GetComponent<MaterialControl>();
                string hitFeedback = hitGameobject.name;
                highlight.EnableMaterial();
                if (Input.GetMouseButtonDown(0))
                {
                    switch(hitFeedback)
                    {
                        case "Bulletin Board Selectable":
                            UnityEngine.Debug.Log("Bulletin Board Action");
                        break;
                    }
                }
            }
        }
    } 
}