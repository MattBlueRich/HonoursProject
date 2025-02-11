using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerFall : MonoBehaviour
{
    public Rigidbody[] platforms;
    private bool onTrigger = false;
    public UnityEvent UE_OnFall;

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
            UE_OnFall.Invoke();
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
