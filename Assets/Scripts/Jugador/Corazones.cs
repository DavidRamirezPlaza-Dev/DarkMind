using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Corazones : MonoBehaviour
{
    public List<Image> listaCorazones;
    public GameObject corazonPrefab;
    public DarkMovement DarkMovement;
    public int indexActual;
    public Sprite corazonLleno;
    public Sprite CorazonVacio;

    private void Awake(){
        DarkMovement.cambioVida.AddListener(CambiarCorazones);
    }

    private void CambiarCorazones(int vidaActual){
        if(!listaCorazones.Any()){
            crearCorazones(vidaActual);
        }else{
            cambiarVida(vidaActual);
        }
    }
    private void crearCorazones(int cantidadVidaMaxima){
        for (int i = 0; i < cantidadVidaMaxima; i++){
            GameObject corazon = Instantiate(corazonPrefab, transform);
            listaCorazones.Add(corazon.GetComponent<Image>());
        }
        indexActual = cantidadVidaMaxima - 1;
    }
    private void cambiarVida(int vidaActual){
        if (vidaActual <= indexActual){
            quitarCorazones(vidaActual);
        }else{
            agregarCorazones(vidaActual);
        }
    }
    private void quitarCorazones(int vidaActual){
        for(int i = indexActual; i >= vidaActual; i--){
            indexActual = i;
            listaCorazones[indexActual].sprite = CorazonVacio;
        }
    }
    private void agregarCorazones(int vidaActual){
        for (int i = indexActual; i < vidaActual; i++){
            indexActual = i;
            listaCorazones[indexActual].sprite = corazonLleno;
        }
    }

}
