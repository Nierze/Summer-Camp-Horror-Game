using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FanRotate : MonoBehaviour
{
    public Transform fanHead;
    [SerializeField] public float rotationSpeed = 25f;
    public float time = 0f;
    
    void Update()
    {
        time += Time.deltaTime;
        if (time <= 4)
            fanHead.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        else if (time >= 8f)
            time = 0f;
        else
            fanHead.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
    }

}
