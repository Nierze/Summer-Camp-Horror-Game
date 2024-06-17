using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannableInteraction : MonoBehaviour
{
    public ScanReveal[] reveal;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scannable"))
        {
            ScanReveal scanRevealComponent = other.GetComponent<ScanReveal>();
            if (scanRevealComponent != null)
            {
                scanRevealComponent.Reveal(true);
            }
        }
    }
}