using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerSettings _playerSettings;
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
}
