using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadbobController : MonoBehaviour
{
    [SerializeField] private bool isEnabled = true;
    [SerializeField, Range(0, 0.1f)] private float amplitude = 0.015f;
    //[SerializeField, Range(0, 30)]
}
