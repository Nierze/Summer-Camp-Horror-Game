using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchViewPerspective : MonoBehaviour
{
    public GameObject FPV;
    public GameObject TPV;
    public PlayerControllerCopy FPVController;
    public NewPlayerController TPVController;
    public bool isFPV = true;


    void Start()
    {
        SwitchToFPV();
    }

    void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            SwitchToTPV();
        }
        
        if (Input.GetKeyDown("i"))
        {
            SwitchToFPV();
        }
    }

    void SwitchToFPV()
    {
        FPV.SetActive(false);
        FPVController.enabled = false;
        TPV.SetActive(true);
        TPVController.enabled = true;
        isFPV = true;
    }

    void SwitchToTPV()
    {
        FPV.SetActive(true);
        FPVController.enabled = true;
        TPV.SetActive(false);
        TPVController.enabled = false;
        isFPV = false;
    }
}
