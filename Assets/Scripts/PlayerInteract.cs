using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint; // A child transform from where the collision for interactable objects is checked.
    [SerializeField] private float interactionPointRadius = 0.5f; // Radius of the interaction collision.
    [SerializeField] private LayerMask interactableMask; // Only check for overlapping interactable objects with this layer mask.

    private readonly Collider[] colliders = new Collider[3]; // How many interactable objects to search for.
    [ReadOnlyInspector][SerializeField] private int numFound; // How many interactable objects found.

    bool interactableCheck = false;

    // Update is called once per frame
    void Update()
    {
        // Creates a sphere collider around the interactionPoint, with a size of interactionRadius, and returns a number (max 3) of overlapping objects with the interactableMask.
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactableMask);

        if(numFound > 0) // If an object with the interactable layer mask enters the OverlapSphere...
        {
            var interactable = colliders[0].GetComponent<IInteractable>(); // Attempts to get the overlapping object's IInteractable Interface.

            if (interactable != null & Keyboard.current.eKey.wasPressedThisFrame) // If the overlapping object DOES in fact have an IInteractable Interface...
            {
                interactable.Interact(this); // Perform the interact function inside the overlapping object, passing a reference to this PlayerInteract.cs script.
            }

            // CURSOR ---

            // If the player is in-range of the interactable, and it can still be interacted with...
            if (interactableCheck && interactable.Enabled)
            {
                interactableCheck = false;
                CursorManager.instance.SetCursorInteract(); // Switch cursor icon to interact cursor icon.
            }
        }
        // Else if there is no interactable in-range...
        else if(numFound <=0 && !interactableCheck)
        {
            interactableCheck = true;

            // CURSOR ---

            CursorManager.instance.SetCursorNormal(); // Switch cursor icon to normal cursor icon.
        }
    }

    // This function creates a visual indicator of the "Physics.OverlapSphereNonAlloc", which can be seen in the editor.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }
}
