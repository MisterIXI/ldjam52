using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// using TMPro;


public class MenuManager : MonoBehaviour
{
    [Header("MainMenu Container")]
    [SerializeField] public Button ButtonStart;
    [SerializeField] public Button ButtonSettings;
    [SerializeField] public Button ButtonCredits;
    [SerializeField] public Button ButtonQuit;
    [SerializeField] public GameObject ContainerMain;

    // [SerializeField] public TextMeshProUGUI StartText;


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


    // private vars

    private Material StartTextMaterial;


    void Start()
    {
        // MainMenu Buttons
        ButtonStart.onClick.AddListener(StartGame);
        ButtonSettings.onClick.AddListener(OpenSettings);
        ButtonCredits.onClick.AddListener(OpenCredits1);
        ButtonQuit.onClick.AddListener(QuitGame);

        // Closing seperate windows
        ButtonSettingsX.onClick.AddListener(OpenMainMenu);
        ButtonCredits1X.onClick.AddListener(OpenMainMenu);
        ButtonCredits2X.onClick.AddListener(OpenMainMenu);

        // Credits Pageing
        ButtonNextPage.onClick.AddListener(OpenCredits2);
        ButtonPrevPage.onClick.AddListener(OpenCredits1);

        // StartText.color = new Color32(251, 1, 223, 255);
    }


    void StartGame()
    {
        Debug.Log("Start Button Pressed --> Starting Game.");
        // YANNIK Start game here
    }


    void OpenSettings() 
    {
        Debug.Log("Settings Button Pressed --> Opening Settings Screen.");
        ContainerMain.SetActive(false);
        ContainerCredits1.SetActive(false);
        ContainerCredits2.SetActive(false);

        ContainerSettings.SetActive(true);
    }

    void OpenCredits1() 
    {
        Debug.Log("Credits1 Button Pressed --> Opening Credits Page1.");

        ContainerMain.SetActive(false);
        ContainerSettings.SetActive(false);
        ContainerCredits2.SetActive(false);

        ContainerCredits1.SetActive(true);
    }


    void OpenCredits2() 
    {
        Debug.Log("Credits2 Button Pressed --> Opening Credits Page2.");

        ContainerMain.SetActive(false);
        ContainerSettings.SetActive(false);
        ContainerCredits1.SetActive(false);

        ContainerCredits2.SetActive(true);
    }


    void OpenMainMenu() 
    {
        Debug.Log("Close Menu Button Pressed --> Opening Main Menu.");

        ContainerSettings.SetActive(false);
        ContainerCredits1.SetActive(false);
        ContainerCredits2.SetActive(false);

        ContainerMain.SetActive(true);
    }


    void QuitGame()
    {
        Debug.Log("Quit Button Pressed --> Exiting Game.");
        Application.Quit();
    }
}


        // if(ControlsPanel.activeSelf == false)
        // {
        //     Debug.Log("Controls Button Pressed --> Showing Controls.");
        //     CreditsPanel.SetActive(false);
        //     ResourcesPanel.SetActive(false);
        //     ControlsPanel.SetActive(true);
        // }
        // else
        // {
        //     Debug.Log("Controls Button Pressed --> Hiding Controls.");
        //     ControlsPanel.SetActive(false);
        // }