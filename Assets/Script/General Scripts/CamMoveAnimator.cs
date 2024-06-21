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
        if(Input.GetKeyDown("m")) camAnimator.SetBool("MoveToBoard", false);


    }

    public void MoveToBoard()
    {
        camAnimator.SetBool("MoveToBoard", true);
    }
}


//if (toBoardMove) camAnimator.SetBool("MoveToBoard", true);
//else camAnimator.SetBool("MoveToBoard", false);

//if (Input.GetKey("m")) camAnimator.SetBool("MoveToBoard", true);
//else camAnimator.SetBool("MoveToBoard", false);