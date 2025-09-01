using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class intro : MonoBehaviour
{
    // Start is called before the first frame update
   public float delay = 3f;
    public int SiguienteEscena;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("cambiarEscena", delay);
    }

    void cambiarEscena(){
        SceneManager.LoadScene(SiguienteEscena);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit")){
            SceneManager.LoadScene(SiguienteEscena);
        }
    }
}
