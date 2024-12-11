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
    [Header("Example UI")]
    public SimpleExample simpleExample;

    [Header("Setup UI")]
    [SerializeField] private GameObject[] menuScreens; // This refers to the different UI panels the menu switches between.
    [SerializeField] private TMP_InputField headsetIDField; // This is the input field from the "Example UI" panel, for storing the inputted headset ID (headset type + ID).
    public UnityEngine.UI.Button screen2ConfirmButton;
    public UnityEngine.UI.Button screen2ReturnButton;
    public GameObject errorText;

    [Header("Transition")]
    [SerializeField] private Animator FadeTransitionAnimator; // This animator performs a basic fade transition between different UI panel screens.
    [SerializeField] private float transitionTime = 1.0f;

    EmotivUnityItf _eItf = EmotivUnityItf.Instance;

    string headsetType;
    EventSystem eventSystem;
    [Header("Default Selected Buttons")]
    [SerializeField] private GameObject screen1Default;
    private void OnEnable()
    {
        eventSystem = EventSystem.current;
    }

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
        switch (screenNo)
        {
            case 1:
                eventSystem.SetSelectedGameObject(screen1Default);
                break;
        }

        StartCoroutine(SwitchScreenWithFade(screenNo));
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

    IEnumerator SwitchScreenWithFade(int num)
    {
        FadeTransitionAnimator.SetBool("FadeIn", true);

        yield return new WaitForSeconds(transitionTime);

        for (int i = 0; i < menuScreens.Length; i++)
        {
            if (i + 1 == num)
            {
                menuScreens[i].SetActive(true);
                
            }
            else
            {
                menuScreens[i].SetActive(false);
            }
        }

        FadeTransitionAnimator.SetBool("FadeIn", false);
    }
}
