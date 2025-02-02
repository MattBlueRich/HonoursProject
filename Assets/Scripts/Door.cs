using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable // Object is marked as interactable, and must use the interact function.
{
    bool canInteract = true;

    public Animator doorAnimator;

    // Called by PlayerInteract.cs when the player is attempting to interact with the object.
    public void Interact(PlayerInteract interactor)
    {
        Debug.Log("Open Door!");
    }
    public bool Enabled
    {
        get { return canInteract; }
    }

    public void EnableDoor(bool enable)
    {
        if (enable == true)
        {
            doorAnimator.SetBool("IsOpen", true);
        }
        else
        {
            doorAnimator.SetBool("IsOpen", false);
        }
    }
}
