using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FanOn : MonoBehaviour
{
    public Transform propeller;
    [SerializeField] public float rotationSpeed = 1000f;

    void Update()
    {
        propeller.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
