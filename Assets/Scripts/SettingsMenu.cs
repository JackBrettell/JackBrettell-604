using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private AudioListener audioListener;



    private void Start()
    {
        // Load settings on start
        masterVolumeSlider.value = SettingsManager.LoadFloat("MasterVolume", 1f);

    }

    public void OnMasterVolumeChanged(float value)
    {
        SettingsManager.SaveSetting("MasterVolume", value);
        AudioListener.volume = value; 
    }

}
