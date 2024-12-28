using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interactable objects will use this interface, and the player character can check if an object with this interface is in the player character radius.
public interface IInteractable
{
    public void Interact(PlayerInteract interactor) { }
}

