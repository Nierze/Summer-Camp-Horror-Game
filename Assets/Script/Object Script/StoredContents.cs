using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredContents : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Material transparentMaterial;
    private Material originalMaterial;
    private bool materialEnabled = true;

    private bool enableDisableMaterial = false;

    Material[] materials;

    public List<GameObject> contents;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        materials = meshRenderer.materials;
        if (materials.Length > 1)
        {
            originalMaterial = materials[materials.Length - 1];
            transparentMaterial = materials[0];
        }

        DisableMaterial();
        CountContents();
    }

    public void DisableMaterial()
    {
        materials[materials.Length - 1] = transparentMaterial;
        meshRenderer.materials = materials;
    }

    public void EnableMaterial()
    {
        materials[materials.Length - 1] = originalMaterial;
        meshRenderer.materials = materials;
    }

    public void CountContents()
    {
        contents.Clear();
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            contents.Add(gameObject.transform.GetChild(i).gameObject);
        }

        foreach (GameObject content in contents)
        {
            UnityEngine.Debug.Log("content = " + content);
        }
    }

    public IEnumerator DisableHighlight()
    {
        yield return new WaitForSeconds(3f);
        CountContents();
        DisableMaterial();
    }
}
