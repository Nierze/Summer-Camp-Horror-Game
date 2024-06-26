using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class OutlineSelection : MonoBehaviour
{
    //public MaterialControl[] highlightables;


    void Update()
    {
        InteractRaycast();
    }

    void InteractRaycast()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionRayHit;
        float interactionRayLength = 50.0f;

        UnityEngine.Debug.DrawRay(interactionRay.origin, interactionRay.direction * interactionRayLength, Color.red);

        if (Physics.Raycast(interactionRay, out interactionRayHit, interactionRayLength))
        {
            GameObject hitGameObject = interactionRayHit.transform.gameObject;
            if (hitGameObject.CompareTag("Enemy"))
            {
                //UnityEngine.Debug.Log("enemy hit");
                //UnityEngine.Debug.Log(hitGameObject.transform.position);
            }
        }
    }

}


/*
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
            if (hitGameobject.CompareTag("Enemy"))
            {
                UnityEngine.Debug.Log("enemy hit");
            }
        }
    }*/

/*
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
 */