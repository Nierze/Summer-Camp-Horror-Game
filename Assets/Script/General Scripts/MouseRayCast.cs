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

                            case "Laptop Selectable":
                                Debug.Log("Laptop Action");
                                camMove.MoveToLaptop(highlight);
                            break;
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