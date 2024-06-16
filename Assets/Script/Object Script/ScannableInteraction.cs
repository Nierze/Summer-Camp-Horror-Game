using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannableInteraction : MonoBehaviour
{
    public ScanReveal reveal;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scannable"))
        {
            UnityEngine.Debug.Log(new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z));
            reveal.Reveal(true);
        }
    }
}
