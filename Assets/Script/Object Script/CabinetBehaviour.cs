using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetBehaviour : MonoBehaviour
{
    public bool open = false;

    public Transform start;
    public Transform end;
    public float timer = 0f;
    public float LerpSpeed = 1f;

    void Update()
    {
        if (Input.GetKeyDown("o") && !open)
        {
            open = true;
            UnityEngine.Debug.Log("Drawer Opened");
            StartCoroutine(Open());
        }

        if (Input.GetKeyDown("c") && open)
        {
            open = false;
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
        
    }

    private IEnumerator Close()
    {
        timer = 0f;
        while (timer < 1)
        {
            transform.position = Vector3.Lerp(transform.position, start.position, timer);
            timer += Time.deltaTime * LerpSpeed;
            yield return null;
        }
        
    }
}
