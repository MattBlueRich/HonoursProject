using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ProBuilder.Shapes;

public class Door : MonoBehaviour
{
    bool canInteract = true;

    [Header("Door Animator")]
    public Animator doorAnimator;
    [Header("Settings")]
    public bool openOnStart;
    public bool Enabled
    {
        get { return canInteract; }
    }
    private void Start()
    {
        if (openOnStart)
        {
            doorAnimator.SetBool("IsOpen", true);
        }
        else
        {
            doorAnimator.SetBool("IsOpen", false);
        }
    }
    public void ActivateDoor(bool enable)
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
    public void ToggleDoor()
    {
        doorAnimator.SetBool("IsOpen", !doorAnimator.GetBool("IsOpen"));
    }
    public void DisableDoor()
    {
        canInteract = false;
    }

    public void EnableDoor()
    {
        canInteract = true;
    }
}
