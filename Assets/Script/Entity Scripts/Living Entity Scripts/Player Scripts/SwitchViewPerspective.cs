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
    public SkinnedMeshRenderer playerModel;

    void Start()
    {
        FPV = GameObject.Find("FPV");
        TPV = GameObject.Find("ThirdPV");
        FPVController = GameObject.Find("Armature").GetComponent<FPVPlayerControl>();
        TPVController = GameObject.Find("Armature").GetComponent<TPVPlayerControl>();
        playerModel = GameObject.Find("Armature_Mesh").GetComponent<SkinnedMeshRenderer>();
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
        playerModel.enabled = false;
        FPV.SetActive(true);
        FPVController.enabled = true;
        TPV.SetActive(false);
        TPVController.enabled = false;
        isFPV = true;
    }

    void SwitchToTPV()
    {
        playerModel.enabled = true;
        FPV.SetActive(false);
        FPVController.enabled = false;
        TPV.SetActive(true);
        TPVController.enabled = true;
        isFPV = false;
    }
}
