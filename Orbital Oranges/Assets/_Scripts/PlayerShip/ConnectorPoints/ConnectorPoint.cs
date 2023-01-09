using UnityEngine;
using System.Collections;


public class ConnectorPoint : MonoBehaviour, IConnector, IInteractable
{
    [SerializeField] private Dir _dir;
    [SerializeField] private int _slot;
    [SerializeField] private GameObject _startObject;
    [SerializeField] private Vector3 _objectRotation;
    [SerializeField] private bool _drawGizmos = true;
    [SerializeField] private Material _connectedMat;
    [SerializeField] private Material _disconnectedMat;
    private ShipController _ship;
    private SphereCollider _connectionCollider;
    private MeshRenderer _meshRenderer;
    private PlayerInteraction _playerInteraction;

    private void Start()
    {
        _ship = GetComponentInParent<ShipController>();
        _connectionCollider = GetComponent<SphereCollider>();
        _meshRenderer = GetComponent<MeshRenderer>();
        if (_startObject != null)
        {
            var spawnedObject = Instantiate(_startObject, transform.position, Quaternion.identity);
            // align spawnedObject right vector with objectRotation
            spawnedObject.transform.localRotation = Quaternion.Euler(_objectRotation);
            spawnedObject.GetComponent<IConnectable>().Connect(_dir, _slot, this);
            OnConnect(spawnedObject.GetComponent<IConnectable>());
        }
        _playerInteraction = RefManager.playerInteraction;
    }
    public void HandleDisconnect(Dir dir, int slot, IConnectable connectable)
    {
        OnDisconnect(connectable);
    }

    private void OnConnect(IConnectable connectable)
    {
        _connectionCollider.enabled = false;
        MonoBehaviour connectMono = (MonoBehaviour)connectable;
        var rb = connectMono.GetComponent<Rigidbody>();
        Destroy(rb);
        ((MonoBehaviour)(connectable)).transform.parent = transform;
        if (connectable.GetType() == typeof(Thruster))
        {
            _ship.AddThruster((Thruster)connectable, _dir, _slot);
        }
        _meshRenderer.material = _connectedMat;
    }
    private void OnDisconnect(IConnectable connectable)
    {
        StartCoroutine(DelayedReady());
        _ship.HandleDisconnect(_dir, _slot, connectable);
        if (connectable.GetType() == typeof(MonoBehaviour))
        {
            ((MonoBehaviour)(connectable)).transform.parent = null;
        }
    }
    private IEnumerator DelayedReady()
    {
        yield return new WaitForSeconds(1f);
        _connectionCollider.enabled = true;
        _meshRenderer.material = _disconnectedMat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Connectable"))
        {
            IConnectable connectable = other.GetComponent<IConnectable>();
            if (connectable != null && !connectable.IsConnected)
            {
                connectable.Connect(_dir, _slot, this);
                OnConnect(connectable);
            }
        }
    }
    public void Interact()
    {
        if(_playerInteraction.currentTractorBeam != null)
        {
            IConnectable connectable = _playerInteraction.currentTractorBeam.itemRigidbody.GetComponent<IConnectable>();
            if(connectable != null && !connectable.IsConnected)
            {
                connectable.Connect(_dir, _slot, this);
                OnConnect(connectable);
            }
        }
    }
    public string GetInteractText()
    {
        if (_playerInteraction.currentTractorBeam != null)
            if (_playerInteraction.currentTractorBeam.itemRigidbody.GetComponent<CollectorBeam>() != null)
                return "Connect to CollectorBeam";
            else
                return "Needs a CollectorBeam to connect to...";
        else
            return "Empty Connector";
    }
    private void OnDrawGizmos()
    {
        if (_drawGizmos)
        {
            if (_startObject != null)
            {
                // draw red box as start object
                Gizmos.color = Color.red;
                Gizmos.DrawCube(transform.position, Vector3.one);
            }
        }
    }
}