using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScanner : MonoBehaviour
{
    public GameObject TerrainScannerPrefab;
    public float duration = 3.75f;
    public float size = 100f;

    void Start()
    {
        if (!AreaScanner.scanning && (Input.GetMouseButton(2) || Input.GetKeyDown("q")) )
        {
            SpawnTerrainScanner();
        } 
    }

    public void SpawnTerrainScanner()
    {
        GameObject terrainScanner = Instantiate(TerrainScannerPrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
        ParticleSystem terrainScannerPS = terrainScanner.transform.GetChild(0).GetComponent<ParticleSystem>();

        if(terrainScannerPS != null)
        {
            var main = terrainScannerPS.main;
            main.startLifetime = duration;
            main.startSize = size;
        }

        Destroy(terrainScanner, duration+1);
    }
}
