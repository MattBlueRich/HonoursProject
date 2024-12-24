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
        InvokeRepeating("PrintTextUpdate", 0.1F, 0.1F);

        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == 0) continue;
            Destroy(transform.GetChild(i).gameObject);
        }
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
                CancelInvoke("PrintTextUpdate");
                Debug.Log("LOADING COMPLETE!");
                // code when complete here.
            }
        }
    }

    public void PrintTextUpdate()
    {
        StartCoroutine("DelayPrint");
    }

    IEnumerator DelayPrint()
    {
        yield return new WaitForSeconds(Random.Range(0.3f, 1.0f));

        float percent = currentTime / loadTime * 100;
        float roundedPercent = Mathf.Round(percent * 100) / 100;

        if (roundedPercent < 100)
        {
            TextMeshProUGUI textUpdate = Instantiate(loadingText, this.transform);
            textUpdate.text = "LOADING... (" + roundedPercent + "% COMPLETED)";
        }
    }
}
