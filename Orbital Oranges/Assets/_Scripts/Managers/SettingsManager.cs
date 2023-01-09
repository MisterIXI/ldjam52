using UnityEngine;
using System.Collections;
using System;

public class SettingsManager : MonoBehaviour
{


    public float SFXVolume { get; private set;}
    public float MusicVolume { get; private set;}
    public float MouseSensitivity { get; private set;}
    public float GamepadSensitivity { get; private set;}

    public Action<float> OnSFXVolumeChanged = delegate { };
    public Action<float> OnMusicVolumeChanged = delegate { };
    public Action<float> OnMouseSensitivityChanged = delegate { };
    public Action<float> OnGamepadSensitivityChanged = delegate { };

    private void Awake() {
        if (RefManager.settingsManager != null)
        {
            Destroy(this);
            return;
        }
        RefManager.settingsManager = this;
        DontDestroyOnLoad(this);
    }
    
    private void Start() {
        MenuManager menuManager = RefManager.menuManager;
        menuManager.SliderSFX.onValueChanged.AddListener(HandleSFXSlider);
        menuManager.SliderMusic.onValueChanged.AddListener(HandleMusicSlider);
        menuManager.SliderMouseSensitivity.onValueChanged.AddListener(HandleMouseSensitivitySlider);
        menuManager.SliderGamepadSensitivity.onValueChanged.AddListener(HandleGamepadSensitivitySlider);
    }
    public void HandleSFXSlider(float value)
    {
        SFXVolume = value;
        OnSFXVolumeChanged(SFXVolume);
    }

    public void HandleMusicSlider(float value)
    {
        MusicVolume = value;
        OnMusicVolumeChanged(MusicVolume);
    }

    public void HandleMouseSensitivitySlider(float value)
    {
        MouseSensitivity = value;
        OnMouseSensitivityChanged(MouseSensitivity);
    }

    public void HandleGamepadSensitivitySlider(float value)
    {
        GamepadSensitivity = value;
        OnGamepadSensitivityChanged(GamepadSensitivity);
    }
}