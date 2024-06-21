using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMoveAnimator : MonoBehaviour
{
    public Animator camAnimator;
    public static bool toBoardMove = false;

    void Start()
    {
        camAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            MouseRayCast.inAction = false;
            camAnimator.SetBool("MoveToBoard", false);
        }
    }

    public void MoveToBoard()
    {
        MouseRayCast.inAction = true;
        camAnimator.SetBool("MoveToBoard", true);
    }
}
