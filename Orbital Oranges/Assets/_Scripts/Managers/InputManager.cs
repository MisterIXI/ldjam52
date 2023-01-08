using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;



public class InputManager : MonoBehaviour
{
    public Action<bool> OnSchemeChanged = delegate { };
    public Action<CallbackContext> OnMove = delegate { };

    public Action<CallbackContext> OnVerticalMove = delegate { };
    public Action<CallbackContext> OnLook = delegate { };
    public Action<CallbackContext> OnInteract = delegate { };
    public Action<CallbackContext> OnBreak = delegate { };
    
    public Action<CallbackContext> OnPause = delegate { };
    private PlayerInput playerInput;
    private InputActionMap actionMap;
    public bool IsGamepadScheme { get; private set; }
    private void Awake()
    {
        if (RefManager.inputManager != null)
        {
            Destroy(this);
            Destroy(gameObject);
            return;
        }
        RefManager.inputManager = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        if(playerInput.currentControlScheme == "Gamepad")
        {
            IsGamepadScheme = true;
        }
        actionMap = playerInput.actions.FindActionMap("Player");
        BindInputs();
        playerInput.onControlsChanged += test;
    }
    private void test(PlayerInput input){
        if(input.currentControlScheme == "Gamepad")
        {
            IsGamepadScheme = true;
        }
        else
        {
            IsGamepadScheme = false;
        }
        OnSchemeChanged(IsGamepadScheme);
    }
    private void BindInputs()
    {
        actionMap.FindAction("Move").started += OnMoveInput;
        actionMap.FindAction("Move").performed += OnMoveInput;
        actionMap.FindAction("Move").canceled += OnMoveInput;
        actionMap.FindAction("VerticalMove").started += OnVerticalMoveInput;
        actionMap.FindAction("VerticalMove").performed += OnVerticalMoveInput;
        actionMap.FindAction("VerticalMove").canceled += OnVerticalMoveInput;
        actionMap.FindAction("Look").started += OnLookInput;
        actionMap.FindAction("Look").performed += OnLookInput;
        actionMap.FindAction("Look").canceled += OnLookInput;
        actionMap.FindAction("Interact").started += OnInteractInput;
        actionMap.FindAction("Interact").performed += OnInteractInput;
        actionMap.FindAction("Interact").canceled += OnInteractInput;
        actionMap.FindAction("Break").started += OnBreakInput;
        actionMap.FindAction("Break").performed += OnBreakInput;
        actionMap.FindAction("Break").canceled += OnBreakInput;
        actionMap.FindAction("Pause").started += OnPauseInput;
        actionMap.FindAction("Pause").performed += OnPauseInput;
        actionMap.FindAction("Pause").canceled += OnPauseInput;
    }
    public void OnMoveInput(CallbackContext context)
    {
        OnMove(context);
    }

    public void OnVerticalMoveInput(CallbackContext context)
    {
        OnVerticalMove(context);
    }


    public void OnLookInput(CallbackContext context)
    {
        OnLook(context);
    }

    public void OnInteractInput(CallbackContext context)
    {
        OnInteract(context);
    }

    public void OnBreakInput(CallbackContext context)
    {
        OnBreak(context);
    }

    public void OnPauseInput(CallbackContext context)
    {
        OnPause(context);
    }
}
