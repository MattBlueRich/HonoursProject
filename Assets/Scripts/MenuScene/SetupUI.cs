using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;
using EmotivUnityPlugin;
using UnityEngine.Windows;
using UnityEngine.EventSystems;

public class SetupUI : MonoBehaviour
{
    [Header("Setup UI")]
    [SerializeField] private TMP_InputField headsetIDField; // This is the input field from the "Example UI" panel, for storing the inputted headset ID (headset type + ID).
    public CanvasGroup[] section1UI;
    public CanvasGroup[] section2UI;
    public TextMeshProUGUI selectHeadsetText;

    private string typeSuffix = string.Empty;
    private string idSuffix = string.Empty;

    [Header("Input")]
    public string headsetInput = string.Empty;

    [HideInInspector] public bool headsetValid = false;

    EmotivUnityItf _eItf = EmotivUnityItf.Instance;

    private void Start()
    {
        SetDefaultValues();
    }

    public void SetDefaultValues()
    {
        // Set default headset type value.
        SetHeadsetType("MN8");

        // Focus on headset type button selection.
        SetupUIFocus(1);
    }

    public void SetHeadsetType(string type)
    {
        typeSuffix = type;
        UpdateHeadsetInput();
        selectHeadsetText.text = "SELECT YOUR HEADSET TYPE: (>" + typeSuffix + ")";
    }

    // This is used by the headset ID input field to make all the letters capitalized.
    public void CapitalizeInput()
    {
        headsetIDField.text = headsetIDField.text.ToUpper();
    }

    // When the player enters their ID in the headset ID input field, this checks if the entered ID is valid and enables the confirm navigation button.
    public void SetHeadsetID()
    {

        idSuffix = headsetIDField.text;
        UpdateHeadsetInput();

        _eItf.CreateSessionWithHeadset(headsetInput); // Submit headset ID.

        if (_eItf.headsetValid)
        {
            headsetValid = true;
        }
        else
        {            
            headsetValid = false;
            // Reset invalid information.
            idSuffix = string.Empty;
            headsetIDField.text = string.Empty;
        }
    }
    public void SetProfile(string profileID)
    {
        _eItf.LoadProfile(profileID);
    }

    public void UpdateHeadsetInput()
    {
        headsetInput = typeSuffix + "-" + idSuffix;
    }

    public void SetupUIFocus(int sectionToFocus)
    {
        switch(sectionToFocus)
        {
            case 1:
                for (int i = 0; i < section1UI.Length; i++)
                {
                    section1UI[i].alpha = 1;
                }
                for (int i = 0; i < section2UI.Length; i++)
                {
                    section2UI[i].alpha = 0.3f;
                }
                break;
            case 2:
                for (int i = 0; i < section1UI.Length; i++)
                {
                    section1UI[i].alpha = 0.3f;
                }
                for (int i = 0; i < section2UI.Length; i++)
                {
                    section2UI[i].alpha = 1.0f;
                }
                break;
        }
    }
}
