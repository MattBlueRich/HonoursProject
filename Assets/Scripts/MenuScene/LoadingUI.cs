using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingUI : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI loadingText;
    public GameObject errorScreen;
    public GameObject mainMenuScreen;
    public SetupUI setupUI;
    public GameObject loadingUI;

    [Header("Load Time Settings")]
    public float minRanLoadTime;
    public float maxRanLoadTime;
    [Tooltip("Multiplies the loading speed")]
    public float loadFactor;
    private bool hasEnded = false;
    private float loadTime;
    public float currentTime;

    [Header("Audio")]
    public AudioSource audioSource;
    public MenuBGM menuBGM;

    [Header("Transition")]
    public Animator fadeAnimator;

    private void OnEnable()
    {
        audioSource.Play();
        hasEnded = false;
        currentTime = 0;

        loadTime = Random.Range(minRanLoadTime, maxRanLoadTime);
        InvokeRepeating("PrintTextUpdate", 0.1F, 0.1F);

        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == 0) continue;
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    // Update() runs a timer which lasts a random amount between min and max values. When it is finished, it either loads the error screen or main menu depending on if the headset is valid.
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
                audioSource.Stop();
                CancelInvoke("PrintTextUpdate");

                Debug.Log("LOADING COMPLETE!");

                if (setupUI.headsetValid)
                {
                    Debug.Log("Headset Valid!");

                    // Transition to main menu.
                    StartCoroutine(TransitionToMainMenu());
                }
                else
                {
                    
                    Debug.Log("Headset Invalid!");
                    
                    // Transition to error screen.
                    errorScreen.SetActive(true);
                }
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

    IEnumerator TransitionToMainMenu()
    {
        fadeAnimator.SetBool("FadeIn", true);
        yield return new WaitForSeconds(.5f);
        mainMenuScreen.SetActive(true);
        setupUI.gameObject.SetActive(false);
        fadeAnimator.SetBool("FadeIn", false);
        menuBGM.StopBGM();
    }
}
