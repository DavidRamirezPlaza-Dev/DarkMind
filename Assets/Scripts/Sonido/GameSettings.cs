using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance { get; private set; }

    public float MasterVolume { get; private set; }
    public float MasterBrightness { get; private set; }
    public int MasterQuality { get; private set; }
    public bool IsFullScreen { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        LoadSettings();
    }

    public void LoadSettings()
    {
        MasterVolume = PlayerPrefs.GetFloat("masterVolume", 1.0f);
        MasterBrightness = PlayerPrefs.GetFloat("masterBrightness", 1.0f);
        MasterQuality = PlayerPrefs.GetInt("masterQuality", 1);
        IsFullScreen = PlayerPrefs.GetInt("masterFullScreen", 1) == 1;
    }

    public void ApplySettings()
    {
        AudioListener.volume = MasterVolume;
        QualitySettings.SetQualityLevel(MasterQuality);
        Screen.fullScreen = IsFullScreen;

        // Aquí puedes aplicar el brillo a tu sistema de iluminación si tienes uno
        // Por ejemplo:
        // RenderSettings.ambientLight = Color.white * MasterBrightness;
    }

    public void SaveSettings(float volume, float brightness, int quality, bool isFullScreen)
    {
        MasterVolume = volume;
        MasterBrightness = brightness;
        MasterQuality = quality;
        IsFullScreen = isFullScreen;

        PlayerPrefs.SetFloat("masterVolume", volume);
        PlayerPrefs.SetFloat("masterBrightness", brightness);
        PlayerPrefs.SetInt("masterQuality", quality);
        PlayerPrefs.SetInt("masterFullScreen", isFullScreen ? 1 : 0);
        PlayerPrefs.Save();

        ApplySettings();
    }
}
