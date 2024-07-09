using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetBehaviour : MonoBehaviour
{
    public bool open = false;
    public bool onMotion = false;

    public float timer = 0f;
    public float LerpSpeed = 1f;

    public float duration = 1.0f;

    public bool state = false;

    void Update()
    {
        /*if (Input.GetKeyDown("i") && !open && !onMotion)
        {
            open = true;
            onMotion = true;
            UnityEngine.Debug.Log("Cabinet Opened");
            StartCoroutine(RotateDoor(new Vector3(0, 90, 0)));
            state = true;
        }

        if (Input.GetKeyDown("c") && open && !onMotion)
        {
            open = false;
            onMotion = true;
            UnityEngine.Debug.Log("Cabinet Closed");
            StartCoroutine(RotateDoor(new Vector3(0, 0, 0)));
            state = false;
        }*/

        if (Input.GetKeyDown(KeyCode.F) && !onMotion)
        {
            if (!state)
            {
                StartCoroutine(RotateDoor(new Vector3(0, 90, 0)));
                onMotion = true;
            }
            else
            {
                StartCoroutine(RotateDoor(new Vector3(0, 0, 0)));
                onMotion = true;
            }
        }
    }

    private IEnumerator RotateDoor(Vector3 targetRotation)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(targetRotation);
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
            
        }
        state = (!state) ? true : false;
        onMotion = false;
        transform.rotation = endRotation;
    }
}
