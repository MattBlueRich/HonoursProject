using EmotivUnityPlugin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{   
    private Light _light;
    EmotivUnityItf _eItf = EmotivUnityItf.Instance;

    [Header("Light Angle")] // The angle FOV of the light is affected by the stress performance metrics.

    [Tooltip("When stress levels equal one:")]
    [Range(70, 120)]
    public float minAngle = 70f; // Minimum FOV of light when stress = 1.0f;

    [Tooltip("When stress levels equal zero:")]
    [Range(70, 120)]
    public float maxAngle = 120f; // Maximum FOV of light when stress = 0.0f;

    [ReadOnlyInspector][SerializeField] private float currentAngle;

    [Header("Light Brightness")] // The brightness of the light is affected by the attention performance metrics.

    [Tooltip("When attention levels equal zero:")]
    [Range(5, 100)]
    public float minBrightness = 5; // Minimum brightness of light when attention = 0.0f;

    [Tooltip("When attention levels equal one:")]
    [Range(5, 100)]
    public float maxBrightness = 100; // Maximum brightness of light when attention = 1.0f;

    [Tooltip("When attention levels equal zero:")]
    [Range(4, 10)]
    public float minRange = 4; // Minimum brightness of light when attention = 0.0f;

    [Tooltip("When attention levels equal one:")]
    [Range(4, 10)]
    public float maxRange = 10; // Maximum brightness of light when attention = 1.0f;

    [ReadOnlyInspector][SerializeField] private float currentBrightness;
    [ReadOnlyInspector][SerializeField] private float currentRange;

    private float currentStr = 0.0f;
    private float currentAtt = 0.0f;
    private float tickSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();  
    }

    // Update is called once per frame
    void Update()
    {       
        // Stress Counter (altered from OutputPMValues.cs to output values slower).

        if (currentStr < (float)_eItf.stressPow)
        {
            currentStr += tickSpeed * Time.deltaTime;
            UpdateLightAngle();
        }

        if (currentStr > (float)_eItf.stressPow)
        {
            currentStr -= tickSpeed * Time.deltaTime;
            UpdateLightAngle();
        }

        // Attention Counter (altered from OutputPMValues.cs to output values slower).

        if (currentAtt < (float)_eItf.attentionPow)
        {
            currentAtt += tickSpeed * Time.deltaTime;
            UpdateLightBrightness();
        }

        if (currentAtt > (float)_eItf.attentionPow)
        {
            currentAtt -= tickSpeed * Time.deltaTime;
            UpdateLightBrightness();
        }

    }
    public void UpdateLightAngle()
    {
        currentAngle = Mathf.Lerp(maxAngle, minAngle, currentStr);
        _light.spotAngle = currentAngle;
        _light.innerSpotAngle = currentAngle;
    }

    public void UpdateLightBrightness()
    {
        currentBrightness = Mathf.Lerp(minBrightness, maxBrightness, currentAtt);
        currentRange = Mathf.Lerp(minRange, maxRange, currentAtt);
        _light.intensity = currentBrightness;
        _light.range = currentRange;
    }
}
