using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaScanner : MonoBehaviour
{
    public static bool scanning = false;
    // private Vector3 initialScale = new Vector3(0f, 0f, 0f);
    // private SphereCollider collider;
    public TerrainScanner initiateScan;
    
    public GameObject scanRange;
    
    // void Start()
    // {
    //     // initialScale = transform.localScale;
    //     // collider = GetComponent<SphereCollider>();
    //     // collider.enabled = false;
    // }

    void Update()
    {
        if (!scanning && (Input.GetMouseButton(2) || Input.GetKeyDown("q")) )
        {
            initiateScan.SpawnTerrainScanner();
            Instantiate(scanRange, gameObject.transform.position, Quaternion.identity);
            //StartCoroutine(InitiateScan());
        }
    }

    // private IEnumerator InitiateScan()
    // {
    //     collider.enabled = true;
    //     scanning = true;
    //     UnityEngine.Debug.Log("Casting");

    //     Vector3 targetScale = new Vector3(100f, 100f, 100f);

    //     float elapsedTime = 0f;
    //     while (elapsedTime < 3f)
    //     {
    //         transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / 3f);
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }

    //     yield return new WaitForSeconds(3f);

    //     transform.localScale = initialScale;
    //     scanning = false;
    //     collider.enabled = false;
    // }
}
