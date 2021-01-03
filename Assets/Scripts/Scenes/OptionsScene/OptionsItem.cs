using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OptionsItem : MonoBehaviour
{


    [Header("Settings")]
    public Button btn_Left;
    public Button btn_Center;
    public Button btn_Right;



    /*
        Opciones Scene posibles actualmente
           Traduccion español / ingléso
           Velocidad de los textos (low,normal,speed)
           Musica(No, Bajo, Medio, Alto)
           Sfx(No,Si)
           Controles(clasico, alternativo)
        */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

/*
 TODO

    Encargado de llamar al manager de Options y, sobre un arreglo de opciones
    decidir si ir para adelante o para atrás, al hacer eso
    el Manager actualizará los valores y el Datapass
 
 */