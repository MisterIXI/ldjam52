using UnityEngine;

public class TractorBeamTarget : MonoBehaviour, IInteractable
{

    private Rigidbody _rigidbody;
    [SerializeField] private float isFruit;
    private PlayerInteraction _playerInteraction;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInteraction = RefManager.playerInteraction;
    }

    public void Interact()
    {
        if(_playerInteraction.currentTractorBeam == null)
        {
            _playerInteraction.currentTractorBeam = Instantiate(_playerInteraction.TractorBeamPrefab).GetComponent<TractorbeamScript>();
            _playerInteraction.currentTractorBeam.itemRigidbody = _rigidbody;
        }
        else
        {
            
        }
    }

    public string GetInteractText()
    {
        return "Toggle tractorbeam";
    }

}