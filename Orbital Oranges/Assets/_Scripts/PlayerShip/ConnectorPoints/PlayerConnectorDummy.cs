using UnityEngine;

public class PlayerConnectorDummy : MonoBehaviour, IConnector
{
    void IConnector.HandleDisconnect(Dir dir, int slot, IConnectable connectable)
    {
    }
}