using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class GameManager : MonoBehaviour
{
    public PlayerSettings _playerSettings;
    private Scene _oldScene;
    private AsyncOperation _operation;
    public Action OnSceneLoaded = delegate { };
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

    private void Start() {
        SceneManager.activeSceneChanged += OnSceneChanged;
        _operation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        _operation.allowSceneActivation = false;
    }

    public void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        _operation = null;
        if(_oldScene.buildIndex == 0 && newScene.buildIndex == 1)
        {
            //switch from menu to game
            SceneManager.UnloadSceneAsync(_oldScene);
            _operation = null;
        }
        if(_oldScene.buildIndex == 1 && newScene.buildIndex == 0)
        {
            //switch from game to menu
            SceneManager.UnloadSceneAsync(_oldScene);
            _operation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        }
    }

    public void StartGame()
    {
        _operation.allowSceneActivation = true;
    }
    public void LoadMenu()
    {
        _operation = SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
        _operation.allowSceneActivation = false;
    }
    public void EndGame()
    {
        _operation.allowSceneActivation = true;
    }
}
