using UnityEngine;
using System.Collections;


public class ConnectorPoint : MonoBehaviour, IConnector
{
    [SerializeField] private Dir _dir;
    [SerializeField] private int _slot;
    [SerializeField] private GameObject _startObject;
    [SerializeField] private Vector3 _objectRotation;
    [SerializeField] private bool _drawGizmos = true;
    private ShipController _ship;
    private SphereCollider _connectionCollider;

    private void Start()
    {
        _ship = GetComponentInParent<ShipController>();
        _connectionCollider = GetComponent<SphereCollider>();
        var spawnedObject = Instantiate(_startObject, transform.position, Quaternion.Euler(_objectRotation));
        spawnedObject.GetComponent<IConnectable>().Connect(_dir, _slot, this);
        OnConnect();
    }
    public void HandleDisconnect(Dir dir, int slot)
    {
        OnDisconnect();
    }

    private void OnConnect()
    {
        _connectionCollider.enabled = false;
    }
    private void OnDisconnect()
    {
        StartCoroutine(DelayedReady());
    }
    private IEnumerator DelayedReady()
    {
        yield return new WaitForSeconds(1f);
        _connectionCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Connectable"))
        {
            IConnectable connectable = other.GetComponent<IConnectable>();
            if (connectable != null && !connectable.IsConnected)
            {
                connectable.Connect(_dir, _slot, this);
                OnConnect();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(_drawGizmos)
        {
            if(_startObject != null)
            {
                // draw red box as start object
                Gizmos.color = Color.red;
                Gizmos.DrawCube(transform.position, Vector3.one);
            }
        }
    }
}