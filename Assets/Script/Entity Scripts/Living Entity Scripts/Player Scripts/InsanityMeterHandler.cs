using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InsanityMeterHandler : MonoBehaviour
{   

    //////////////////////////////////////
    // General Variables
    [SerializeField]
    private float maxInsanityMeter = 100f;
    
    [SerializeField]
    private float currrentInsanityMeter = 0f;
    
    [SerializeField]
    private float currentInsanityBuildUp = 1f;
    
    [SerializeField]
    private float currentInsanityBuildUpMultiplier = 1f;

    //////////////////////////////////////
    // Insanity UI Variables
    public RectTransform insanityMeter;
    float percentUnit;
    float insanityPercentUnit;

    //////////////////////////////////////
    // Fade Variables
    [Header("Fade Settings")]
    public Image parentImage;
    public float targetTransparencyFade = 0.0f;
    public float targetTransparencyAppear = 1f;
    public float fadeDuration = 1.0f;
    public float timeBeforeFade = 2f;

    // Private variables
    private float timeElapsed = 0f;


    // Update is called once per frame

    void Start()
    {
        percentUnit = 1f / insanityMeter.anchorMax.x;
        insanityPercentUnit = 100f / maxInsanityMeter;
    }
    void Update()
    {

        ///////////////////////////////////////
        // Threshold 

        // If the current insanity build up is greater than 0, add to the insanity meter
        currrentInsanityMeter = currrentInsanityMeter + CalculateInsanity();
        if (currentInsanityBuildUp <= 0 ) {
            currrentInsanityMeter -= 0.04f;
        }
        if (currrentInsanityMeter <= 0) {
            currrentInsanityMeter = 0;
        }
        if (currrentInsanityMeter >= maxInsanityMeter) {
            currrentInsanityMeter = maxInsanityMeter;
        }



        // Update the insanity meter UI
        float currentInsanityPercent = currrentInsanityMeter * insanityPercentUnit;

        insanityMeter.anchorMax = new Vector2((currentInsanityPercent * percentUnit) / 100f, insanityMeter.anchorMax.y);

        // Debug
        Debug.Log("Insanity: " + currentInsanityPercent);

        // Fade
        if (currentInsanityBuildUp == 0) {

            if (timeElapsed >= timeBeforeFade && currrentInsanityMeter == 0) {
                StartCoroutine(FadeTransparency(parentImage, targetTransparencyFade, fadeDuration));
                timeElapsed = 0f;
            } else if (!(timeElapsed >= timeBeforeFade) && currrentInsanityMeter == 0) {
                timeElapsed += Time.deltaTime;
            }
        } else if (currentInsanityBuildUp > 0 && parentImage.color.a == 0.0) {
            StartCoroutine(FadeTransparency(parentImage, targetTransparencyAppear, fadeDuration));
            timeElapsed = 0f;
        }
        
        


        //Debug.Log("Insanity: " + Math.Round(currrentInsanityMeter,2));
        //Debug.Log("Rand" + UnityEngine.Random.Range(0f, currentInsanityBuildUp));

    }

    ///////////////////////////////////////
    // Insanity Functions
    float CalculateInsanity()
    {
        float randomValue1 = UnityEngine.Random.Range(0f, currentInsanityBuildUp);
        float randomValue2 = UnityEngine.Random.Range(0f, currentInsanityBuildUp);

        float insanityValue = randomValue1 + (randomValue2*-1);

        return insanityValue * currentInsanityBuildUpMultiplier;
    }


    ///////////////////////////////////////
    // Fade Functions
    IEnumerator FadeTransparency(Image image, float targetAlpha, float duration)
    {
        float startAlpha = image.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            SetAlpha(image, newAlpha);
            yield return null;
        }

        SetAlpha(image, targetAlpha);
    }

    void SetAlpha(Image image, float alpha)
    {
        // Change the transparency of the current image
        Color color = image.color;
        color.a = alpha;
        image.color = color;

        // Recursively change the transparency of child images
        foreach (Transform child in image.transform)
        {
            Image childImage = child.GetComponent<Image>();
            if (childImage != null)
            {
                Color childColor = childImage.color;
                childColor.a = alpha;
                childImage.color = childColor;
            }
        }
    }

}
