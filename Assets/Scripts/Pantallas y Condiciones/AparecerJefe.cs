using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparecerJefe : MonoBehaviour
{
    public GameObject Jefe;
    public GameObject BarraDeVida;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dark"))
        {
            Jefe.SetActive(true);
            BarraDeVida.SetActive(true);
            gameObject.SetActive(false); // Desactiva el GameObject que contiene este script y el collider
        }
    }
}
