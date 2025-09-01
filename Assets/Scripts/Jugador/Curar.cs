using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curar : MonoBehaviour
{
     public int curación;

    private void OnTriggerEnter2D(Collider2D other)
    {
        DarkMovement jugador = other.GetComponent<DarkMovement>();
        if (jugador != null && jugador.CompareTag("Dark") && jugador.PuedeCurarse())
        {
            jugador.CurarVida(curación);
            Destroy(gameObject); // Destruir el objeto corazón después de curar
        }
    }
}
