using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Door door;
    public bool openOnEnter;
    public bool closeOnExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && openOnEnter && door.Enabled)
        {
            door.EnableDoor(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && closeOnExit)
        {
            door.EnableDoor(false);
        }
    }
}
