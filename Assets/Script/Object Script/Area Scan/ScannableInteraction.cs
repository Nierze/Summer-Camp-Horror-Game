using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannableInteraction : MonoBehaviour
{
    public ScanReveal[] reveal;

    public MaterialControl[] highlightables;
    private MaterialControl currentHighlight = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scannable") || other.CompareTag("CanHold") || other.CompareTag("CanTake"))
        {
            ScanReveal scanRevealComponent = other.GetComponent<ScanReveal>();
            if (scanRevealComponent != null)
            {
                scanRevealComponent.Reveal(true);
            }

            StoredContents storedContents = other.GetComponent<StoredContents>();
            if (storedContents != null)
            {
                storedContents.CountContents();
                if (storedContents.contents.Count != 0)
                {
                    storedContents.EnableMaterial();
                    storedContents.StartCoroutine(storedContents.DisableHighlight());
                }
            }
        }

        else if (other.CompareTag("Selectable"))
        {
            UnityEngine.Debug.Log(other.name + " scanned");

            MaterialControl highlight = other.GetComponent<MaterialControl>();
            string hitFeedback = other.name;
            string tag = "CanBePickUP";

            GameObject parent = other.gameObject;
            GameObject child = FindChildWithTag(parent, tag);
            if (child != null)
            {
                if (highlight != null)
                {
                    if (currentHighlight != highlight)
                    {
                        if (currentHighlight != null)
                        {
                            currentHighlight.DisableMaterial();
                        }
                        highlight.EnableMaterial();
                        currentHighlight = highlight;
                        StartCoroutine(ScanDisableHighlight());
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
    }

    private IEnumerator ScanDisableHighlight()
    {
        yield return new WaitForSeconds(3f);
        currentHighlight.DisableMaterial();
        currentHighlight = null;
    }

    GameObject FindChildWithTag(GameObject parent, string tag)
    {
        GameObject child = null;

        foreach (Transform transform in parent.transform)
        {
            if (transform.CompareTag(tag))
            {
                child = transform.gameObject;
                break;
            }
        }

        return child;
    }
}

