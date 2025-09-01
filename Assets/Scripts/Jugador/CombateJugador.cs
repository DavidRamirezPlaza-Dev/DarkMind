using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateJugador : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform controladorGolpe;
    public float radioGolpe;
    public float dañoGolpe;
    private Animator Animator;
    public float tiempoEntreAtaques;
    public float tiempoSiguienteAtaque;

    private void Start(){
        Animator = GetComponent<Animator>();
    }

    private void Update(){
        if(tiempoSiguienteAtaque > 0){
            tiempoSiguienteAtaque -= Time.deltaTime;
        }
        if(Input.GetButtonDown("Fire1") && tiempoSiguienteAtaque <= 0){
            Golpe();
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }
    
    }

    private void Golpe(){
        Animator.SetTrigger("attack");
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos){
            if(colisionador.CompareTag("Enemigo")){
                colisionador.transform.GetComponent<Enemigo>().TomarDaño(dañoGolpe);
            }
            if(colisionador.CompareTag ("Jefe")){
                colisionador.transform.GetComponent<Mother>().TomarDaño(dañoGolpe);
            }
        }
    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}
