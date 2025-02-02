using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorTrigger : MonoBehaviour
{
    [Header("Door")]
    public Door door;
    [Header("Settings")]
    public bool openOnEnter;
    public bool closeOnExit;
    public bool disableOnEnter;
    [Header("Events")]
    public bool onlyInvokeOnce = false;
    public UnityEvent UE_Enter;
    private bool _onlyInvokeOnce = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && openOnEnter && door.Enabled)
        {
            InvokeEvent();
            door.ActivateDoor(true);
        }
        else if(other.CompareTag("Player") && disableOnEnter)
        {
            InvokeEvent();
            door.ActivateDoor(false);
            door.DisableDoor();
        }
        else if(other.CompareTag("Player"))
        {
            InvokeEvent();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && closeOnExit)
        {
            door.ActivateDoor(false);
        }
    }
    public void InvokeEvent()
    {
        if (onlyInvokeOnce && _onlyInvokeOnce)
        {
            _onlyInvokeOnce = false;
            UE_Enter.Invoke();
        }
        else if (!onlyInvokeOnce)
        {
            UE_Enter.Invoke();
        }
    }
}
