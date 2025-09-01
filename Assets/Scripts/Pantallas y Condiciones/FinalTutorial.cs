using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalTutorial : MonoBehaviour
{
    public int SiguienteEscena;

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Dark")){
            SceneManager.LoadScene(SiguienteEscena);
        }
    }
}
