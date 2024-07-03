using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour
{
    [SerializeField] GameObject flashlightLight;
    [SerializeField] private bool flashlightActive = false;


    //Battery 
    public Light m_Light;
    public bool drainOverTime;
    public float maxBrightness;
    public float minBrightness;
    public float drainRate;

    //Blinking
    public float blinkAtThisTime;
    public float timer;

    public float Battery = 20f;
    public Image batteryImage;
    public Sprite battery0;
    public Sprite battery25;
    public Sprite battery50;
    public Sprite battery75;
    public Sprite battery100;


    // Start is called before the first frame update
    void Start()
    {
        flashlightLight.gameObject.SetActive(false);
        flashlightActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckBattery(Battery);
        if (Battery <= 0)
        {
            m_Light.enabled = false;
            Battery = 0;
        }
        
        else
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (flashlightActive == false)
                {
                    Debug.Log("ON");
                    flashlightLight.gameObject.SetActive(true);
                    flashlightActive = true;
                }
                else
                {
                    Debug.Log("OFF");
                    flashlightLight.gameObject.SetActive(false);
                    flashlightActive = false;
                }

            }

            m_Light.intensity = Mathf.Clamp(m_Light.intensity, minBrightness, maxBrightness);

            if (drainOverTime == true && flashlightActive == true)
            {
                Battery -= Time.deltaTime;
                if (m_Light.intensity > minBrightness)
                {
                    m_Light.intensity -= Time.deltaTime * (drainRate / 1000);
                }

                if (Battery < 25)
                {
                    Debug.Log(timer);
                    timer += Time.deltaTime;
                    if (timer > blinkAtThisTime)
                    {
                        StartCoroutine("Blink");
                        timer = 0;
                    }
                }
            }
        }
      

    }

    IEnumerator Blink()
    {
        m_Light.enabled = false;
        yield return new WaitForSeconds(.2f);
        m_Light.enabled = true;
        yield return new WaitForSeconds(.1f);
        m_Light.enabled = false;
        yield return new WaitForSeconds(.13f);
        m_Light.enabled = true;
    }


    private void CheckBattery(float Battery)
    {
        if (Battery <= 0)
        {
            batteryImage.sprite = battery0;
        }
        else if (Battery < 25)
        {
            batteryImage.sprite = battery25;
        }

        else if (Battery < 50)
        {
            batteryImage.sprite = battery50;
        }

        else if (Battery < 75)
        {
            batteryImage.sprite = battery75;
        }
        else
        {
            batteryImage.sprite = battery100;
        }
    }
    public void AddBattery (float amout)
    {
        Battery += amout;

        if (Battery >= 100)
        {
            Battery = 100;
        }
    }
}
