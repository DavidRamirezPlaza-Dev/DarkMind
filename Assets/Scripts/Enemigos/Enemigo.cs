using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Enemigo: MonoBehaviour
{

    public float vida;
    private Animator Animator;
    public int Rutina;
    public float Cronometro;
    public int direccion;
    public float speed_run;
    public bool Ataque;
    public GameObject Target;
    public float rango_vision;
    public float rango_ataque;
    public GameObject Rango;
    public GameObject HitPj;

    
    

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("Dark");
        Animator = GetComponent<Animator>();
    }

    public void final_Animation(){
        Animator.SetBool("Attack", false);
        Ataque = false;
        Rango.GetComponent<CapsuleCollider2D>().enabled = true;
    }
    public void ColliderWeaponTrue(){
        HitPj.GetComponent<CapsuleCollider2D>().enabled = true;
    }
    public void ColliderWeaponFalse(){
        HitPj.GetComponent<CapsuleCollider2D>().enabled = false;
    }

    void Update(){
        Comportamientos();
    }

    //Comportamiento de ataque

    


    public void TomarDaño(float daño){
        vida -= daño;
        if(vida <= 0)
        {
            Muerte();
        }else{
            Hit();
        }
    }
    private void Muerte(){
        Animator.SetTrigger("Muerte");
    }
    private void Hit(){
        Animator.SetTrigger("Hit");
    }
    public void Destroy(){
        Destroy(gameObject);
    }

    public void Comportamientos(){
        if(Mathf.Abs(transform.position.x - Target.transform.position.x) > rango_vision && !Ataque){
            Animator.SetBool("Running",false);
            Cronometro +=1 * Time.deltaTime;
            if (Cronometro >= 1){
                Rutina = Random.Range(0,2);
                Cronometro = 0;
            }
            switch (Rutina){
                case 0:
                    Animator.SetBool("Running", false);
                    break;
                case 1:
                    direccion = Random.Range(0,2);
                    Rutina++;
                    break;
                case 2:
                    switch(direccion){
                        case 0:
                            transform.rotation = Quaternion.Euler(0,0,0);
                            transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                            break;
                        case 1:
                            transform.rotation = Quaternion.Euler(0,180,0);
                            transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                            break;
                    }
                    Animator.SetBool("Running",true);
                    break;
            }
        }else{
            if (Mathf.Abs(transform.position.x - Target.transform.position.x) < rango_ataque && !Ataque){
                if(transform.position.x < Target.transform.position.x){
                    Animator.SetBool("Running",true);
                    transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0,0,0);
                    Animator.SetBool("Attack", false);
                }else{
                    Animator.SetBool("Running",true);
                    transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0,180,0);
                    Animator.SetBool("Attack", false);
                }
            }else{
                if(!Ataque){
                    if(transform.position.x < Target.transform.position.x){
                        transform.rotation = Quaternion.Euler(0,0,0);
                    }else{
                        transform.rotation = Quaternion.Euler(0,180,0);
                    }
                    Animator.SetBool("Running",false);
                }
            }
        }
    }
}