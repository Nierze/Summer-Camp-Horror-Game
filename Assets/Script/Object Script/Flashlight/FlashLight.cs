using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] GameObject flashlightLight;
    [SerializeField] private bool flashlightActive = true;
    // Start is called before the first frame update
    void Start()
    {
        flashlightLight.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("clicked");
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
    }
}
