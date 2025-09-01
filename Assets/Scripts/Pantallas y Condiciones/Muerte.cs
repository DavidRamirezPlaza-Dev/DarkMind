using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Muerte : MonoBehaviour
{
    

    public void btnReinicio(){
        string previousScene = PlayerPrefs.GetString("PreviousScene", "Tutorial"); // Por defecto, vuelve a "Tutorial" si no se encuentra el valor
        SceneManager.LoadScene(previousScene);
    }
    public void btnMenu(){
        SceneManager.LoadScene(1);
    }

}
