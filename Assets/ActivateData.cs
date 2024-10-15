using dirox.emotiv.controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using EmotivUnityPlugin;

public class ActivateData : MonoBehaviour
{
    public DataSubscriber dataSubscriber;

    private void Start()
    {
        dataSubscriber.Activate();
        dataSubscriber.onPMSubBtnClick();
    }
}
