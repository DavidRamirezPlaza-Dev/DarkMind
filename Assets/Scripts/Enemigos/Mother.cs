using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mother : MonoBehaviour
{
    private Animator animator;
    public GameObject ataque;
    public GameObject habilidad;
    public Rigidbody2D rb2D;
    public Transform jugador;
    public GameObject Llave;
    private bool mirandoDerecha = true;
    public BarraDeVida barraDeVida;
    public GameObject BarraVida;

    public float vida;

    // Start is called before the first frame update
    void Start()
    {
        
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        jugador = GameObject.FindGameObjectWithTag("Dark").GetComponent<Transform>();
        barraDeVida.InicializarBarraVida(vida);
    }

    void Update()
    {
        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);
        animator.SetFloat("distanciaJugador", distanciaJugador);
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        barraDeVida.CambiarVidaActual(vida);
        if (vida <= 0)
        {
            animator.SetTrigger("Muerte");
            BarraVida.SetActive(false);
        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }

    private void Muerte()
    {
        Instantiate(Llave, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void MirarJugador()
    {
        if ((jugador.position.x > transform.position.x && !mirandoDerecha) || (jugador.position.x < transform.position.x && mirandoDerecha))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

    // Método para instanciar el ataque
    public void Atacar()
    {
        GameObject nuevoAtaque = Instantiate(ataque, transform.position, Quaternion.identity);
        AtaqueNormal ataqueScript = nuevoAtaque.GetComponent<AtaqueNormal>();
        if (mirandoDerecha)
        {
            ataqueScript.SetDirection(Vector2.right);
            nuevoAtaque.transform.localScale = new Vector3(-1, 1, 1); // Asegúrate de que la escala sea correcta para la dirección derecha
        }
        else
        {
            ataqueScript.SetDirection(Vector2.left);
            nuevoAtaque.transform.localScale = new Vector3(1, 1, 1); // Voltea el ataque horizontalmente para la dirección izquierda
        }
    }

    private void UsarHabilidad()
    {
        GameObject nuevaHabilidad = Instantiate(habilidad, transform.position, Quaternion.identity);
        AtaqueNormal habilidadScript = nuevaHabilidad.GetComponent<AtaqueNormal>(); // Asegúrate de que la habilidad también tenga el script AtaqueNormal
        if (mirandoDerecha)
        {
            habilidadScript.SetDirection(Vector2.right);
            nuevaHabilidad.transform.localScale = new Vector3(-1, 1, 1); // Asegúrate de que la escala sea correcta para la dirección derecha
        }
        else
        {
            habilidadScript.SetDirection(Vector2.left);
            nuevaHabilidad.transform.localScale = new Vector3(1, 1, 1); // Voltea la habilidad horizontalmente para la dirección izquierda
        }
    }
}
