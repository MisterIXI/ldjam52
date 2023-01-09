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
    public const int GAME_DURATION = 600;
    public const int GAME_CRITICAL_STAGE = 459;
    public Action<state> OnGameStateChange = delegate { };
    public float _clockTime { get; private set; }
    private state _currentState = state.Menu;
    public enum state
    {
        None,
        running,
        Critical,
        Menu
    }
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
        // _operation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        // _operation.allowSceneActivation = false;

        OnGameStateChange += OnStateChange;
        SceneManager.sceneLoaded += OnSceneLoaded;
        _currentScene = SceneManager.GetActiveScene();
        _clockTime = -1f;
        StartCoroutine(DelayedStart());
    }
    private IEnumerator DelayedStart()
    {
        // trigger first scene loaded one frame after actually loading since OnSceneLoaded doesn't trigger the first time otherwise
        yield return null;
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
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
            OnGameStateChange(state.Menu);
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
        if (_currentState != state.Critical &&Time.time - _clockTime >= GAME_CRITICAL_STAGE)
        {
            OnGameStateChange(state.Critical);
        }
        if (_currentState == state.Critical &&Time.time - _clockTime >= GAME_DURATION)
        {
            GameEnd();
        }
    }

    private void GameStart()
    {
        _clockTime = Time.time;
        OnGameStateChange(state.running);
    }

    public void GameEnd()
    {
        RefManager.menuManager.gameRunning = false;
        OnGameStateChange(state.None);
    }

    private void OnStateChange(state newState)
    {
        _currentState = newState;
    }
}
