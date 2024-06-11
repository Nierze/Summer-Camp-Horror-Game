using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InsanityMeterHandler : MonoBehaviour
{
    [SerializeField]
    private float maxInsanityMeter = 100f;
    
    [SerializeField]
    private float currrentInsanityMeter = 0f;
    
    [SerializeField]
    private float currentInsanityBuildUp = 0.1f;
    
    [SerializeField]
    private float currentInsanityBuildUpMultiplier = 0.5f;

    // Update is called once per frame
    void Update()
    {
        currrentInsanityMeter = (currrentInsanityMeter + CalculateInsanity()) < 0 ? 0 : currrentInsanityMeter + CalculateInsanity();
        Debug.Log("Insanity: " + currrentInsanityMeter);
    }

    float CalculateInsanity()
    {
        System.Random random = new System.Random();
        float insanityValue;
        
        currentInsanityBuildUp = (float)(random.NextDouble() + (random.NextDouble() * -1));
        insanityValue = (currentInsanityBuildUp * currentInsanityBuildUpMultiplier);

        return (float)Math.Round(insanityValue, 2);
    }
}
