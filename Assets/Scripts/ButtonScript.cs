using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour, IInteractable // Object is marked as interactable, and must use the interact function.
{
    bool canInteract = true;

    public UnityEvent UE_OnButtonPressed;

    public bool Enabled
    {
        get { return canInteract; }
    }

    // Called by PlayerInteract.cs when the player is attempting to interact with the object.
    public void Interact(PlayerInteract interactor)
    {
        UE_OnButtonPressed.Invoke();
    }
}
