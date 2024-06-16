using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanReveal : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    public void Reveal(bool reveal)
    {
        if(reveal) meshRenderer.enabled = true;
        StartCoroutine(hide());
    }

    private IEnumerator hide()
    {
        yield return new WaitForSeconds(3f);
        meshRenderer.enabled = false;
    }
}
