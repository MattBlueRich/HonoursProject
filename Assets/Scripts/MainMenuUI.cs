using EmotivUnityPlugin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    EmotivUnityItf _eItf = EmotivUnityItf.Instance;
    List<string> _streams = new List<string> { "met", "com" };

    public void LoadGameScene()
    {
        _eItf.SubscribeData(_streams); // Subscribes to mental commands and performance metrics data streams.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
