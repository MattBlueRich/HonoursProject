using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    AudioSource audioSource;
    AudioLowPassFilter lowPassFilter;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lowPassFilter = GetComponent<AudioLowPassFilter>();
    }
    public void ToggleLowPass()
    {
        lowPassFilter.enabled = !lowPassFilter.enabled;
    }
}
