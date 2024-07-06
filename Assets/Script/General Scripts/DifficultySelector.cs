using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum difficulty
{
    easy,
    normal,
    hard
}

public class DifficultySelector : MonoBehaviour
{
    public static string setDifficulty;
    private string difficultyDescription;
    private string difficultyTitle;

    public GameObject difficultyConfirmationWindow;

    public TextMeshProUGUI description;
    public TextMeshProUGUI title;

    public Animator panelAnimator;

    private bool windowActive;

    public void Start()
    {
        windowActive = false;
        panelAnimator.ResetTrigger("Show Panel");
    }

    public void Update()
    {
        if (difficultyConfirmationWindow.activeSelf == false)
        {
            windowActive = false;
        }
    }

    public void selectEasy()
    {
        setDifficulty = difficulty.easy.ToString();
        UnityEngine.Debug.Log("difficulty set to " + setDifficulty);
        //SceneManager.LoadScene(1);

        difficultyDescription = "Are you a new to the field of being a SPI? We have openings for you as an INTERN! Here, you'll be exposed to the workings of the freelance work and its nature with more forgiving circumstances.";
        difficultyTitle = "Intern Level Investigator";

        description.text = difficultyDescription;
        title.text = difficultyTitle;

        if (!windowActive)
        {
            difficultyConfirmationWindow.SetActive(true);
            windowActive = true;
            panelAnimator.SetTrigger("Show Panel");
        }
    }

    public void selectNormal()
    {
        setDifficulty = difficulty.normal.ToString();
        UnityEngine.Debug.Log("difficulty set to " + setDifficulty);
        // SceneManager.LoadScene(1);

        difficultyDescription = "This level of work is more suited for those who are already experienced being a Supernatural Paranormal Investigator. The risks are apparent and more life-threatening. Only those who are aware to the intensity of this line of work are worthy of reaching Senior Level payments and commissions.";
        difficultyTitle = "Senior Level Investigator";

        description.text = difficultyDescription;
        title.text = difficultyTitle;

        if (!windowActive)
        {
            difficultyConfirmationWindow.SetActive(true);
            windowActive = true;
            panelAnimator.SetTrigger("Show Panel");
        }
    }

    public void selectHard()
    {
        setDifficulty = difficulty.hard.ToString();
        UnityEngine.Debug.Log("difficulty set to " + setDifficulty);
        // SceneManager.LoadScene(1);

        difficultyDescription = "Fight to live; you are the last in line between the living and the malicious dead. Take on the nightmares that not even a group of Senior-Level Investigators can handle. Protect the Haunted. Survive the Night. Become the field's living legend.";
        difficultyTitle = "Modern Folklore Legend";

        description.text = difficultyDescription;
        title.text = difficultyTitle;

        if (!windowActive)
        {
            difficultyConfirmationWindow.SetActive(true);
            windowActive = true;
            panelAnimator.SetTrigger("Show Panel");
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(5);
    }
}
