using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevelManager : MonoBehaviour
{
    public void Start()
    {
        //Cursor.visible = true; //prev en
        Cursor.visible = false; //prev dis
    }

    // Update is called once per frame
    public void Pause()
    {
        Cursor.visible = true; //prev dis
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        Cursor.visible = false; //prev dis
        Time.timeScale = 1;
    }
}
