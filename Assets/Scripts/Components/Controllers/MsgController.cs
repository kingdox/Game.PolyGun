#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Translate;
using Environment;
#endregion
public class MsgController : MonoInit
{
    #region var


    [Header("Settings")]
    public Text txt_msg;
    public TKey key = TKey.No;
    [Space]
    private string text = null;

    [Space]
    [Header("Time")]
    public float delayTime = 0;
    public float waitTime = 0.20f;
    //public float timeVariance = 0.1f;
    #endregion
    #region Events
    //Guardamos los textos y lueego los limpiamos
    //TODO estudiar esto del hecho de tener que usar New
     new void Awake() {
        if (key.Equals(TKey.No)) text = txt_msg.text;
        txt_msg.text = "";
        Begin();
    } 
    public override void Init(){
        if (!key.Equals(TKey.No)) text = Data.Translated().Value(key);
        LoadText(text);
    }
    #endregion
    #region Methods
    /// <summary>
    /// Nos permitirá cargar un texto distinto al <see cref="text"/>
    /// </summary>
    /// <param name="s"></param>
    public void LoadText(string s) => StartCoroutine(SetText(0,s));


    private IEnumerator SetText(int index = 0, string s = null){

        bool condition = s != null;
        //asignamos el string en caso de haber entrante...
        yield return new WaitForSeconds(condition ? waitTime : 0);//Random.Range(waitTime-timeVariance,waitTime+timeVariance)
        if (condition) s = text;
        if (index != s.Length + 1){
            txt_msg.text = new string(s.ToCharArray(0, index++));
            StartCoroutine(SetText(index, s));
        }
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