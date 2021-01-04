#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Translate;
#endregion
/// TODO ver como implementarlo, como prefab?
public class OptionSystem : MonoBehaviour
{
    #region var
    public static OptionSystem _ = null;
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


    [Header("Options Settings")]
    public GameObject obj_screen_last;
    public GameObject obj_screen_option;
    [Space]
    public MsgController msg_title;
    public MsgController msg_description;
    public OptionsItem[] opt_items;

    public MsgController msg_back;

    //Con esto podemos ver si ha sido abierto o no ?
    public static bool isOpened = false;

    #endregion
    #region Events
    private void Awake(){
        _ = gameObject.GetComponent<OptionSystem>();
    }
    #endregion
    #region Methods

    /// <summary>
    /// Revisamos si va a ser abierto la ventana o no,
    /// 
    /// </summary>
    /// <param name="toOpen"></param>
    public static void OpenClose(bool toOpen){
        _.obj_screen_option.SetActive(toOpen);
        _.obj_screen_last.SetActive(!toOpen);
        if (toOpen){
            _.RefreshAll();
        }
     
        isOpened = toOpen;
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
    public void RefreshAll(){
        foreach (OptionsItem opt in opt_items) opt.RefreshText();
        msg_description.LoadKey(msg_description.key);
        msg_title.LoadKey(msg_description.key);
        msg_back.LoadKey(msg_back.key);
    }
    #endregion
}
//TODO mejorar este script