using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject backgroundMusic;
    private static GameObject bgMusicInstant;
    public GameObject mainM;
    public GameObject optionsM;
    public Toggle ScreenToggle;
    bool isFullScreen;
    Animator menuAnimator;
    void Start()
    {
        isFullScreen = ScreenToggle.isOn;
        menuAnimator = GetComponent<Animator>();
        //bgMusicInstant = GameObject.FindGameObjectWithTag("Sound");
        //if(backgroundMusic.active == true)
        //{
        //    DestroyImmediate(bgMusicInstant);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OptionsClick()
    {
        menuAnimator.Play("MoveCamera");
        mainM.SetActive(false);
    }
    public void Options()
    {
        optionsM.SetActive(true);

    }
    public void BackClick()
    {
        menuAnimator.Play("Return");
        optionsM.SetActive(false);
    }
    public void Back()
    {
        mainM.SetActive(true);
    }
    public void ChangeSceneToData()
    {
        SceneManager.LoadScene("CharacterData");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void ChangeScreenSize(Dropdown dropdown)
    {
        if(dropdown.value == 0)
        {
            Screen.SetResolution(1980, 1080, isFullScreen);
        }
        else if(dropdown.value == 1)
        {
            Screen.SetResolution(1600, 900, isFullScreen);
        }
        else if(dropdown.value == 2)
        {
            Screen.SetResolution(1280, 720, isFullScreen);
        }
        
    }

    public void FullScreenOrWindows(Toggle toggle)
    {
        if (toggle.isOn)
        {
            Screen.fullScreen = true;
            isFullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
            isFullScreen = false;
        }

    }
}
