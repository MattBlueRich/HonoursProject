using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/* https://discussions.unity.com/t/lerp-from-a-to-b-with-time-x-help-required/678242/10 */

public class Transition : MonoBehaviour
{
    public float transitionTime;
    public bool fadeOutOnStart;
    public CanvasGroup transitionFadeBG;

    private void Start()
    {
        if (fadeOutOnStart)
        {
            FadeOut();
        }
    }

    public void FadeIn(string sceneName)
    {
        StartCoroutine(Fade(true, sceneName));
    }
    public void FadeOut()
    {
        StartCoroutine(Fade(false, string.Empty));
    }

    private IEnumerator Fade(bool _fadeIn, string _sceneName)
    {
        if(_fadeIn)
        {
            StartCoroutine(FadeBGIn());
        }
        else
        {
            StartCoroutine(FadeBGOut());
        }

        yield return new WaitForSeconds(transitionTime);
        
        if(_sceneName != string.Empty)
        {
            SceneManager.LoadScene(_sceneName);
        }
    }

    private IEnumerator FadeBGIn()
    {
        float lerp = 0;
        transitionFadeBG.alpha = 0;

        while (transitionTime > 0 && lerp < 1)
        {
            lerp = Mathf.MoveTowards(lerp, 1, Time.deltaTime / transitionTime);
            transitionFadeBG.alpha = Mathf.Lerp(0, 1, lerp);

            yield return null;
        }
        transitionFadeBG.alpha = 1;
    }

    private IEnumerator FadeBGOut()
    {
        float lerp = 1;
        transitionFadeBG.alpha = 1;

        while (transitionTime > 0 && lerp > 0)
        {
            lerp = Mathf.MoveTowards(lerp, 0, Time.deltaTime / transitionTime);
            transitionFadeBG.alpha = Mathf.Lerp(0, 1, lerp);

            yield return null;
        }
        transitionFadeBG.alpha = 0;
    }
}
