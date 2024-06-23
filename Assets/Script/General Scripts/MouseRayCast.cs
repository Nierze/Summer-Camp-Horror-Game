using UnityEngine;

public class MouseRayCast : MonoBehaviour
{
    public MaterialControl[] highlightables;
    public bool enableMouseRayCast = true;

    public CamMoveAnimator camMove;
    public static bool inAction = false;
    private MaterialControl currentHighlight = null;

    void Update()
    {
        if (enableMouseRayCast) InteractRaycast();
    }

    void InteractRaycast()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionRayHit;
        float interactionRayLength = 50.0f;

        Debug.DrawRay(interactionRay.origin, interactionRay.direction * interactionRayLength, Color.red);

        if (Physics.Raycast(interactionRay, out interactionRayHit, interactionRayLength))
        {
            GameObject hitGameObject = interactionRayHit.transform.gameObject;
            if (hitGameObject.CompareTag("Selectable"))
            {
                MaterialControl highlight = hitGameObject.GetComponent<MaterialControl>();
                string hitFeedback = hitGameObject.name;

                if (highlight != null)
                {
                    //Debug.Log("highlight = " + highlight);
                    //Debug.Log("hitFeedback = " + hitFeedback);

                    if (!inAction && currentHighlight != highlight)
                    {
                        if (currentHighlight != null)
                        {
                            currentHighlight.DisableMaterial();
                        }
                        highlight.EnableMaterial();
                        currentHighlight = highlight;
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        switch (hitFeedback)
                        {
                            case "Bulletin Board Selectable":
                                //Debug.Log("Bulletin Board Action");
                                camMove.MoveToBoard(highlight);
                                break;
                        }
                    }
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
}

// using System.Diagnostics;
// using UnityEngine;

// public class MouseRayCast : MonoBehaviour
// {
//     public MaterialControl[] highlightables;
//     public bool enableMouseRayCast = true;

//     public CamMoveAnimator camMove;
//     public static bool inAction = false;

//     void Update()
//     {
//         if(enableMouseRayCast) InteractRaycast();
//     }

//     void InteractRaycast()
//     {
//         Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
//         RaycastHit interactionRayHit;
//         float interactionRayLength = 50.0f;

//         UnityEngine.Debug.DrawRay(interactionRay.origin, interactionRay.direction * interactionRayLength, Color.red);

//         if (Physics.Raycast(interactionRay, out interactionRayHit, interactionRayLength))
//         {
//             GameObject hitGameobject = interactionRayHit.transform.gameObject;
//             if (hitGameobject.CompareTag("Selectable"))
//             {
//                 MaterialControl highlight = hitGameobject.GetComponent<MaterialControl>();
//                 string hitFeedback = hitGameobject.name;
//                 UnityEngine.Debug.Log("highlight = " + highlight);
//                 UnityEngine.Debug.Log("hitFeedback = " + hitFeedback);
//                 if(!inAction) highlight.EnableMaterial();

//                 if (Input.GetMouseButtonDown(0))
//                 {
//                     switch (hitFeedback)
//                     {
//                         case "Bulletin Board Selectable":
//                             UnityEngine.Debug.Log("Bulletin Board Action");
//                             camMove.MoveToBoard(highlight);

//                             //enableMouseRayCast = false;
//                         break;
//                     }
//                 }
//             }
            
//         }
//     }
// }