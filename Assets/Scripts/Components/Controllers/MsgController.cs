#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Translate;
using Environment;
using XavLib;
#endregion
public class MsgController : MonoInit
{
    #region var

    
    //Datos guardados para pintar
    private string savedText = null;


    [Header("Settings")]
    //a donde haremos el renderizado
    public Text txt_msg;
    //a donde buscaremos para traducir en caso de existir
    public TKey key = TKey.No;
    //si quiere cargarse al iniciar
    public bool wantInit = true;

    #endregion
    #region Events
     new void Awake() {

        savedText = key.Equals(TKey.No)
            // en caso de no tener key guardamos el texto internamente
            ? txt_msg.text
            // en caso de haber key guardamos internamente la llave como string
            : key.ToString() // aquí cualquier texto valía
        ;
        //Limpiamos el texto
        txt_msg.text = "";
        //Iniciamos a revisar si DataPass.IsReady
        Begin();
    }
    //Si ya esta listo DataPass...
    public override void Init(){

        //si puede iniciar
        if (wantInit){   

            //Si posee llave buscamos la traducción de la llave
            if (!key.Equals(TKey.No) ) savedText = Data.Translated().Value(key);

            //Cargamos los datos del texto guardado
            LoadText(savedText);
        }
    }
    #endregion
    #region Methods
    /// <summary>
    /// Nos permitirá cargar un texto distinto al <see cref="text"/>
    /// </summary>
    public void LoadText(string s, float textSpeed = -1) {

        //asignamos el nuevo texto
        savedText = s;
        //Limpiamos el campo
        txt_msg.text = "";

        float speed = textSpeed != -1 ? textSpeed : Data.data.textSpeed[DataPass.GetSavedData().textSpeed];

        //Corremos el nuevo texto
        StartCoroutine(SetText(speed, 0, s));
    }

    /// <summary>
    /// Cargamos una llave y la guardamos, 
    /// <para>si es <see cref="TKey.No"/> limpia el texto</para>
    /// </summary>
    public void LoadKey(TKey k = TKey.No , float textSpeed = -1) {

        key = k;
        //si no se asigno llave 
        if (k == TKey.No)LoadText("");
        else LoadText(Data.Translated().Value(k), textSpeed);
    }

    private IEnumerator SetText(float speed, int index = 0, string s = null){


        //esperamos el tiempo
        yield return new WaitForSeconds(speed);


        //Seguirá cargando siempre que el texto guardado sea el mismo que el que corre
        if (savedText == s)
        {
            if (index != s.Length + 1){

                if (speed == 0){
                    txt_msg.text = s;
                }
                else
                {
                    txt_msg.text = new string(s.ToCharArray(0, index++));
                    StartCoroutine(SetText(speed,index, s));
                }
            }
        }
    }




    /// <summary>
    /// Transformas las propiedades del color del texto(RGBA)
    /// <para>
    /// donde el parametro a cambiar a medida que pasa el tiempo
    /// </para>
    /// TODO NO usado aun
    /// </summary>
    public void TextColor(int param, float reachValue, float timeScale){

        Color _col = txt_msg.color;
        _col[0] = 0;

        XavHelpTo.Set.UnitInTime(_col[param], reachValue, timeScale);

    }



    #endregion
}

/*
   public void FitText(){
        float newFontSize = ((Screen.height < Screen.width ? Screen.height : Screen.width) / 100) * (fontSizeMax / 10);
        newFontSize = newFontSize > fontSizeMax ? fontSizeMax : newFontSize;
        txt_msg.fontSize = (int)newFontSize;
    }
 */