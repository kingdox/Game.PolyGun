#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Translate;
using Environment;
using XavLib;
#endregion

/// <summary>
/// Clase utilizada para el manejo del sistema de opciones en la pantalla
/// de opciones, siendo siempre hija de alguna escena puesto que aparece
/// en más de un sitio, esta hecha como un prefab
/// </summary>
public class OptionSystem : MonoBehaviour
{
    #region var
    public static OptionSystem _ = null;


    //orden con las traducciones TODO al cargarlas se debe usar el metodo string para añadir el valor actual
    private readonly TKey[] messages = {
        TKey.MSG_OPT_LANGUAGE,
        TKey.MSG_OPT_TEXTSPEED,
        TKey.MSG_OPT_MUSIC,
        TKey.MSG_OPT_SOUND,
        TKey.MSG_OPT_CONTROLS,
        TKey.MSG_OPT_BACK,
    };

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
    private void Awake() {
        //Singleton 
        _ = this;

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
    public static void OpenClose(bool toOpen) {
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
                if (KeyMoveAvailable(key))
                {
                    lastOpt += key.Equals(KeyPlayer.DOWN) ? 1 : -1;
                    opt_items[(int)lastOpt].FocusCenterButton();
                    LoadMsg();
                }
                break;
            default:
                break;
        }

    }

    /// <summary>
    /// Revisa si es disponible mover el key
    /// </summary>
    private bool KeyMoveAvailable(KeyPlayer key) => !lastOpt.Equals(Options.LANGUAGE) && key.Equals(KeyPlayer.UP) || !lastOpt.Equals(Options.BACK) && key.Equals(KeyPlayer.DOWN);

    /// <summary>
    /// Dependiendo de la opción y la condicioón
    /// se ejecutará una acción o otra de la lista de opciones
    /// </summary>
    public static void Actions(Options option, bool condition, bool fromOpt = false) {

       // Debug.Log($"Option . =>  {option} : {condition}");
        if (option.Equals(Options.BACK))
        {
            if (fromOpt) {
                if (_.existChanges) {
                    _.existChanges = false;
                    //Guardamos los cambios
                   // Debug.Log("Guardano.....");
                    DataPass.SaveLoadFile(true);
                }

                OpenClose(false);
            }
        }
        else {
            //tomamos los guardados
            SavedData saved = DataPass.GetSavedData();
            _.existChanges = true;

            switch (option)
            {
                case Options.LANGUAGE:
                    saved.idiom = XavHelpTo.Know.NextIndex(condition, Data.GetLangLength(), saved.idiom);
                    break;
                case Options.TEXTSPEED:
                    saved.textSpeed = XavHelpTo.Know.NextIndex(condition, Data.data.textSpeed.Length, saved.textSpeed);

                    break;
                case Options.MUSIC:
                    saved.musicVolume = XavHelpTo.Know.NextIndex(condition, Data.data.musicVolume.Length, saved.musicVolume);

                    break;
                case Options.SOUND:
                    saved.sfxVolume = XavHelpTo.Know.NextIndex(condition, Data.data.sfxVolume.Length, saved.sfxVolume);

                    break;
                case Options.CONTROLS:
                    saved.control = XavHelpTo.Know.NextIndex(condition, Data.data.controls, saved.control);
                    break;
            }


            DataPass.SetData(saved);

            //si eres language cambiado refrescamos los textos en las pantallas
            if (OptionEqual(option, Options.LANGUAGE, Options.TEXTSPEED)) {
                //Seteamos los datos
                _.RefreshAll();
            }

        }
    }




    /// <summary>
    /// Actualizamos los textos de la pantalla al idioma correspondiente
    /// </summary>
    public void RefreshAll() {
        foreach (OptionsItem opt in opt_items) opt.RefreshText();

        LoadMsg();

        msg_title.LoadKey(msg_description.key);
    }


    /// <summary>
    /// Carga el mensaje correspondiente al boton y el valor actual
    /// </summary>
    private void LoadMsg(){
        string msg = Data.Translated().Value(messages[(int)lastOpt]) + "<color=green>HOLA</color>";
        msg_description.LoadText(msg);

    }

    /// <summary>
    /// De la lista proporcionada revisa si el seleccionado forma parte
    /// </summary>
    private static bool OptionEqual(Options opt, params Options[] opts){
        foreach (Options o in opts) if (opt.Equals(o)) return true;
        return false;
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