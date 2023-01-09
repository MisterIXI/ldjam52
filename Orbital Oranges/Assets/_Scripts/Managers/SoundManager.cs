using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips = new AudioClip[10];
    private AudioSource musicAudioSource;
    private AudioSource sfxAudioSource;
    [SerializeField] private AudioClip mainBG;
    [SerializeField] private AudioClip finalBG;
    [SerializeField] private AudioClip menuBG;
    [SerializeField] private AudioClip hoverSFX;
    [SerializeField] private AudioClip clickSFX;
    private bool _finalStageTriggered = false;
    private float _startTime;
    private bool _gameRunning;

    public float volumeScale = 1;
    private SettingsManager _settingsManager;
    private void Awake()
    {
        if (RefManager.soundManager != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        RefManager.soundManager = this;
    }
    void Start()
    {
        musicAudioSource = Camera.main.GetComponent<AudioSource>();
        musicAudioSource.loop = false;
        sfxAudioSource = Camera.main.gameObject.AddComponent<AudioSource>();
        musicAudioSource.volume = 0.5f;
        sfxAudioSource.volume = 0.5f;
        RefManager.gameManager.OnGameStateChange += OnGameStateChange;

        Debug.Log("SoundManager: Start");
        _settingsManager = RefManager.settingsManager;
        _settingsManager.OnMusicVolumeChanged += OnMusicVolumeChanged;
        _settingsManager.OnSFXVolumeChanged += OnSFXVolumeChanged;
    }
    public void OnMusicVolumeChanged(float newVolume)
    {
        musicAudioSource.volume = newVolume / 10f;
    }

    public void OnSFXVolumeChanged(float newVolume)
    {
        sfxAudioSource.volume = newVolume / 10f;
    }
    public void PauseMusic()
    {
        musicAudioSource.Pause();
    }

    public void UnPauseMusic()
    {
        musicAudioSource.Play();
    }
    private void OnDestroy()
    {
        if (RefManager.gameManager != null)
            RefManager.gameManager.OnGameStateChange -= OnGameStateChange;
    }
    public void OnGameStateChange(GameManager.state newState)
    {
        Debug.Log("SoundManager: OnGameStateChange: " + newState);
        switch (newState)
        {
            case GameManager.state.running:
                _startTime = Time.time;
                _gameRunning = true;
                _finalStageTriggered = false;
                musicAudioSource.clip = mainBG;
                musicAudioSource.loop = false;
                musicAudioSource.Play();
                break;
            case GameManager.state.Critical:
                musicAudioSource.clip = finalBG;
                musicAudioSource.loop = false;
                musicAudioSource.Play();
                break;
            case GameManager.state.Menu:
                _gameRunning = false;
                musicAudioSource.clip = menuBG;
                musicAudioSource.Play();
                musicAudioSource.loop = true;
                break;
            default:
                _gameRunning = false;
                break;
        }
    }
    void FixedUpdate()
    {
        if (_gameRunning)
        {
            if (!_finalStageTriggered && Time.time - _startTime > GameManager.GAME_CRITICAL_STAGE)
            {
                _finalStageTriggered = true;

            }
        }
    }

    public void playhoverSFX()
    {
        if (sfxAudioSource != null)
            sfxAudioSource.PlayOneShot(hoverSFX);
    }
    public void playclickSFX()
    {
        if (sfxAudioSource != null)
            sfxAudioSource.PlayOneShot(clickSFX);
    }


}
