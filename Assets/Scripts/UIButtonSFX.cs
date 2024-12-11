using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSFX : MonoBehaviour, ISelectHandler
{
    public AudioClip[] SFX;
    private AudioSource audioSource;

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
