using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundfxSource;
    [SerializeField] private Slider musicSlider, soundfxSlider;
    [SerializeField] private GameObject settingsScreen;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("MusicVolume"))
            PlayerPrefs.SetInt("MusicVolume", 3);

        if (!PlayerPrefs.HasKey("SoundFXVolume"))
            PlayerPrefs.SetInt("SoundFXVolume", 100);

        musicSlider.value = PlayerPrefs.GetInt("MusicVolume");
        soundfxSlider.value = PlayerPrefs.GetInt("SoundFXVolume");
    }


    public void ChangeMusicVolume()
    {
        musicSource.volume = musicSlider.value / 100;
    }

    public void ChangeSoundFXVolume()
    {
        soundfxSource.volume = musicSlider.value / 100;
    }


    public void SaveSettings()
    {
        PlayerPrefs.SetInt("MusicVolume", (int)musicSlider.value);
        PlayerPrefs.SetInt("SoundFXVolume", (int)soundfxSlider.value);
        settingsScreen.SetActive(false);
    }

    public void SettingsScreenOn()
    {
        settingsScreen.SetActive(true);
    }
}
