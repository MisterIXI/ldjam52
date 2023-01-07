using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Awake() {
        if (RefManager.inputManager != null) {
            Destroy(this);
            return;
        }
        RefManager.inputManager = this;
    }
}
