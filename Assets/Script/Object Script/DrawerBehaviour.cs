using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerBehaviour : MonoBehaviour
{
    public bool open = false;
    public bool onMotion = false;

    public Transform start;
    public Transform end;
    public float timer = 0f;
    public float LerpSpeed = 1f;

    public string setObjectInteraction;
    public SetObjectEnum setEnum;
    
    void Start()
    {
        setObjectInteraction = ObjectInteractEnum.objectInteraction.open.ToString();
        setEnum = GetComponent<SetObjectEnum>();
        setEnum.objectInteraction = setObjectInteraction;
        //UnityEngine.Debug.Log(setObjectInteraction);
    }

    void Update()
    {
        if (setEnum.enableOpen)
        {
            if (Input.GetKeyDown("i") && !open && !onMotion)
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
        setEnum.enableOpen = false;
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
        setEnum.enableOpen = false;
    }
}
