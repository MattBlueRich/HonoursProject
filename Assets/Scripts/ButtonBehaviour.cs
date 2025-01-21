using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] buttonHoverSFX;
    public AudioClip[] buttonClickSFX;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OnButtonHover()
    {
        CursorManager.instance.SetCursorInteract();

        audioSource.volume = 0.15f;
        audioSource.pitch = Random.Range(0.85f, 1f);
        audioSource.clip = buttonHoverSFX[Random.Range(0, buttonHoverSFX.Length)];
        audioSource.Play();
    }

    public void OnButtonExit()
    {
        CursorManager.instance.SetCursorNormal();
    }

    public void OnButtonClick()
    {
        audioSource.volume = 0.5f;
        audioSource.pitch = Random.Range(0.85f, 1f);
        audioSource.clip = buttonClickSFX[Random.Range(0, buttonClickSFX.Length)];
        audioSource.Play();
    }
}
