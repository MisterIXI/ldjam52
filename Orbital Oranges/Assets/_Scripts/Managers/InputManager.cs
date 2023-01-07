using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;



public class InputManager : MonoBehaviour
{
    public Action<CallbackContext> OnMove = delegate { };
    public Action<CallbackContext> OnUp = delegate { };
    public Action<CallbackContext> OnDown = delegate { };
    public Action<CallbackContext> OnLook = delegate { };
    public Action<CallbackContext> OnInteract = delegate { };
    public Action<CallbackContext> OnPause = delegate { };
    private PlayerInput playerInput;
    private InputActionMap actionMap;

    private void Awake()
    {
        if (RefManager.inputManager != null)
        {
            Destroy(this);
            return;
        }
        RefManager.inputManager = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        actionMap = playerInput.actions.FindActionMap("Player");
        BindInputs();
    }

    private void BindInputs()
    {
        actionMap.FindAction("Move").started += OnMoveInput;
        actionMap.FindAction("Move").performed += OnMoveInput;
        actionMap.FindAction("Move").canceled += OnMoveInput;
        actionMap.FindAction("Up").started += OnUpInput;
        actionMap.FindAction("Up").performed += OnUpInput;
        actionMap.FindAction("Up").canceled += OnUpInput;
        actionMap.FindAction("Down").started += OnDownInput;
        actionMap.FindAction("Down").performed += OnDownInput;
        actionMap.FindAction("Down").canceled += OnDownInput;
        actionMap.FindAction("Look").started += OnLookInput;
        actionMap.FindAction("Look").performed += OnLookInput;
        actionMap.FindAction("Look").canceled += OnLookInput;
        actionMap.FindAction("Interact").started += OnInteractInput;
        actionMap.FindAction("Interact").performed += OnInteractInput;
        actionMap.FindAction("Interact").canceled += OnInteractInput;
        actionMap.FindAction("Pause").started += OnPauseInput;
        actionMap.FindAction("Pause").performed += OnPauseInput;
        actionMap.FindAction("Pause").canceled += OnPauseInput;
    }
    public void OnMoveInput(CallbackContext context)
    {
        OnMove(context);
    }

    public void OnUpInput(CallbackContext context)
    {
        OnUp(context);
    }

    public void OnDownInput(CallbackContext context)
    {
        OnDown(context);
    }

    public void OnLookInput(CallbackContext context)
    {
        OnLook(context);
    }

    public void OnInteractInput(CallbackContext context)
    {
        OnInteract(context);
    }

    public void OnPauseInput(CallbackContext context)
    {
        OnPause(context);
    }


}
