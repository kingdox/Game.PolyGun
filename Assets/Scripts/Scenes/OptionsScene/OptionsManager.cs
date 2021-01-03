#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Translate;
#endregion
/// TODO ver como implementarlo, como prefab?
public class OptionsManager : MonoManager
{
    #region var
    //orden con las traducciones
    private readonly TKey[] messages = {
        TKey.MSG_OPT_LANGUAGE,
        TKey.MSG_OPT_TEXTSPEED,
        TKey.MSG_OPT_MUSIC,
        TKey.MSG_OPT_SOUND,
        TKey.MSG_OPT_CONTROLS,
        TKey.MSG_OPT_BACK,
    };
    private readonly Idiom[]  idioms = { Idiom.es, Idiom.en };
    private readonly float[] musicVolume = { 0, 30, 50, 70 };
    private readonly float[] sfxVolume = { 0, 70 };
    private readonly string[] controls = { "clasic", "alternative" };//ps4?

    private int msgIndex = 0;


    [Header("Options Manager")]

    public MsgController msg_title;
    public MsgController msg_description;
    public OptionsItem[] opt_items;
    public MsgController msg_back;


    #endregion
    #region Events
    public override void Init(){}
    #endregion
    #region Methods

    private void CheckControls()
    {
        
        //button.Select()
    }

    //Cargamos el mensaje basado en el sitio donde se encuentra
    //el focus de los botones, si pertenece a alguno muestra, sino no hace nada
    private void LoadMsg(){

        for (int x = 0; x < opt_items.Length; x++)
        {
            if (opt_items[x].IsFocus()){
                msgIndex = x;
            }
        }

    }

    /// <summary>
    /// Actualizamos los textos de la pantalla al idioma correspondiente
    /// </summary>
    private void RefreshAll(){
        foreach (OptionsItem opt in opt_items) opt.RefreshText();
        msg_description.LoadKey(msg_description.key);
        msg_title.LoadKey(msg_description.key);
        msg_back.LoadKey(msg_back.key);
    }
    #endregion
}