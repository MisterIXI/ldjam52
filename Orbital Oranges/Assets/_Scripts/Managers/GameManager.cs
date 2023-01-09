using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class GameManager : MonoBehaviour
{
    public PlayerSettings _playerSettings;
    private Scene _oldScene;
    private Scene _currentScene;
    private AsyncOperation _operation;
    private const int CLOCK_START_TIME = 600;
    public float _clockTime { get; private set; }
    private void Awake()
    {
        if (RefManager.gameManager != null)
        {
            Destroy(this);
            return;
        }
        RefManager.gameManager = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _operation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        _operation.allowSceneActivation = false;

        SceneManager.sceneLoaded += OnSceneLoaded;
        _currentScene = SceneManager.GetActiveScene();
        _clockTime = -1f;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name + " " + mode + " -----------");
        _operation = null;
        // SceneManager.UnloadSceneAsync(_currentScene);
        // _currentScene = scene;
        if (scene.buildIndex == 0)
        {
            _operation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
            _operation.allowSceneActivation = false;
        }
        if (scene.buildIndex == 1)
        {
            GameStart();
        }
    }

    public void AllowTrigger()
    {
        _operation.allowSceneActivation = true;
    }
    public void LoadMenu(bool allowSceneActivation = false)
    {
        _operation = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        _operation.allowSceneActivation = allowSceneActivation;
    }

    private void Update()
    {
        if(Time.time - _clockTime >= CLOCK_START_TIME)
        {
            GameEnd();
        }
    }

    private void GameStart()
    {
        _clockTime = Time.time;
        RefManager.menuManager.StartTime = _clockTime;
        RefManager.menuManager.gameRunning = true;
    }

    public void GameEnd()
    {
        RefManager.menuManager.gameRunning = false;
    }
}
