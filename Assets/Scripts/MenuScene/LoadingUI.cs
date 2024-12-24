using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingUI : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI loadingText;

    [Header("Load Time Settings")]
    public float minRanLoadTime;
    public float maxRanLoadTime;
    [Tooltip("Multiplies the loading speed")]
    public float loadFactor;
    private bool hasEnded = false;
    private float loadTime;
    public float currentTime;

    private void OnEnable()
    {
        currentTime = 0;
        loadTime = Random.Range(minRanLoadTime, maxRanLoadTime);
    }

    private void Update()
    {
        if(currentTime < loadTime)
        {
            currentTime += Time.deltaTime * loadFactor;
        }
        else
        {
            if (!hasEnded)
            {
                hasEnded = true;
                currentTime = loadTime;
                // code when complete here.
            }
        }
    }
}
