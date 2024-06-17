using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanReveal : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Light light;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        light = GetComponent<Light>();
        light.enabled = false;
    }

    public void Reveal(bool reveal)
    {
        if(reveal) meshRenderer.enabled = true;
        StartCoroutine(hide());
    }

    private IEnumerator hide()
    {
        light.enabled = true;
        yield return new WaitForSeconds(3f);
        meshRenderer.enabled = false;
        light.enabled = false;
    }
}
