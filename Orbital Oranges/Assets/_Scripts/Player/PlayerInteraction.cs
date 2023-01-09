using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float interactionDistance;
    private void Start()
    {
        InputManager inputManager = RefManager.inputManager;

        inputManager.OnInteract += FireRay;
    }


    void FireRay(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            int layers = 1 << 3;
            layers = ~layers;
            Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.red);
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, layers))
            {

                if(hit.collider.GetComponent<IInteractable>() != null)
                {
                    IInteractable interactableObject = hit.collider.GetComponent<IInteractable>();
                    interactableObject.Interact();
                }
                else
                {
                    Rigidbody[] bodies = hit.collider.transform.root.GetComponentsInChildren<Rigidbody>();
                    foreach(Rigidbody rb in bodies)
                    {
                        rb.drag = 2;
                        rb.angularDrag = 2;
                    }
                }
            }   
        }
    }

    private void OnDestroy() {
        RefManager.inputManager.OnInteract -= FireRay;
    }
}
