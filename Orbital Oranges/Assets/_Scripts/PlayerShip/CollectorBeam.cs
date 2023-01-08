using UnityEngine;

public class CollectorBeam : MonoBehaviour, IConnectable
{
    [SerializeField] private Material _connectedMat;
    [SerializeField] private Material _disconnectedMat;
    private MeshRenderer _meshRenderer;
    private SphereCollider _connectionCollider;
    private IConnector _connector;
    private Dir _dir;
    private int _slot;
    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _connectionCollider = GetComponentInChildren<SphereCollider>();
    }

    public bool IsConnected { get; private set; }

    public void Connect(Dir dir, int slot, IConnector connector)
    {
        IsConnected = true;
        _connectionCollider.enabled = false;
        _meshRenderer.materials[1] = _connectedMat;
        _connector = connector;
        _dir = dir;
        _slot = slot;

    }

    public void Disconnect()
    {
        IsConnected = false;
        _connectionCollider.enabled = true;
        _meshRenderer.materials[1] = _disconnectedMat;
        _connector.HandleDisconnect(_dir, _slot, this);
    }
}