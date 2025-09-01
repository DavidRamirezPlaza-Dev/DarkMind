using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueNormal : MonoBehaviour
{
    public float speed;
    private Vector2 Direction;
    private Rigidbody2D Rigidbody2D;

    public int dañoGolpeEnemigo;

    void OnTriggerEnter2D(Collider2D Collider)
    {
        if (Collider.CompareTag("Dark"))
        {
            
            Collider.transform.GetComponent<DarkMovement>().RecibirDaño(dañoGolpeEnemigo);
            Destroy(gameObject);
            
        }
    }

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Movimiento del ataque
        Rigidbody2D.velocity = Direction * speed;
    }

    // Método para establecer la dirección del ataque
    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
