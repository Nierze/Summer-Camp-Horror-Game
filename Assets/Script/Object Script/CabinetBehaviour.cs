using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetBehaviour : MonoBehaviour
{
    public bool open = false;
    public bool onMotion = false;

    public Transform start;
    public Transform end;
    public float timer = 0f;
    public float LerpSpeed = 1f;

    void Update()
    {
        if (Input.GetKeyDown("o") && !open && !onMotion)
        {
            open = true;
            onMotion = true;
            UnityEngine.Debug.Log("Drawer Opened");
            StartCoroutine(Open());
        }

        if (Input.GetKeyDown("c") && open && !onMotion)
        {
            open = false;
            onMotion = true;
            UnityEngine.Debug.Log("Drawer Closed");
            StartCoroutine(Close());
        }
    }

    private IEnumerator Open()
    {
        timer = 0f;
        while (timer < 1)
        {
            transform.position = Vector3.Lerp(start.position, end.position, timer);
            timer += Time.deltaTime * LerpSpeed;
            yield return null;
        }
        onMotion = false;
    }

    private IEnumerator Close()
    {
        timer = 0f;
        while (timer < 1)
        {
            transform.position = Vector3.Lerp(end.position, start.position, timer);
            timer += Time.deltaTime * LerpSpeed;
            yield return null;
        }
        onMotion= false;
    }
}
