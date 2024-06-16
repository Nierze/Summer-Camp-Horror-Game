using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaScanner : MonoBehaviour
{
    private bool scanning = false;
    private Vector3 initialScale = new Vector3(0f, 0f, 0f);

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (!scanning && Input.GetMouseButton(2))
        {
            StartCoroutine(InitiateScan());
        }
    }

    private IEnumerator InitiateScan()
    {
        scanning = true;
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
        scanning = false; 
    }
}
