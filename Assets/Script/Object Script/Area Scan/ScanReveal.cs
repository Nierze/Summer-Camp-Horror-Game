using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScanReveal : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Light light;
    public GameObject itemAttributes;
    public TextMeshPro textComponent;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        light = GetComponent<Light>();

        GameObject parent = transform.parent.gameObject;
        itemAttributes = parent.transform.Find("Text (TMP)").gameObject;

        light.enabled = false;
        itemAttributes.SetActive(false);
        textComponent = itemAttributes.GetComponent<TextMeshPro>();
        textComponent.text = gameObject.name;
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
        //meshRenderer.enabled = false;
        light.enabled = false;
        itemAttributes.SetActive(false);
    }
}
