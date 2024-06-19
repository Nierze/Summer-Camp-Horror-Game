using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaScanner : MonoBehaviour
{
    public static bool scanning = false;
    public TerrainScanner initiateScan;
    public Vector3 castPosition;

    public GameObject scanRange;

    void Update()
    {
        if (!scanning && (Input.GetMouseButton(2) || Input.GetKeyDown("q")) )
        {
            castPosition = gameObject.transform.position;
            StartCoroutine(multiPulses(castPosition));
            Instantiate(scanRange, gameObject.transform.position, Quaternion.identity);
        }
    }
    
    private IEnumerator multiPulses(Vector3 castPosition)
    {
        initiateScan.SpawnTerrainScanner(castPosition);
        yield return new WaitForSeconds(1f);
        initiateScan.SpawnTerrainScanner(castPosition);
        yield return new WaitForSeconds(1f);
        initiateScan.SpawnTerrainScanner(castPosition);
        yield return new WaitForSeconds(1f);

    }
    
}
