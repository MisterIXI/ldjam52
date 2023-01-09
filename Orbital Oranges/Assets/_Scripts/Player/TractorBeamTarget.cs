using UnityEngine;

public class TractorBeamTarget : MonoBehaviour, IInteractable
{

    private Rigidbody _rigidbody;
    [SerializeField] private bool isFruit;
    private PlayerInteraction _playerInteraction;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInteraction = RefManager.playerInteraction;
    }

    public void Interact()
    {

        if (_playerInteraction.currentTractorBeam == null)
        {
            _playerInteraction.currentTractorBeam = Instantiate(_playerInteraction.TractorBeamPrefab).GetComponent<TractorbeamScript>();
            _playerInteraction.currentTractorBeam.itemRigidbody = _rigidbody;
            _playerInteraction.currentTractorBeam.targetTransform = _playerInteraction.transform;
            _playerInteraction.currentTractorBeam.playerRigidbody = _playerInteraction.gameObject.GetComponent<Rigidbody>();
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