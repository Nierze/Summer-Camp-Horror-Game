using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMoveAnimator : MonoBehaviour
{
    public Animator camAnimator;
    public static bool toBoardMove = false;
    public GameObject boardCanvas;
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
    }

    public void MoveToBoard()
    {
        MouseRayCast.inAction = true;
        state = true;
        StartCoroutine(delay(boardCanvas, state));
        camAnimator.SetBool("MoveToBoard", true);
    }

    private IEnumerator delay(GameObject boardCanvas, bool state)
    {
        yield return new WaitForSeconds(1.5f);
        boardCanvas.SetActive(state);
    }
}