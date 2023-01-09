using UnityEngine;

public class CollectorBeam : MonoBehaviour, IConnectable
{
    [SerializeField] private Material _connectedMat;
    [SerializeField] private Material _disconnectedMat;
    [SerializeField] private Transform _tipTransform;
    private MeshRenderer _meshRenderer;
    private SphereCollider _connectionCollider;
    private IConnector _connector;
    private Dir _dir;
    private int _slot;
    private Rigidbody _shipRB;
    public TractorbeamScript CurrentTractorbeam { get; private set; }
    private PlayerInteraction _playerInteraction;
    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _connectionCollider = GetComponentInChildren<SphereCollider>();
    }

    private void Start() {
        _shipRB = GetComponentInParent<Rigidbody>();
        _playerInteraction = RefManager.playerInteraction;
    }

    public bool IsConnected { get; private set; }
    
    public void ConnectTractorBeam(TractorbeamScript tractorbeam)
    {
        CurrentTractorbeam = tractorbeam;
        tractorbeam.playerRigidbody = _shipRB;
        tractorbeam.targetTransform = transform;
    }

    public void DisconnectTractorBeam()
    {
        CurrentTractorbeam = null;
    }

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
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }


}