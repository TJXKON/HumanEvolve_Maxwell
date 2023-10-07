using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LevelBrightnessController : MonoBehaviour
{
    public PostProcessProfile levelBrightness;

    private AutoExposure exposure;

    private void Start()
    {
        // Access the AutoExposure component from the PostProcessProfile
        exposure = levelBrightness.GetSetting<AutoExposure>();

        // Load brightness value from PlayerPrefs
        float brightnessValue = PlayerPrefs.GetFloat("masterBrightness", 1.0f); // Default value if not found

        if (exposure != null)
        {
            exposure.keyValue.value = brightnessValue;
        }
    }
}
