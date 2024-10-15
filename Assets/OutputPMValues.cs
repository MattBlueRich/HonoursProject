using dirox.emotiv.controller;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class OutputPMValues : MonoBehaviour
{
    public DataSubscriber dataSubscriber;
    public TextMeshProUGUI attText, strText, relText;

    public float currentAtt = 0.0f;
    public float currentStr = 0.0f;
    public float currentRel = 0.0f;

    public float tickSpeed = 20;

    private void Update()
    {

        // This increments / decreases a float variable by Time.deltaTime to equal a target value (its performance metric value).

        // Attention Counter

        if(currentAtt < (float)dataSubscriber.attention)
        {
            currentAtt += tickSpeed * Time.deltaTime;
            currentAtt = Mathf.Round(currentAtt * 10.0f) * 0.1f;
            attText.text = currentAtt.ToString("F1");
        }

        if (currentAtt > (float)dataSubscriber.attention)
        {
            currentAtt -= tickSpeed * Time.deltaTime;
            currentAtt = Mathf.Round(currentAtt * 10.0f) * 0.1f;
            attText.text = currentAtt.ToString("F1");
        }

        // Relaxation Counter

        if (currentRel < (float)dataSubscriber.relaxation)
        {
            currentRel += tickSpeed * Time.deltaTime;
            currentRel = Mathf.Round(currentRel * 10.0f) * 0.1f;
            relText.text = currentRel.ToString("F1");
        }

        if (currentRel > (float)dataSubscriber.relaxation)
        {
            currentRel -= tickSpeed * Time.deltaTime;
            currentRel = Mathf.Round(currentRel * 10.0f) * 0.1f;
            relText.text = currentRel.ToString("F1");
        }

        // Stress Counter

        if (currentStr < (float)dataSubscriber.stress)
        {
            currentStr += tickSpeed * Time.deltaTime;
            currentStr = Mathf.Round(currentStr * 10.0f) * 0.1f;
            strText.text = currentStr.ToString("F1");
        }

        if (currentStr > (float)dataSubscriber.stress)
        {
            currentStr -= tickSpeed * Time.deltaTime;
            currentStr = Mathf.Round(currentStr * 10.0f) * 0.1f;
            strText.text = currentStr.ToString("F1");
        }

    }
}
