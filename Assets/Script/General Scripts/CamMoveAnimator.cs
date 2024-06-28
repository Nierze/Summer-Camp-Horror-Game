using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMoveAnimator : MonoBehaviour
{
    public Animator camAnimator;
    public static bool toBoardMove = false;
    public GameObject boardCanvas;
    public static bool toLaptopMove = false;
    public GameObject laptopCanvas;
    public bool state = false;
    void Start()
    {
        camAnimator = GetComponent<Animator>();
        boardCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            MouseRayCast.inAction = false;
            state = false;
            StartCoroutine(delay(boardCanvas, state));
            camAnimator.SetBool("MoveToBoard", false);
        }
        
        if(Input.GetKeyDown("l"))
        {
            MouseRayCast.inAction = false;
            state = false;
            StartCoroutine(delay(laptopCanvas, state));
            camAnimator.SetBool("MoveToLaptop", false);
        }
    }

    public void MoveToBoard(MaterialControl highlight)
    {
        //highlight.DisableMaterial();
        MouseRayCast.inAction = true;
        state = true;
        StartCoroutine(delay(boardCanvas, state));
        camAnimator.SetBool("MoveToBoard", true);
    }

    public void MoveToLaptop(MaterialControl highlight)
    {
        MouseRayCast.inAction = true;
        state = true;
        StartCoroutine(delay(laptopCanvas, state));
        camAnimator.SetBool("MoveToLaptop", true);
    }

    private IEnumerator delay(GameObject CanvasToActive, bool state)
    {
        yield return new WaitForSeconds(1.5f);
        CanvasToActive.SetActive(state);
    }
}