using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanReveal : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Light light;
    // [SerializeField]
    public GameObject itemAttributes;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        light = GetComponent<Light>();
        light.enabled = false;
        itemAttributes.SetActive(false);
    }

    public void Reveal(bool reveal)
    {
        if(reveal) meshRenderer.enabled = true;
        StartCoroutine(hide());
    }

    private IEnumerator hide()
    {
        light.enabled = true;
        itemAttributes.SetActive(true);
        yield return new WaitForSeconds(3f);
        meshRenderer.enabled = false;
        light.enabled = false;
        itemAttributes.SetActive(false);
    }
}
