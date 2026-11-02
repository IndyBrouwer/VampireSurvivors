using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private AudioMixer Mixer;

    private void Start()
    {
        //Load saved values, or use default if they dont exist
        float savedMasterVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0f);

        //Apply saved (or default) settings to sliders
        masterSlider.value = savedMasterVolume;
        musicSlider.value = savedMusicVolume;
        sfxSlider.value = savedSFXVolume;

        //Apply created settings to the audio mixer
        ApplyMasterVolume(savedMasterVolume);
        ApplyMusicVolume(savedMusicVolume);
        ApplySFXVolume(savedSFXVolume);
    }

    public void ApplyMasterVolume(float value)
    {
        if (Mixer != null)
        {
            Mixer.SetFloat("MasterVolume", value);
            PlayerPrefs.SetFloat("MasterVolume", value);
        }
    }

    public void ApplyMusicVolume(float value)
    {
        if (Mixer != null)
        {
            Mixer.SetFloat("MusicVolume", value);
            PlayerPrefs.SetFloat("MusicVolume", value);
        }
    }

    public void ApplySFXVolume(float value)
    {
        if (Mixer != null)
        {
            Mixer.SetFloat("SFXVolume", value);
            PlayerPrefs.SetFloat("SFXVolume", value);
        }
    }
}
