using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{

    public void TestLog()
    {
        UnityEngine.Debug.Log("Test debug");
    }

    public void GoToLevel1()
    {
        UnityEngine.Debug.Log("Goes to Level 1");
        SceneManager.LoadScene(2);
    }

    public void GoToLevel2()
    {
        UnityEngine.Debug.Log("Goes to Level 2");
        SceneManager.LoadScene(3);
    }

    public void GoToLevel3()
    {
        UnityEngine.Debug.Log("Goes to Level 3");
        SceneManager.LoadScene("Level 3");
    }
    
}
