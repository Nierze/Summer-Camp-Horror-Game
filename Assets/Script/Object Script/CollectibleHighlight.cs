using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CollectibleHighlight : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Material transparentMaterial;
    private Material originalMaterial;
    private bool materialEnabled = true;

    private bool enableDisableMaterial = false;

    Material[] materials;

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

    public IEnumerator DisableHighlight()
    {
        yield return new WaitForSeconds(3f);
        DisableMaterial();
    }

}