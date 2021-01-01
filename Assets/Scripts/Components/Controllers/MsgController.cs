#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion
public class MsgController : MonoBehaviour
{
    #region var

    [Header("Provisional")]
    //Esto sera sutituido por arreglos de strings...
    [TextArea(3,6)]
    public string _textoEjemplo = "Fue hace mucho tiempo....";
    public char[] _chars;
    private int _longitud = 0;
    private int _index = 0;
    [Space]

    [Header("Settings")]
    
    public Text txt_msg;
    public float font_default = 40;
    public bool useInitialText = true;

    [Space]
    public float waitTime = 0.20f;
    public float timeVariance = 0.1f;
    #endregion
    #region Events
    private void Awake(){

        if (useInitialText){
            _textoEjemplo = txt_msg.text;
        }
        _chars = _textoEjemplo.ToCharArray();
        _longitud = _chars.Length + 1;

        StartCoroutine(SetText());
    }
    #endregion
    #region Methods

    IEnumerator SetText(){

        yield return new WaitForSeconds(Random.Range(waitTime-timeVariance,waitTime+timeVariance));
        if (_index != _longitud)
        {
            txt_msg.text = new string(_textoEjemplo.ToCharArray(0, _index));
            _index++;
            StartCoroutine(SetText());
        }
    }

    //agregar al text caracter por caracter, poner el string en un array
    //Se tiene que hacer animación de fade
    //Poder cambiar de diferentes "Mensajes" que son cadenas de string

    #endregion
}
/*TODO
Componente enfocado a mostrar y esconder cadenas de string cada cierto tiempo

Este componente mostrará texto basado en un arreglo y esta puede ser
secuencial o aleatorio


TODO -> Se plantea usar para: MenuManager,

    */
/*

    El Texto irá renderizando caracter por caracter, como si fuesen tipo chats
    Nosotros veremos como se va escribiendo...
 


    Tener una fuente estandar
        Cuanta fuente se le debe añadir por cada pixel de pantalla basado en la estandar?
        Esto se refresca solo cuando las dimensiones de la pantalla cambian....
 */