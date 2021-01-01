#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion
public class MsgController : MonoBehaviour
{
    #region var
    private float fontSizeMax;


    [Header("Settings")]
    public Text txt_msg;
    public bool useInitialText = true;

    [Space]
    public string text = null;

    [Space]
    [Header("Time")]
    public float delayTime = 0;
    public float waitTime = 0.20f;
    public float timeVariance = 0.1f;
    #endregion
    #region Events
    //TODO esto NO se ejecutará en Awake
    //habrá un receptor que, al ingresar datos en el string por codigo
    //podrás hacer el seteo
    private void Awake(){

        if (useInitialText) text = txt_msg.text;

        fontSizeMax = txt_msg.fontSize;

        LoadText(text);
    }

    #endregion
    #region Methods

    public void FitText(){
        float newFontSize = ((Screen.height < Screen.width ? Screen.height : Screen.width) / 100) * (fontSizeMax / 10);
        newFontSize = newFontSize > fontSizeMax ? fontSizeMax : newFontSize;
        txt_msg.fontSize = (int)newFontSize;
    }

    public void LoadText(string s) => StartCoroutine(SetText(0,s));

    private IEnumerator SetText(int index = 0, string s = null){

        //asignamos el string en caso de haber entrante...
        if (s != null) s = text;

        yield return new WaitForSeconds(Random.Range(waitTime-timeVariance,waitTime+timeVariance));
        if (index != s.Length + 1)
        {
            txt_msg.text = new string(s.ToCharArray(0, index++));
            StartCoroutine(SetText(index, s));
        }
    }

    //agregar al text caracter por caracter, poner el string en un array
    //Se tiene que hacer animación de fade
    //Poder cambiar de diferentes "Mensajes" que son cadenas de string

    #endregion
}
