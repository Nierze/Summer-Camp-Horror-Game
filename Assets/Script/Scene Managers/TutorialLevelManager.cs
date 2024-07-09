using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevelManager : MonoBehaviour
{
    public void Start()
    {
        Cursor.visible = true;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    public void Pause()
    {
        //Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        //Cursor.visible = false;
        Time.timeScale = 1;
    }
}
