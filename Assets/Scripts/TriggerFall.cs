using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFall : MonoBehaviour
{
    public Rigidbody[] platforms;
    private bool onTrigger = false;

    private void Start()
    {
        UsePhysics(false);
    }
    public void UsePhysics(bool enabled)
    {
        if (enabled)
        {
            foreach (Rigidbody rb in platforms)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
        }
        else
        {
            foreach (Rigidbody rb in platforms)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && onTrigger)
        {
            UsePhysics(true);
        }
    }
    public void EnableTrigger()
    {
        onTrigger = true;
    }
    public void DisableTrigger()
    {
        onTrigger = false;
    }
}
