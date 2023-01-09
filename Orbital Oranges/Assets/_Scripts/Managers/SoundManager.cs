using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips = new AudioClip[10];
    private AudioSource audioSource;
    [SerializeField] private AudioClip mainBG;
    [SerializeField] private AudioClip finalBG;
    [SerializeField] private AudioClip menuBG;
    [SerializeField] private AudioClip hoverSFX;
    [SerializeField] private AudioClip clickSFX;
    private bool _finalStageTriggered = false;
    private float _startTime;
    private bool _gameRunning;

    public float volumeScale = 1;
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
        audioSource = Camera.main.GetComponent<AudioSource>();
        audioSource.loop = false;
        RefManager.gameManager.OnGameStateChange += OnGameStateChange;
        Debug.Log("SoundManager: Start");
    }
    public void PauseMusic()
    {
        audioSource.Pause();
    }

    public void UnPauseMusic()
    {
        audioSource.Play();
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
                audioSource.clip = mainBG;
                audioSource.loop = false;
                audioSource.Play();
                break;
            case GameManager.state.Critical:
                audioSource.clip = finalBG;
                audioSource.loop = false;
                audioSource.Play();
                break;
            case GameManager.state.Menu:
                _gameRunning = false;
                audioSource.clip = menuBG;
                audioSource.Play();
                audioSource.loop = true;
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

    public void PlaySound(string name, float volumeScale)
    {
       // GameObject soundGameObject = new GameObject("Sound");
        audioSource.PlayOneShot(getSound(name), volumeScale);
    }
    public void playhoverSFX()
    {
        audioSource.PlayOneShot(hoverSFX, volumeScale);
    }
    public void playclickSFX()
    {
        audioSource.PlayOneShot(clickSFX, volumeScale);
    }
    private AudioClip getSound(string _name)
    {
        AudioClip _myAudioClip = clips[0];
        foreach (AudioClip i in clips)
        {
            if (_name == i.name)
            {
                _myAudioClip = i;
            }
        }
        return _myAudioClip;
    }

}
