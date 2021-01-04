#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Translate;
using Environment;
using XavLib;
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

    private bool existChanges = false;


    //podemos ver si ha sido abierto o no
    public static bool isOpened = false;

    [Header("Options Settings")]
    public GameObject obj_screen_last;
    public GameObject obj_screen_option;
    [Space]
    public MsgController msg_title;
    public MsgController msg_description;
    [Space]
    public OptionsItem[] opt_items;
    [Space]

    [Header("IndexActual")]
    private static Options lastOpt = Options.LANGUAGE;


    #endregion
    #region Events
    private void Awake(){

            _ = this;
        //Singleton corroboration
        //if (_ == null){
        //}else if (_ != this) Destroy(gameObject);

        existChanges = false;
        lastOpt = Options.LANGUAGE;
    }
    private void Update()
    {
        if (Input.anyKeyDown && isOpened) ControlCheck();
    }
    #endregion
    #region Methods

    /// <summary>
    /// Revisamos si va a ser abierto la ventana o no,
    /// </summary>
    /// <param name="toOpen"></param>
    public static void OpenClose(bool toOpen){
        _.obj_screen_option.SetActive(toOpen);
        _.obj_screen_last.SetActive(!toOpen);
        lastOpt = Options.LANGUAGE;
        if (toOpen) _.RefreshAll();
        isOpened = toOpen;
    }

    /// <summary>
    /// Checkeamos el teclado y, dependiendo de la posición que se encuentre
    /// el jugador podrá hacer una accion o otra
    /// </summary>
    public void ControlCheck()
    {
      
        KeyPlayer key = ControlSystem.KnowKey(KeyPlayer.DOWN, KeyPlayer.UP, KeyPlayer.LEFT, KeyPlayer.RIGHT);

        //botones a detectar
        switch (key)
        {
            case KeyPlayer.LEFT:
            case KeyPlayer.RIGHT:

                Actions(lastOpt, key.Equals(KeyPlayer.RIGHT));

                break;
            case KeyPlayer.UP:
            case KeyPlayer.DOWN:

                //si estas en los limites
                if(
                    !lastOpt.Equals(Options.LANGUAGE) && key.Equals(KeyPlayer.UP)
                    || !lastOpt.Equals(Options.BACK) && key.Equals(KeyPlayer.DOWN)
                ){
                    lastOpt += key.Equals(KeyPlayer.DOWN) ? 1 : -1;
                    opt_items[(int)lastOpt].FocusCenterButton();
                }
                break;
            default:
                break;
        }

    }

    /// <summary>
    /// Dependiendo de la opción y la condicioón
    /// se ejecutará una acción o otra de la lista de opciones
    /// TODO
    /// </summary>
    public static void Actions(Options option,bool condition, bool fromOpt = false){

        Debug.Log($"Option . =>  {option} : {condition}");
        if (option.Equals(Options.BACK))
        {
            if (fromOpt) {
                if (_.existChanges){
                    _.existChanges = false;
                    //Guardamos los cambios
                    Debug.Log("Guardano.....");
                    DataPass.SaveLoadFile(true);
                }

                OpenClose(false);


            }
            
        }
        else{
            //tomamos los guardados
            SavedData saved = DataPass.GetSavedData();
            _.existChanges = true;

            switch (option)
            {
                case Options.LANGUAGE:

                    saved.idiom = XavHelpTo.Know.NextIndex(condition, Data.GetLangLength(), saved.idiom);
                    //Todo
                    break;
                case Options.TEXTSPEED:

                    break;
                case Options.MUSIC:

                    break;
                case Options.SOUND:

                    break;
                case Options.CONTROLS:

                    break;
            }
            //Seteamos los datos
            DataPass.SetData(saved);
            _.RefreshAll();

        }
    }


    

    /// <summary>
    /// Actualizamos los textos de la pantalla al idioma correspondiente
    /// </summary>
    public void RefreshAll(){
        foreach (OptionsItem opt in opt_items) opt.RefreshText();
        msg_description.LoadKey(msg_description.key);
        msg_title.LoadKey(msg_description.key);
    }



  
    #endregion
}
//Opciones que hay en el menú de opciones
public enum Options{
    LANGUAGE,
    TEXTSPEED,
    MUSIC,
    SOUND,
    CONTROLS,
    BACK
}
//TODO Options aparecerá como un global