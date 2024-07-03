using UnityEngine;

public class PlayerCamRaycast : MonoBehaviour
{
    public Camera mainCamera;

    public MaterialControl[] highlightables;
    private MaterialControl currentHighlight = null;

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


        if (Physics.Raycast(interactionRay, out interactionRayHit, interactionRayLength))
        {
            GameObject hitGameObject = interactionRayHit.transform.gameObject;
            if (hitGameObject.CompareTag("Selectable"))
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