using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBGM : MonoBehaviour
{
    public AudioClip startUpSFX;
    public AudioClip ambienceSFX;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.loop = false;
        StartCoroutine(StartBGM());
    }
    IEnumerator StartBGM()
    {
        audioSource.clip = startUpSFX;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.clip = ambienceSFX;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }
}
