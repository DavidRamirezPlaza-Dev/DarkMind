using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsLoader : MonoBehaviour
{
     private void Start()
    {
        GameSettings.Instance.ApplySettings();
    }
}
