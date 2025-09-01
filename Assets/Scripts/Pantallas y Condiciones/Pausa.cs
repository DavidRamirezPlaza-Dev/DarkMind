using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    private bool juegoPausado = false;
    public GameObject menu;



    private void Update(){

        if(Input.GetKeyDown(KeyCode.Escape)){
            if (juegoPausado){
                Reanudar();
            }else{
                Pausar();
            }
        }
    }

    public void Pausar(){
        Time.timeScale = 0f;
        juegoPausado = true;
        menu.SetActive(true);
        
    }

    public void Reanudar(){
        Time.timeScale = 1f;
        juegoPausado = false;
        menu.SetActive(false);
        
    }
    public void Reiniciar(){
        Time.timeScale = 1f;
        menu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Cerrar(){
        SceneManager.LoadScene(1);
    }
}
