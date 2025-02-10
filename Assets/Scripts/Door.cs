using Cinemachine;
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

    public bool pullCameraFocus = false;
    public CinemachineVirtualCamera virtualCam;
    public float camTime = 1f;
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
        if(pullCameraFocus && virtualCam != null)
        {
            StartCoroutine(CamFocus());
        }
        else
        {
            doorAnimator.SetBool("IsOpen", !doorAnimator.GetBool("IsOpen"));
        }
    }
    public void DisableDoor()
    {
        canInteract = false;
    }

    public void EnableDoor()
    {
        canInteract = true;
    }

    private IEnumerator CamFocus()
    {
        Transform player = virtualCam.Follow;
        virtualCam.Follow = this.transform;

        yield return new WaitForSeconds(camTime/2);
        doorAnimator.SetBool("IsOpen", !doorAnimator.GetBool("IsOpen"));

        yield return new WaitForSeconds(camTime/2);
        virtualCam.Follow = player;
    }
}
