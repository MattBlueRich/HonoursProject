using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour, IInteractable // Object is marked as interactable, and must use the interact function.
{
    bool canInteract = true;


    public Animator buttonAnimator;
    public bool activeOnStart = false;
    public UnityEvent UE_OnButtonPressed;
    private bool isActive = false;
    public bool Enabled
    {
        get { return canInteract; }
    }
    private void Start()
    {
        if (activeOnStart)
        {
            buttonAnimator.SetBool("On", true);
            isActive = true;
        }
        else
        {
            buttonAnimator.SetBool("On", false);
            isActive = false;
        }
    }

    // Called by PlayerInteract.cs when the player is attempting to interact with the object.
    public void Interact(PlayerInteract interactor)
    {
        UE_OnButtonPressed.Invoke();
        ToggleAnimation();
    }

    public void ToggleAnimation()
    {
        if (isActive)
        {
            buttonAnimator.SetBool("On", false);
            isActive = false;
        }
        else
        {
            buttonAnimator.SetBool("On", true);
            isActive = true;
        }
    }
}
