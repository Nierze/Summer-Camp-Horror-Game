using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MaterialControl : MonoBehaviour
{
    public GameObject parentObject;
    public MeshRenderer meshRenderer;
    public Material transparentMaterial;
    private Material originalMaterial;
    private bool materialEnabled = true;

    private bool enableDisableMaterial = false;
    
    Material[] materials;

    void Start()
    {
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
}