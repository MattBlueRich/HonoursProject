using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class SetupUI : MonoBehaviour
{
    [Header("Example UI")]
    public SimpleExample simpleExample;
    public InputField headsetID;

    public void SetHeadsetType(string prefix)
    {
        headsetID.text = prefix;
    }
}
