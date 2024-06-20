using System.Diagnostics;
using UnityEngine;

public class MouseRayCast : MonoBehaviour
{
    public MaterialControl[] highlightables;
    public bool enableMouseRayCast = true;

    void Update()
    {
        if(enableMouseRayCast) InteractRaycast();
    }

    void InteractRaycast()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionRayHit;
        float interactionRayLength = 50.0f;

        UnityEngine.Debug.DrawRay(interactionRay.origin, interactionRay.direction * interactionRayLength, Color.red);

        if (Physics.Raycast(interactionRay, out interactionRayHit, interactionRayLength))
        {
            GameObject hitGameobject = interactionRayHit.transform.gameObject;
            if (hitGameobject.CompareTag("Selectable"))
            {
                MaterialControl highlight = hitGameobject.GetComponent<MaterialControl>();
                string hitFeedback = hitGameobject.name;
                highlight.EnableMaterial();

                if (Input.GetMouseButtonDown(0))
                {
                    switch (hitFeedback)
                    {
                        case "Bulletin Board Selectable":
                            UnityEngine.Debug.Log("Bulletin Board Action");

                            //enableMouseRayCast = false;
                        break;
                    }
                }
            }
        }
    }
}