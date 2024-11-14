using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;
using EmotivUnityPlugin;
using UnityEngine.Windows;

public class SetupUI : MonoBehaviour
{
    [Header("Example UI")]
    public SimpleExample simpleExample;

    [Header("Setup UI")]
    public GameObject[] screens;
    public TMP_InputField headsetIDField;
    public UnityEngine.UI.Button screen2ConfirmButton;
    public UnityEngine.UI.Button screen2ReturnButton;
    public GameObject errorText;

    EmotivUnityItf _eItf = EmotivUnityItf.Instance;

    string headsetType;

    private void Start()
    {
        SwitchToScreen(1);
    }

    public void SetHeadsetType(string type)
    {
        headsetType = type;
    }

    // This is used by the navigation buttons to move between the Setup UI windows.
    public void SwitchToScreen(int screenNo)
    {     
        for (int i = 0; i < screens.Length; i++)
        {
            if(i+1 == screenNo)
            {
                screens[i].SetActive(true);
            }
            else
            {
                screens[i].SetActive(false);
            }
        }
    }

    // This is used by the headset ID input field to make all the letters capitalized.
    public void CapitalizeInput()
    {
        headsetIDField.text = headsetIDField.text.ToUpper();
    }

    // When the player enters their ID in the headset ID input field, this checks if the entered ID is valid and enables the confirm navigation button.
    public void SetHeadsetID(string ID)
    {
        string input = headsetType + "-" + ID;

        _eItf.CreateSessionWithHeadset(input); // Submit headset ID.

        if (_eItf.headsetValid)
        {
            screen2ConfirmButton.interactable = true;
            screen2ReturnButton.interactable = false;
            errorText.SetActive(false);
        }
        else
        {
            errorText.SetActive(true);
            input = headsetType;
        }
    }

    // This is used by the return button in the second setup UI screen ("(2) Insert Headset ID") to reset screen 2.
    public void ClearID()
    {
        errorText.SetActive(false);
        headsetIDField.text = string.Empty;
    }

    public void SetProfile(string profileID)
    {
        _eItf.LoadProfile(profileID);
    }
}
