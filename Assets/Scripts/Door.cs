using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable // Object is marked as interactable, and must use the interact function.
{
    bool canInteract = true;

    // Called by PlayerInteract.cs when the player is attempting to interact with the object.
    public void Interact(PlayerInteract interactor)
    {
        Debug.Log("Open Door!");
    }

    public bool Enabled
    {
        get { return canInteract; }
    }
}
