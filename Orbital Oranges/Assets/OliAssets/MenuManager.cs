using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class MenuManager : MonoBehaviour
{
    [Header("MainMenu Container")]
    [SerializeField] public Button ButtonStart;
    [SerializeField] public Button ButtonSettings;
    [SerializeField] public Button ButtonCredits;
    [SerializeField] public Button ButtonQuit;
    [SerializeField] public GameObject ContainerMain;


    [Header("Settings Container")]
    [SerializeField] public Button ButtonSettingsX;
    [SerializeField] public Slider SliderMusic;
    [SerializeField] public Slider SliderSFX;
    [SerializeField] public GameObject ContainerSettings;


    [Header("Settings Keyboard Keybinds Buttons")]
    [SerializeField] public Button ButtonKeyboardForwards;
    [SerializeField] public Button ButtonKeyboardBackwards;
    [SerializeField] public Button ButtonKeyboardLeft;
    [SerializeField] public Button ButtonKeyboardRight;
    [SerializeField] public Button ButtonKeyboardInteract;


    [Header("Settings Gamepad Keybinds Buttons")]
    [SerializeField] public Button ButtonGamepadForwards;
    [SerializeField] public Button ButtonGamepadBackwards;
    [SerializeField] public Button ButtonGamepadLeft;
    [SerializeField] public Button ButtonGamepadRight;
    [SerializeField] public Button ButtonGamepadInteract;


    [Header("Credits Container 1&2")]
    [SerializeField] public Button ButtonCredits1X;
    [SerializeField] public Button ButtonCredits2X;
    [SerializeField] public Button ButtonNextPage;
    [SerializeField] public Button ButtonPrevPage;
    [SerializeField] public GameObject ContainerCredits1;
    [SerializeField] public GameObject ContainerCredits2;


    [Header("Pause/Sure Container")]
    [SerializeField] public Button ButtonResume;
    [SerializeField] public Button ButtonSettings2;
    [SerializeField] public Button ButtonMain;
    [SerializeField] public Button ButtonQuit2;
    [SerializeField] public Button ButtonYes;
    [SerializeField] public Button ButtonNo;
    [SerializeField] public GameObject ContainerPause;
    [SerializeField] public GameObject ContainerSure;


    [Header("End Container")]
    [SerializeField] public Button ButtonMain2;
    [SerializeField] public GameObject ContainerEnd;


    // private vars
    private Material StartTextMaterial;
    private bool isPaused = false;
    private bool isMain = false;


    void Start()
    {
        // MainMenu Buttons
        ButtonStart.onClick.AddListener(StartGame);
        ButtonSettings.onClick.AddListener(OpenSettings);
        ButtonCredits.onClick.AddListener(OpenCredits1);
        ButtonQuit.onClick.AddListener(QuitGame);

        // Closing seperate windows
        ButtonSettingsX.onClick.AddListener(CloseSettings);

        ButtonCredits1X.onClick.AddListener(OpenMainMenu);
        ButtonCredits2X.onClick.AddListener(OpenMainMenu);

        // Credits Pageing
        ButtonNextPage.onClick.AddListener(OpenCredits2);
        ButtonPrevPage.onClick.AddListener(OpenCredits1);

        // Pause Menu
        ButtonResume.onClick.AddListener(HideAllContainers);
        ButtonSettings2.onClick.AddListener(OpenSettings);
        ButtonMain.onClick.AddListener(OpenSure);
        ButtonQuit2.onClick.AddListener(OpenSure);

        // Sure Menu
        ButtonYes.onClick.AddListener(ConfirmSureMenu);
        ButtonNo.onClick.AddListener(OpenPauseMenu);

        // End Menu
        ButtonMain2.onClick.AddListener(OpenMainMenu);
    }


    void StartGame()
    {
        Debug.Log("Start Button Pressed --> Starting Game.");
        // YANNIK Start game here
    }   
        


    void OpenSettings() 
    {
        Debug.Log("Settings Button Pressed --> Opening Settings Screen.");

        HideAllContainers();
        ContainerSettings.SetActive(true);

        ButtonGamepadForwards.Select();
    }

    void OpenCredits1() 
    {
        Debug.Log("Credits1 Button Pressed --> Opening Credits Page1.");

        HideAllContainers();
        ContainerCredits1.SetActive(true);

        ButtonNextPage.Select();
    }


    void OpenCredits2() 
    {
        Debug.Log("Credits2 Button Pressed --> Opening Credits Page2.");

        HideAllContainers();
        ContainerCredits2.SetActive(true);

        ButtonPrevPage.Select();
    }


    void OpenMainMenu() 
    {
        Debug.Log("Close Menu Button Pressed --> Opening Main Menu.");

        HideAllContainers();
        ContainerMain.SetActive(true);

        isPaused = false;

        ButtonStart.Select();
    }

    void OpenPauseMenu() // YANNIK du kannst die funktion OpenPauseMenu(); ausführen um das pausenmenu zu öffnen... duh! (Set it on ESC)
    {
        Debug.Log("Pausing the Game.");

        HideAllContainers();
        ContainerPause.SetActive(true);

        isPaused = true;

        ButtonResume.Select();
    }


    void OpenEndMenu() // YANNIK Wenn du OpenEndMenu(); Machst kannst du End Screen ausführen.
    {
        Debug.Log("GameOver Screen.");

        HideAllContainers();
        ContainerEnd.SetActive(true);

        ButtonMain2.Select();
    }
    


    void OpenSure() 
    {
        Debug.Log("Quit/MainMenu Button Pressed --> Asking if this guy is a chicken or not.");

        HideAllContainers();
        ContainerSure.SetActive(true);

        ButtonNo.Select();
    }


    void CloseSettings()
    {
        if (isPaused)
        {
            OpenPauseMenu();
        }
        else
        {
            OpenMainMenu();
        }
    }


    void ConfirmSureMenu()
    {
        if (isMain)
        {
            OpenMainMenu();
        }
        else
        {
            QuitGame();
        }
    }


    void QuitGame()
    {
        Debug.Log("Quit Button Pressed --> Exiting Game.");
        Application.Quit();
    }

    void HideAllContainers()
    {
        ContainerMain.SetActive(false);
        ContainerSettings.SetActive(false);
        ContainerCredits1.SetActive(false);
        ContainerCredits2.SetActive(false);
        ContainerPause.SetActive(false);
        ContainerSure.SetActive(false);
        ContainerEnd.SetActive(false);
    }
}
