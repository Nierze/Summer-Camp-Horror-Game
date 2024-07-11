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

    public string setObjectInteraction;
    public SetObjectEnum setEnum;

    public List<GameObject> contents;

    void Start()
    {
        setObjectInteraction = ObjectInteractEnum.objectInteraction.open.ToString();
        setEnum = GetComponent<SetObjectEnum>();
        setEnum.objectInteraction = setObjectInteraction;

        UpdateContent();
    }

    void Update()
    {
        if (setEnum.enableOpen)
        {
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
        setEnum.enableOpen = false;
        transform.rotation = endRotation;
        
    }

    void UpdateContent()
    {
        contents.Clear();
        GameObject drawerSection = gameObject.transform.GetChild(0).gameObject;
        int childCount = drawerSection.transform.childCount;
        UnityEngine.Debug.Log(childCount);
        for (int i = 0; i < childCount; i++)
        {
            contents.Add(drawerSection.transform.GetChild(i).gameObject);
            UnityEngine.Debug.Log(contents[i].name);
        }


    }
}
