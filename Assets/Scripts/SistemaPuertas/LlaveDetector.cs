using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LlaveDetector : MonoBehaviour
{
    public GameObject simboloExclamacion;
    public Puerta Puerta; // Referencia al script de la puerta

    private bool jugadorEnArea = false;
    private DarkMovement jugador;

    private void Update()
    {
        if (jugadorEnArea && jugador != null && jugador.TieneLlave && Input.GetKeyDown(KeyCode.E))
        {
            Puerta.AbrirPuerta();
            jugador.TieneLlave = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dark"))
        {
            jugadorEnArea = true;
            jugador = other.GetComponent<DarkMovement>();
            if (jugador != null && jugador.TieneLlave)
            {
                // Mostrar el símbolo de exclamación
                simboloExclamacion.SetActive(true);
            }
            else
            {
                // No tiene la llave, no puede pasar
                simboloExclamacion.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Dark"))
        {
            jugadorEnArea = false;
            simboloExclamacion.SetActive(false);
        }
    }
}

