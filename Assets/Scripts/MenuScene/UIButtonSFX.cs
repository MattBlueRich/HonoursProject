using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSFX : MonoBehaviour, ISelectHandler
{
    public AudioClip[] SFX;
    private AudioSource audioSource;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.keepAnimatorControllerStateOnDisable = true; // This is a bug-fix for the button animation when this GameObject becomes inactive.
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomSFX()
    {
        int randomPick = Random.Range(0, SFX.Length);

        audioSource.clip = SFX[randomPick];
        audioSource.pitch = Random.Range(0.9f, 1.0f);
        audioSource.Play();
    }

    public void OnSelect(BaseEventData eventData)
    {
        PlayRandomSFX();
    }

}
