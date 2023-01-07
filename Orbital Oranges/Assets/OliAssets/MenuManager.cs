using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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


    void Start()
    {
        ButtonStart.onClick.AddListener(StartGame);
        ButtonSettings.onClick.AddListener(OpenSettings);
        ButtonCredits.onClick.AddListener(OpenCredits);
        ButtonQuit.onClick.AddListener(QuitGame);
    }


    void StartGame()
    {
        Debug.Log("Start Button Pressed.");
    }


    void OpenSettings() 
    {
        Debug.Log("Settings Button Pressed.");
    }

    void OpenCredits() 
    {
        Debug.Log("Credits Button Pressed.");
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