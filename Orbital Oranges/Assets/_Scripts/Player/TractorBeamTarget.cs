using UnityEngine;

public class TractorBeamTarget : MonoBehaviour, IInteractable
{

    private Rigidbody _rigidbody;
    [SerializeField] private bool isFruit;
    private PlayerInteraction _playerInteraction;
    public TractorbeamScript ConnectedBeam;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInteraction = RefManager.playerInteraction;
    }
    public void DisconnectBeam()
    {
        ConnectedBeam = null;
        CollectorBeam beam = gameObject.GetComponent<CollectorBeam>();
        if (beam != null)
        {
            beam.DisconnectTractorBeam();
        }
    }
    public void Interact()
    {
        Thruster thruster = gameObject.GetComponent<Thruster>();
        if(thruster != null && thruster.IsConnected)
        {
            thruster.Disconnect();
        }
        CollectorBeam collectorBeam = gameObject.GetComponent<CollectorBeam>();
        if (collectorBeam != null && collectorBeam.IsConnected)
        {
            collectorBeam.Disconnect();
        }

        if (_playerInteraction.currentTractorBeam == null)
        {
            if (ConnectedBeam == null)
            {

                _playerInteraction.currentTractorBeam = Instantiate(_playerInteraction.TractorBeamPrefab).GetComponent<TractorbeamScript>();
                _playerInteraction.currentTractorBeam.itemRigidbody = _rigidbody;
                _playerInteraction.currentTractorBeam.targetTransform = _playerInteraction.transform;
                _playerInteraction.currentTractorBeam.playerRigidbody = _playerInteraction.gameObject.GetComponent<Rigidbody>();
                ConnectedBeam = _playerInteraction.currentTractorBeam;
            }
            else
            {
                _playerInteraction.currentTractorBeam = ConnectedBeam;
                _playerInteraction.currentTractorBeam.itemRigidbody = _rigidbody;
                _playerInteraction.currentTractorBeam.targetTransform = _playerInteraction.transform;
                _playerInteraction.currentTractorBeam.playerRigidbody = _playerInteraction.gameObject.GetComponent<Rigidbody>();
            }
        }
        else
        {
            CollectorBeam beam = gameObject.GetComponent<CollectorBeam>();
            if (beam != null)
            {
                beam.ConnectTractorBeam(_playerInteraction.currentTractorBeam);
            }
        }
    }

    public string GetInteractText()
    {
        return "Toggle tractorbeam";
    }

}