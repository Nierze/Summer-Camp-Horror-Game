using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaScanRange : MonoBehaviour
{
    private Vector3 initialScale = new Vector3(0f, 0f, 0f);
    private SphereCollider collider;

    void Start()
    {
        initialScale = transform.localScale;
        collider = GetComponent<SphereCollider>();
        StartCoroutine(InitiateScan());
    }

    private IEnumerator InitiateScan()
    {
        collider.enabled = true;
        AreaScanner.scanning = true;
        UnityEngine.Debug.Log("Casting");

        Vector3 targetScale = new Vector3(100f, 100f, 100f);

        float elapsedTime = 0f;
        while (elapsedTime < 3f)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / 3f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        transform.localScale = initialScale;
        AreaScanner.scanning = false;
        collider.enabled = false;
        Destroy(gameObject, 1f);
    }
}
