using UnityEngine;

public class ConnectorPoint : MonoBehaviour, IConnector
{
    [SerializeField] private Dir _dir;
    [SerializeField] private int _slot;
    [SerializeField] private GameObject _startObject;
    private ShipController _ship;

    private void Start() {
        _ship = GetComponentInParent<ShipController>();
    }
    public void HandleDisconnect(Dir dir, int slot)
    {

    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Connectable"))
        {
            IConnectable connectable = other.GetComponent<IConnectable>();
            if (connectable != null)
            {
                connectable.Connect(_dir, _slot, this);
            }
        }
    }
}