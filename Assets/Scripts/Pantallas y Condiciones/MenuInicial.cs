using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuInicial : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private TMP_Text txtValorVolumen = null;
    [SerializeField] private Slider VolumenSlider = null;
    [SerializeField] private float DefaultVolume = 1.0f;

    [Header("Graphics Settings")]
    [SerializeField] private Slider BrilloSlider = null;
    [SerializeField] private TMP_Text txtValorBrillo = null;
    [SerializeField] private float DefaultBrightness = 1;

    [Space(10)]
    [SerializeField] private TMP_Dropdown CalidadDropdown;
    [SerializeField] private Toggle PantallaCompletaToggle;

    private int _qualityLevel;
    private bool _isFullScreen;
    private float _brightnessLevel;

    [Header("Confirmation")]
    [SerializeField] private GameObject comfirmationPrompt = null;

    [Header("Resolution Dropdowns")]
    public TMP_Dropdown ResolucionDropdown;
    private Resolution[] resolutions;

    private void Start()
    {
        // Cargar configuraciones guardadas
        LoadSettings();

        // Configuración de resoluciones
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        ResolucionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        ResolucionDropdown.AddOptions(options);
        ResolucionDropdown.value = currentResolutionIndex;
        ResolucionDropdown.RefreshShownValue();
    }

    private void LoadSettings()
    {
        // Aplicar las configuraciones del singleton GameSettings
        GameSettings.Instance.ApplySettings();

        // Aplicar las configuraciones a los elementos de UI
        VolumenSlider.value = GameSettings.Instance.MasterVolume;
        txtValorVolumen.text = GameSettings.Instance.MasterVolume.ToString("0.0");

        BrilloSlider.value = GameSettings.Instance.MasterBrightness;
        txtValorBrillo.text = GameSettings.Instance.MasterBrightness.ToString("0.0");

        CalidadDropdown.value = GameSettings.Instance.MasterQuality;
        PantallaCompletaToggle.isOn = GameSettings.Instance.IsFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void Jugar(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir(){
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        txtValorVolumen.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        GameSettings.Instance.SaveSettings(VolumenSlider.value, GameSettings.Instance.MasterBrightness, GameSettings.Instance.MasterQuality, GameSettings.Instance.IsFullScreen);
        StartCoroutine(ConfirmationBox());
    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        txtValorBrillo.text = brightness.ToString("0.0");
    }

    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }

    public void GraphicsApply()
    {
        GameSettings.Instance.SaveSettings(GameSettings.Instance.MasterVolume, BrilloSlider.value, CalidadDropdown.value, PantallaCompletaToggle.isOn);
        StartCoroutine(ConfirmationBox());
    }

    public void ResetButton(string MenuType)
    {
        if(MenuType == "Audio")
        {
            AudioListener.volume = DefaultVolume;
            VolumenSlider.value = DefaultVolume;
            txtValorVolumen.text = DefaultVolume.ToString("0.0");
            VolumeApply();
        }

        if (MenuType == "Graphics")
        {
            //Reset Brightness
            BrilloSlider.value = DefaultBrightness;
            txtValorBrillo.text = DefaultBrightness.ToString("0.0");
            //Reset Quality
            CalidadDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            PantallaCompletaToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            ResolucionDropdown.value = resolutions.Length;
            GraphicsApply();
        }
    }

    public IEnumerator ConfirmationBox()
    {
        comfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        comfirmationPrompt.SetActive(false);
    }
}
