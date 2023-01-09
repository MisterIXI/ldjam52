using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float interactionDistance;
    private TextMeshProUGUI interactText;
    public TractorbeamScript currentTractorBeam;
    public GameObject TractorBeamPrefab;
    private void Awake() {
        if(RefManager.playerInteraction != null) {
            Destroy(gameObject);
            return;
        }
        RefManager.playerInteraction = this;
    }
    private void Start()
    {
        InputManager inputManager = RefManager.inputManager;
        interactText = RefManager.menuManager.TextInteract;
        inputManager.OnInteract += FireRay;
    }

    private void Update()
    {
        RaycastHit hit;
        // int layers = 1 << 3;
        // layers = ~layers;
        Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            if (hit.collider.GetComponent<IInteractable>() != null)
            {
                IInteractable interactableObject = hit.collider.GetComponent<IInteractable>();
                interactText.text = interactableObject.GetInteractText();
            }
            // Debug.Log("Ray hit: " + hit.collider.name);
        }
        else
        {
            interactText.text = "";
        }
    }
    void FireRay(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // int layers = 1 << 3;
            // layers = ~layers;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
            {

                if (hit.collider.GetComponent<IInteractable>() != null)
                {
                    IInteractable interactableObject = hit.collider.GetComponent<IInteractable>();
                    interactableObject.Interact();
                }
                else
                {
                    Rigidbody[] bodies = hit.collider.transform.root.GetComponentsInChildren<Rigidbody>();
                    foreach (Rigidbody rb in bodies)
                    {
                        rb.drag = 2;
                        rb.angularDrag = 2;
                    }
                }
            }
            else
            {
                if(currentTractorBeam != null)
                {
                    currentTractorBeam.SelfDestruct();
                    currentTractorBeam = null;
                }
            }
        }
    }

    private void OnDestroy()
    {
        RefManager.inputManager.OnInteract -= FireRay;
    }
}
