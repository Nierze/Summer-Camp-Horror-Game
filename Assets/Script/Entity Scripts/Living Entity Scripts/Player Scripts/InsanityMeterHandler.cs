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
    private float currentInsanityBuildUp = 1f;
    
    [SerializeField]
    private float currentInsanityBuildUpMultiplier = 1f;

    // Update is called once per frame
    void Update()
    {
        // currrentInsanityMeter = currrentInsanityMeter + CalculateInsanity();
        // if (currentInsanityBuildUp <= 0 ) {
        //     currrentInsanityMeter -= 0.01f;
        // }
        // if (currrentInsanityMeter <= 0) {
        //     currrentInsanityMeter = 0;
        // }
        // Debug.Log("Insanity: " + Math.Round(currrentInsanityMeter,2));
        // Debug.Log("Rand" + UnityEngine.Random.Range(0f, currentInsanityBuildUp));
    }

    float CalculateInsanity()
    {
        float randomValue1 = UnityEngine.Random.Range(0f, currentInsanityBuildUp);
        float randomValue2 = UnityEngine.Random.Range(0f, currentInsanityBuildUp);

        float insanityValue = randomValue1 + (randomValue2*-1);

        return insanityValue * currentInsanityBuildUpMultiplier;
    }
}
