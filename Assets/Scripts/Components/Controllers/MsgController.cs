#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Translate;
using Environment;
using XavLib;
using Options;
#endregion
public class MsgController : MonoInit
{
    #region var

    
    //Datos guardados para pintar
    private string savedText = null;

    //Se revisa el numero de carga actual
    private int loadNumber = 0;

    //Dato guardado de la etiqueta actual
    //private string actualTagValue = null;

    // Permite correr o no la ejecución de los textos
    //private bool keepLoading = true;

    [Header("Settings")]
    //a donde haremos el renderizado
    public Text txt_msg;
    //a donde buscaremos para traducir en caso de existir
    public TKey key = TKey.No;
    //si quiere cargarse
    public bool wantInit = true;


    #endregion
    #region Events
     new void Awake() {

        if (!txt_msg) txt_msg = GetComponent<Text>();

        //keepLoading = true;
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
            Refresh();
        }
    }
    #endregion
    #region Methods

    /// <summary>
    /// Nos informa si ha terminado de cargar el texto
    /// </summary>
    public bool IsFinished() => txt_msg.text == savedText;
    /// <summary>
    /// Cargamos el ultimo texto guardado
    /// </summary>
    public void Refresh() => LoadText(savedText);


    private IEnumerator SetText(
        float speed,
        int number,
        int index = 0,
        string s = null
    ){


        //esperamos el tiempo
        yield return new WaitForSeconds(speed);//speed

        if (loadNumber.Equals(number)) 
        {
            //Seguirá cargando siempre que el texto guardado sea el mismo que el que corre
            if (savedText == s)
            {
                //Si todiavía falta por recorrer
                if (index < s.Length){
                    //si la velocidad es 0 para cargar al instante
                    if (speed == 0){
                        txt_msg.text = s;
                    }
                    else
                    {
                        //si encuentra alguna etiqueta de inicio
                        if (s[index].Equals('<')){
                            //XavHelpTo.Look.Print($"Tag encontrado en index{index}");
                            TagDetector(speed,number,index,s);
                        }
                        //Si todavía puede seguir cargandolo
                        else
                        {
                            txt_msg.text = new string(s.ToCharArray(0, ++index));
                            StartCoroutine(SetText(speed,number, index, s));
                        }
                    }
                }
            }
        }
    }


    /// <summary>
    /// Nos permitirá cargar un texto distinto al <see cref="text"/>
    /// </summary>
    public void LoadText(string s="", float textSpeed = -1)
    {
        //asignamos el nuevo texto
        savedText = s;
        //Limpiamos el campo
        txt_msg.text = "";

        float speed = textSpeed != -1 ? textSpeed : OptionData.textSpeed[DataPass.GetSavedData().textSpeed];

        //Añadimos la carga
        loadNumber++;

        //Corremos el nuevo texto
        StartCoroutine(SetText(speed,loadNumber, 0, s));
    }
    /// <summary>
    /// Cargamos una llave y la guardamos, 
    /// <para>si es <see cref="TKey.No"/> limpia el texto</para>
    /// </summary>
    public void LoadKey(TKey k = TKey.No, float textSpeed = -1)
    {
        key = k;
        //si no se asigno llave 
        if (k == TKey.No) LoadText("", textSpeed);
        else LoadText(Data.Translated().Value(k), textSpeed);
    }
    /// <summary>
    /// Si encuentra el inicio de alguna etiqueta, la detecta completamente
    /// y pretende pintar
    /// el indexActual se encuentra en un '>'
    /// </summary>
    public void TagDetector(float speed,int number, int index_Start, string text) {

        //Obtenemos los punteros que nos muestran la etiqueta
        //y, indirectamente el texto contenido
        int[] tagIndex = XavHelpTo.Know.IndexsOfTag(text, index_Start);

        // 2. extraemos las partes: tag1 | valor | tag2
        string[] tagParts = new string[] {
            //start
            XavHelpTo.Set.Join(text, tagIndex[0], tagIndex[1]),
            //Val
            XavHelpTo.Set.Join(text, tagIndex[1] + 1, tagIndex[2] - 1),
            //end
            XavHelpTo.Set.Join(text, tagIndex[2], tagIndex[3])
        };

        // 3. extraemos 
        string[] textParts = new string[] {
            XavHelpTo.Set.Join(text, 0, tagIndex[0] - 1),
            XavHelpTo.Set.Join(text, tagIndex[3])
        };


        StartCoroutine(SetTag(
            speed,
            number,
            textParts,
            tagParts,
            text,
            tagIndex[3]
        ));
    }
    /// <summary>
    /// Carga las propiedades de la etiqueta, tras terminar
    /// redirige a setText para continuar...
    /// </summary>
    private IEnumerator SetTag(
        float speed, // valocidad del wait...
        int number,
        string[] textParts, // parte de inicio del tag, resto del tag
        string[] tagParts,
        string actualText,
        int endTagIndex,
        int index=0
    ){
        //speed
        yield return new WaitForSeconds(speed);

        if (loadNumber.Equals(number))
        {
            //protección con el guardado
            if (actualText.Equals(savedText))
            {

                //si es menor al limite
                if (index < tagParts[1].Length)
                {

                    //si la velocidad es 0 para cargar al instante
                    if (speed == 0) txt_msg.text = actualText;
                    else
                    {
                        //unimos poco a poco cada caracter del texto
                        txt_msg.text = textParts[0] + (tagParts[0] + XavHelpTo.Set.Join(tagParts[1], 0, index) + tagParts[2]);
                        StartCoroutine(SetTag(
                            speed,
                            number,
                            textParts,
                            tagParts,
                            actualText,
                            endTagIndex,
                            ++index
                        ));
                    }
                }
                //regresamos....
                else
                {
                    //TODO por aquí hay un error de longitudes, revisar
                    //Debug.Log($"{actualText.Length}, {textParts[0].Length}, {XavHelpTo.Set.Join(tagParts).Length}");
                    int newIndex = endTagIndex ;//textParts[0].Length + XavHelpTo.Set.Join(tagParts).Length;

                    Debug.Log($" textParts[0] {textParts[0]} | endTagIndex: {endTagIndex} | {actualText[endTagIndex]}");

                    StartCoroutine(SetText(speed, number, newIndex, actualText));
                }
            }
        }
    }
    /// <summary>
    /// TODO esto solo es para testeos
    /// </summary>
    public string _GetSavedData() => savedText;
    #endregion
}