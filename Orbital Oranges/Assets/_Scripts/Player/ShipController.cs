using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ShipController : MonoBehaviour
{

    private bool _isControlled;
    private Vector3 _input;


    public void SetControlled(bool isControlled)
    {
        _isControlled = isControlled;

    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (_isControlled)
        {
            Vector2 input = context.ReadValue<Vector2>();
            _input = new Vector3(input.x, _input.z, input.y);
            Debug.Log(input);
        }

    }

    public void OnUpInput(InputAction.CallbackContext context)
    {


    }

    public void OnDownInput(InputAction.CallbackContext context)
    {


    }


    private void OnDestroy()
    {
        if (RefManager.shipController == this)
        {
            RefManager.shipController = null;
        }
        UnSubscibeToInput();
    }

    public void SubcribeToInput()
    {
        InputManager inputManager = RefManager.inputManager;
        if (inputManager != null)
        {
            inputManager.OnMove += OnMoveInput;
            inputManager.OnUp += OnUpInput;
            inputManager.OnDown += OnDownInput;
        }
    }

    public void UnSubscibeToInput()
    {
        InputManager inputManager = RefManager.inputManager;
        if (inputManager != null)
        {
            inputManager.OnMove -= OnMoveInput;
            inputManager.OnUp -= OnUpInput;
            inputManager.OnDown -= OnDownInput;

        }
    }
}
