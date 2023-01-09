using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Thruster : MonoBehaviour, IConnectable
{
    public bool IsConnected { get; private set; }
    [SerializeField] private ThrusterSettings _settings;
    [SerializeField] private ParticleSystem _particleSystem;

    public float CurrentStrength { get; private set; }

    private float _targetStrength;
    private Dir _dir;
    private int _slot;
    private IConnector _connector;
    private SphereCollider _connectionCollider;
    private void Awake()
    {
        _connectionCollider = GetComponentInChildren<SphereCollider>();

    }
    private void Start()
    {
    }

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
        if (_particleSystem != null)
        {
            // update particle system
            var main = _particleSystem.main;
            main.startSpeed = CurrentStrength * (_settings.ParticleSpeedRange.y - _settings.ParticleSpeedRange.x) + _settings.ParticleSpeedRange.x;
        }
    }

    public void SetTargetStrength(float targetStrength)
    {
        _targetStrength = targetStrength * _settings.Strength;
    }

    public void Connect(Dir dir, int slot, IConnector connector)
    {
        IsConnected = true;
        if (_connectionCollider != null)
            _connectionCollider.enabled = false;
        _connector = connector;
        _dir = dir;
        _slot = slot;
    }

    public void Disconnect()
    {
        IsConnected = false;
        _connector.HandleDisconnect(_dir, _slot, this);
        _connector = null;
        _dir = Dir.Default;
        _slot = -1;
        _connectionCollider.enabled = true;
    }
}
