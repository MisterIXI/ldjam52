using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ShipController : MonoBehaviour, IConnector
{

    private bool _isControlled;
    private Vector3 _input;
    private bool _isGamePadScheme;
    private Vector2 _lookPoint;
    private Vector2 _lookInput;
    private Rigidbody _rigidbody;
    private Dictionary<Dir, Thruster[]> _thrusters = new();
    private Vector3 _thrusterForce;
    private bool _isBreaking;
    [SerializeField] private GameObject _thrusterPrefab;
    private PlayerSettings _playerSettings;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        InitThrusterArrays();
        SubcribeToInput();
        SetControlled(true);
        InitWithThrusters();
        _playerSettings = RefManager.gameManager._playerSettings;
    }

    public void InitThrusterArrays()
    {
        _thrusters[Dir.Default] = new Thruster[0];
        _thrusters[Dir.Up] = new Thruster[4];
        _thrusters[Dir.Down] = new Thruster[6];
        _thrusters[Dir.Left] = new Thruster[4];
        _thrusters[Dir.Right] = new Thruster[4];
        _thrusters[Dir.Forward] = new Thruster[3];
        _thrusters[Dir.Backward] = new Thruster[3];
    }
    public void HandleDisconnect(Dir dir, int id)
    {
        _thrusters[dir][id] = null;
    }
    public void InitWithThrusters()
    {
        _thrusters[Dir.Up][0] = Instantiate(_thrusterPrefab, transform).GetComponent<Thruster>();
        _thrusters[Dir.Up][0].Connect(Dir.Up, 0, this);
        _thrusters[Dir.Down][0] = Instantiate(_thrusterPrefab, transform).GetComponent<Thruster>();
        _thrusters[Dir.Down][0].Connect(Dir.Down, 0, this);
        _thrusters[Dir.Left][0] = Instantiate(_thrusterPrefab, transform).GetComponent<Thruster>();
        _thrusters[Dir.Left][0].Connect(Dir.Left, 0, this);
        _thrusters[Dir.Right][0] = Instantiate(_thrusterPrefab, transform).GetComponent<Thruster>();
        _thrusters[Dir.Right][0].Connect(Dir.Right, 0, this);
        _thrusters[Dir.Forward][0] = Instantiate(_thrusterPrefab, transform).GetComponent<Thruster>();
        _thrusters[Dir.Forward][0].Connect(Dir.Forward, 0, this);
        _thrusters[Dir.Backward][0] = Instantiate(_thrusterPrefab, transform).GetComponent<Thruster>();
        _thrusters[Dir.Backward][0].Connect(Dir.Backward, 0, this);
    }
    public void FixedUpdate()
    {
        _lookInput = Vector2.MoveTowards(_lookInput, _lookPoint, 0.1f);
        if (!_isGamePadScheme)
        {
            _lookPoint = Vector2.MoveTowards(_lookPoint, Vector2.zero, _playerSettings.mousePointDrag * Time.fixedDeltaTime);
        }
        // rotate ship towards _lookInput
        // Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, _lookInput.x, 0) * Quaternion.Euler(-_lookInput.y, 0, 0);
        // Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 100 * Time.fixedDeltaTime);
        float angle_x = transform.rotation.eulerAngles.x;
        float angle_y = transform.rotation.eulerAngles.y;
        // rotate angle_x by _lookInput.x
        float old_x = angle_x;
        angle_x = angle_x - _lookInput.y * _playerSettings.cameraSensitivity;
        if (angle_x > 85 && angle_x < 180)
        {
            angle_x = 85;
        }
        if (angle_x > 180 && angle_x < 275)
        {
            angle_x = 275;
        }
        // angle_x = Mathf.Clamp(angle_x + _lookInput.y * _playerSettings.cameraSensitivity, -89, 89);
        angle_y = Mathf.MoveTowardsAngle(angle_y, angle_y + _lookInput.x * _playerSettings.cameraSensitivity, _playerSettings.cameraSensitivity);
        Quaternion newRotation = Quaternion.Euler(angle_x, angle_y, 0);
        _rigidbody.MoveRotation(newRotation);
        Debug.Log("new euler x: " + transform.rotation.eulerAngles.x);

        _thrusterForce = HandleThrusters();
        _rigidbody.AddForce(_thrusterForce, ForceMode.Acceleration);
        if (_isBreaking)
        {
            _rigidbody.angularVelocity = Vector3.MoveTowards(_rigidbody.angularVelocity, Vector3.zero, _playerSettings.angularBreakingPower);
            if (_rigidbody.velocity.magnitude < _playerSettings.breakingStopPoint)
                _rigidbody.velocity = Vector3.zero;
        }
    }

    private Vector3 HandleThrusters()
    {
        float x_val = 0;
        float y_val = 0;
        float z_val = 0;
        Thruster[] currArr = _thrusters[Dir.Down];
        Vector3 tempInput = _input;
        if (_isBreaking)
        {
            tempInput = -(_rigidbody.velocity);
            tempInput = Vector3.ClampMagnitude(tempInput, 1);
            // tempInput = tempInput.normalized;
        }
        float currInput = Mathf.Clamp(tempInput.y, -1, 0);
        foreach (Thruster thruster in currArr)
        {
            if (thruster != null)
            {
                thruster.SetTargetStrength(-currInput);
                y_val -= thruster.CurrentStrength;
            }
        }
        currArr = _thrusters[Dir.Up];
        currInput = Mathf.Clamp(tempInput.y, 0, 1);
        foreach (Thruster thruster in currArr)
        {
            if (thruster != null)
            {
                thruster.SetTargetStrength(currInput);
                y_val += thruster.CurrentStrength;
            }
        }
        currArr = _thrusters[Dir.Left];
        currInput = Mathf.Clamp(tempInput.x, -1, 0);
        foreach (Thruster thruster in currArr)
        {
            if (thruster != null)
            {
                thruster.SetTargetStrength(-currInput);
                x_val -= thruster.CurrentStrength;
            }
        }
        currArr = _thrusters[Dir.Right];
        currInput = Mathf.Clamp(tempInput.x, 0, 1);
        foreach (Thruster thruster in currArr)
        {
            if (thruster != null)
            {
                thruster.SetTargetStrength(currInput);
                x_val += thruster.CurrentStrength;
            }
        }
        currArr = _thrusters[Dir.Backward];
        currInput = Mathf.Clamp(tempInput.z, -1, 0);
        foreach (Thruster thruster in currArr)
        {
            if (thruster != null)
            {
                thruster.SetTargetStrength(-currInput);
                z_val -= thruster.CurrentStrength;
            }
        }
        currArr = _thrusters[Dir.Forward];
        currInput = Mathf.Clamp(tempInput.z, 0, 1);
        foreach (Thruster thruster in currArr)
        {
            if (thruster != null)
            {
                thruster.SetTargetStrength(currInput);
                z_val += thruster.CurrentStrength;
            }
        }
        Vector3 result = new Vector3(x_val, y_val, z_val);
        if (!_isBreaking)
            result = transform.rotation * result;
        return result;
    }

    public void SetControlled(bool isControlled)
    {
        _isControlled = isControlled;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (_isControlled)
        {
            Vector2 input = context.ReadValue<Vector2>();
            _input = new Vector3(input.x, _input.y, input.y);
            // Debug.Log("New input: " + _input);
        }
    }

    public void OnVerticalMovement(InputAction.CallbackContext context)
    {
        if (_isControlled && !context.started)
        {
            float input = context.ReadValue<float>();
            _input = new Vector3(_input.x, input, _input.z);
            // Debug.Log("New input: " + _input);
        }
    }

    public void OnBreakInput(InputAction.CallbackContext context)
    {
        if (_isControlled)
        {
            if (context.started)
            {
                _isBreaking = true;
            }
            else if (context.canceled)
            {
                _isBreaking = false;
                if (_rigidbody.velocity.magnitude < _playerSettings.breakingStopPoint)
                    _rigidbody.velocity = Vector3.zero;
            }
        }
    }
    public void OnLookInput(InputAction.CallbackContext context)
    {
        if (_isControlled)
        {
            if (_isGamePadScheme)
            {
                Debug.Log("Delta: " + context.ReadValue<Vector2>());
                _lookPoint = context.ReadValue<Vector2>();

            }
            else
            {
                // Debug.Log("Delta: " + curr_input);
                Vector2 curr_input = Vector2.ClampMagnitude(context.ReadValue<Vector2>(), 0.05f) * _playerSettings.mouseMultiplier;
                // Debug.Log("Clamped: " + curr_input);
                var old = _lookPoint;
                _lookPoint = Vector2.ClampMagnitude(_lookPoint + curr_input, 1);
                // Debug.Log("Look point: " + old + " -> " + _lookPoint);
            }
        }
    }
    private void OnDestroy()
    {
        if (RefManager.shipController == this)
        {
            RefManager.shipController = null;
        }
        UnSubscibeToInput();
    }

    public void OnControlSchemeChanged(bool isGamepadNow)
    {
        _isGamePadScheme = isGamepadNow;
        _lookInput = Vector2.zero;
    }
    public void SubcribeToInput()
    {
        InputManager inputManager = RefManager.inputManager;
        if (inputManager != null)
        {
            inputManager.OnSchemeChanged += OnControlSchemeChanged;
            inputManager.OnMove += OnMoveInput;
            inputManager.OnVerticalMove += OnVerticalMovement;
            inputManager.OnBreak += OnBreakInput;
            inputManager.OnLook += OnLookInput;
        }
    }

    public void UnSubscibeToInput()
    {
        InputManager inputManager = RefManager.inputManager;
        if (inputManager != null)
        {
            inputManager.OnSchemeChanged -= OnControlSchemeChanged;
            inputManager.OnMove -= OnMoveInput;
            inputManager.OnVerticalMove -= OnVerticalMovement;
            inputManager.OnBreak -= OnBreakInput;
            inputManager.OnLook -= OnLookInput;
        }
    }

    [SerializeField] private bool showGizmos;


    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            // Draw 3D gizmo of _thrusterForce
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.right * _thrusterForce.x);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.up * _thrusterForce.y);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, transform.forward * _thrusterForce.z);
        }
    }
}
