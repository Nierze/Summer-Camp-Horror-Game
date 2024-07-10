using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchViewPerspective : MonoBehaviour
{
    public GameObject FPV;
    public GameObject TPV;
    public FPVPlayerControl FPVController;
    public TPVPlayerControl TPVController;
    public bool isFPV = true;


    void Start()
    {
        FPV = GameObject.Find("FPV");
        TPV = GameObject.Find("ThirdPV");
        FPVController = GameObject.Find("Armature").GetComponent<FPVPlayerControl>();
        TPVController = GameObject.Find("Armature").GetComponent<TPVPlayerControl>();

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
        FPV.SetActive(true);
        FPVController.enabled = true;
        TPV.SetActive(false);
        TPVController.enabled = false;
        isFPV = true;
    }

    void SwitchToTPV()
    {
        FPV.SetActive(false);
        FPVController.enabled = false;
        TPV.SetActive(true);
        TPVController.enabled = true;
        isFPV = false;
    }
}
