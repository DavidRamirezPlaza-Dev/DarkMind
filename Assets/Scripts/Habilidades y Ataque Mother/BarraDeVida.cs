using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    private Slider slider;
    private float cantidadVida;
    public GameObject BarraVida;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }
    
    
    public void CambiarVidaActual(float cantidadVida){
        slider.value = cantidadVida;
        
    }
    public void InicializarBarraVida(float CantidadDaño){
        CambiarVidaActual(cantidadVida);
    }
}
