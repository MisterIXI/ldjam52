using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Awake()
    {
        if (RefManager.playerController != null)
        {
            Destroy(this);
            return;
        }
        RefManager.playerController = this;
    }
}
