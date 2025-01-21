using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorUI : MonoBehaviour
{
    public SetupUI setupUI;
    public AudioSource audioSource;
    public AudioClip[] errorSFX;
    private void OnEnable()
    {
        audioSource.clip = errorSFX[Random.Range(0, errorSFX.Length)];
        audioSource.pitch = Random.Range(0.85f, 1.0f);
        audioSource.Play();
    }

    public void InstallButton()
    {
        Application.OpenURL("https://www.emotiv.com/pages/download-emotiv-launcher?srsltid=AfmBOopRZ0a5ecA47lYEQaGVs9WxZzS0bZzqIGwMToEd1Aq60U9ji6Ox");
    }
}
