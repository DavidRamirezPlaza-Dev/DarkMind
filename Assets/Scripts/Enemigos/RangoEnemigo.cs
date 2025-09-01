using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoEnemigo : MonoBehaviour
{

    public Animator Animator;
    public Enemigo Enemigo;
    void OnTriggerEnter2D(Collider2D Collider){

        if (Collider.CompareTag("Dark")){
            Animator.SetBool("Running", false);
            Animator.SetBool("Attack", true);

            Enemigo.Ataque = true;
            GetComponent<CapsuleCollider2D>().enabled = false;
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
