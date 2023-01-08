using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Thruster : MonoBehaviour, IConnectable
{
    public bool IsConnected { get; private set; }
    [SerializeField] private ThrusterSettings _settings;

    public float CurrentStrength { get; private set; }

    private float _targetStrength;
    private Dir _dir;
    private int _slot;
    private IConnector _connector;

    private void FixedUpdate()
    {
        if (IsConnected)
        {
            // check if acceleration or deceleration is needed
            float maxDelta = _targetStrength > CurrentStrength ? _settings.Acceleration : _settings.Deceleration;
            // move CurrentStrength towards _targetStrength
            CurrentStrength = Mathf.MoveTowards(CurrentStrength, _targetStrength, maxDelta * Time.fixedDeltaTime);
        }
        else if (CurrentStrength > 0)
        {
            CurrentStrength = Mathf.MoveTowards(CurrentStrength, 0, _settings.Deceleration * Time.fixedDeltaTime);
        }
    }

    public void SetTargetStrength(float targetStrength)
    {
        _targetStrength = targetStrength * _settings.Strength;
    }

    public void Connect(Dir dir, int slot, IConnector connector)
    {
        IsConnected = true;
    }

    public void Disconnect()
    {
        IsConnected = false;
        _connector.HandleDisconnect(_dir, _slot);
        _connector = null;
        _dir = Dir.Default;
        _slot = -1;
    }
}
