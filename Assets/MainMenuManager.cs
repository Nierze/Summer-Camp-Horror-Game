using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public Animator mainMenuAnimator;
    public Animator settingsAnimator;
    public Animator introAnimator;
    public Animator DSAnimator;

    public GameObject mainMenu;
    public GameObject settings;
    public GameObject startButton;
    public GameObject DS;
    public GameObject DSPanel;

    private bool introBool;
    private bool settingsBool;
    private bool DSBool;
    // Start is called before the first frame update
    void Start()
    {
        introBool = false;
        settingsBool = false;
        DSBool = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey && !introBool && startButton.activeSelf)
        {
            StartCoroutine(PressAnyKey());
        }
    }

    public void Settings()
    {
        if (!settingsBool) StartCoroutine(OpenSettings());
        else if (settingsBool) StartCoroutine(CloseSettings());
    }

    public void DifficultySelector()
    {
        if (!DSBool) StartCoroutine(OpenDS());
        else if (DSBool) StartCoroutine(CloseDS());
    }

    public IEnumerator PressAnyKey()
    {
        introAnimator.SetTrigger("PressAnyKey");
        mainMenu.SetActive(true);
        yield return new WaitForSeconds(1f);
        mainMenuAnimator.SetTrigger("Fade In Menu");
    }

    public IEnumerator OpenSettings()
    {
        mainMenuAnimator.SetTrigger("Fade Out Menu");
        yield return new WaitForSeconds(1f);
        settings.SetActive(true);
        settingsAnimator.SetTrigger("Open Settings");
        settingsBool = true; 
        mainMenu.SetActive(false);
    }

    public IEnumerator CloseSettings()
    {
        settingsAnimator.SetTrigger("Close Settings");
        yield return new WaitForSeconds(1f);
        mainMenu.SetActive(true);
        mainMenuAnimator.SetTrigger("Fade In Menu");
        settingsBool = false;
        settings.SetActive(false);
    }

    public IEnumerator OpenDS()
    {
        mainMenuAnimator.SetTrigger("Fade Out Menu");
        yield return new WaitForSeconds(1f);
        DS.SetActive(true);
        DSAnimator.SetTrigger("Open DS");
        DSBool = true;
        mainMenu.SetActive(false);
    }

    public IEnumerator CloseDS()
    {
        DSAnimator.SetTrigger("Close DS");
        yield return new WaitForSeconds(1f);
        mainMenu.SetActive(true);
        mainMenuAnimator.SetTrigger("Fade In Menu");
        DSBool = false;
        DS.SetActive(false);
        DSPanel.SetActive(false);
    }

    public IEnumerator MainMenuFadeOut()
    {
        mainMenuAnimator.SetTrigger("Fade Out Menu");
        yield return new WaitForSeconds(0.3f);
        mainMenu.SetActive(false);
    }

    public IEnumerator MainMenuFadeIn()
    {
        mainMenu.SetActive(true);
        mainMenuAnimator.SetTrigger("Fade In Menu");
        mainMenuAnimator.ResetTrigger("Fade Out Menu");
        yield return new WaitForSeconds(0.3f);
    }

}
