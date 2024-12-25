using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorUI : MonoBehaviour
{
    public SetupUI setupUI;

    public void InstallButton()
    {
        Application.OpenURL("https://www.emotiv.com/pages/download-emotiv-launcher?srsltid=AfmBOopRZ0a5ecA47lYEQaGVs9WxZzS0bZzqIGwMToEd1Aq60U9ji6Ox");
    }
}
