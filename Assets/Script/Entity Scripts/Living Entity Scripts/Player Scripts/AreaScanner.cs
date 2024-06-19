using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaScanner : MonoBehaviour
{
    public static bool scanning = false;
    public TerrainScanner initiateScan;
    
    public GameObject scanRange;

    void Update()
    {
        if (!scanning && (Input.GetMouseButton(2) || Input.GetKeyDown("q")) )
        {
            StartCoroutine(multiPulses());
            Instantiate(scanRange, gameObject.transform.position, Quaternion.identity);
        }
    }
    
    private IEnumerator multiPulses()
    {
        initiateScan.SpawnTerrainScanner();
        yield return new WaitForSeconds(1f);
        initiateScan.SpawnTerrainScanner();
        yield return new WaitForSeconds(1f);
        initiateScan.SpawnTerrainScanner();
        yield return new WaitForSeconds(1f);

    }
    
}
