using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public string sceneName;
    public Transition transition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(sceneName != null)
            {
                other.GetComponent<PlayerController>().disableAllMovement = true;
                transition.FadeIn(sceneName);
            }
        }
    }

}
