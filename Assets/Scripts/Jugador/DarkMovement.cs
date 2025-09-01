using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DarkMovement : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    public float speed;
    public int vida;
    public float jumpForce;
    public UnityEvent<int> cambioVida;
    public int ScenaActual;
    public bool TieneLlave = false;
    private void jump(){
        Rigidbody2D.AddForce(Vector2.up * jumpForce);
    }
    
    private bool Grounded;

    // Start is called before the first frame update
    void Start()
    {
        // Trae el componente Rigidbody2D
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent <Animator>();
        cambioVida.Invoke(vida);
    }

    // Update is called once per frame
    void Update()
    {
        // Asigna el valor de -1 a 1 a la variable de instancia Horizontal
        Horizontal = Input.GetAxisRaw("Horizontal");
        if(Horizontal < 0.0f){
            transform.localScale = new Vector3(-1.0f, 1.0f,1.0f);
        }else if(Horizontal > 0.0f){
            transform.localScale = new Vector3 (1.0f,1.0f,1.0f);
        }
        
        if (Grounded){
            Animator.SetBool("running", Horizontal != 0.0f);
        }else{
            Animator.SetBool("running",false);
        }
        
        

        Debug.DrawRay(transform.position, Vector3.down * 0.2f, Color.red);

        if (Physics2D.Raycast(transform.position, Vector3.down, 0.2f)){
            Grounded = true;
            Animator.SetBool("jumping", false);
        }else{
            Grounded = false;
            Animator.SetBool("jumping", true);
        }

        if(Input.GetKeyDown(KeyCode.Space) && Grounded){
            jump();            
        }

    }
    public void RecibirDaño(int daño){
        vida -= daño;
        cambioVida.Invoke(vida);
        if (vida <= 0){
            Animator.SetTrigger("Death");
            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            StartCoroutine(LoadDeathScene());
        }else{
            Animator.SetTrigger("Hit");
        }
    }
    public void CurarVida(int cantidadCuracion){
        vida = Mathf.Min(vida + cantidadCuracion, 5);
        cambioVida.Invoke(vida);
    }
    public bool PuedeCurarse(){
        return vida < 5; // Retorna true si la vida es menor que el máximo
    }
    public void Destroy(){
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Llave"))
        {
            Destroy(other.gameObject); // Destruir la llave del mundo
            TieneLlave = true;
            Debug.Log("Llave Conseguida");
            // Aquí podrías mostrar un mensaje o cambiar algún estado visual para indicar que el jugador tiene la llave
        }
    }

     private IEnumerator LoadDeathScene()
    {
        yield return new WaitForSeconds(2.2f); // Ajusta el tiempo para que coincida con la duración de tu animación de muerte
        SceneManager.LoadScene("Muerte");
    }
    

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        // Aplica la velocidad al Rigidbody2D
        Rigidbody2D.velocity = new Vector2(Horizontal * speed, Rigidbody2D.velocity.y);
    }
}
