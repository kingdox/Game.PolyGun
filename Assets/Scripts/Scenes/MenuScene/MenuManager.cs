#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavLib;
using Environment;
using Translate;
#endregion
public class MenuManager : MonoManager
{
    #region Variables
    private enum Menu{
        Play,
        Intro,
        Achieve,
        Opt,
        Exit
    }

    private readonly TKey[] keys_Msg ={
        TKey.MSG_CYBORG_WHERE,
        TKey.MSG_CYBORG_UNKNOW
    };
    [Header("MenuManager")]
    public Button[] btns_Menu;
    public MsgController msg_Message;
    //[Space]
    public bool waitToLoad = false;

    #endregion
    #region Events
    private void Update(){

        if (!waitToLoad && MsgMessageReady())
        {
            Debug.Log("Cargando mensaje");
            StartCoroutine(MessageWait());
        }
    }
    public override void Init(){
        SavedData saved = DataPass.GetSavedData();
        ButtonAdjust(!saved.isIntroCompleted);
        Debug.Log("Inited");
    }
    #endregion
    #region Methods

    /// <summary>
    ///  Revisamos si en las opciones estan cerradas y si el mensaje ya ha terminado
    ///  TODO revisar si esto lo reutilizamos para los demas msg
    /// </summary>
    private bool MsgMessageReady() => !OptionSystem.isOpened && msg_Message.IsFinished();

    /// <summary>
    /// Limpia el mensaje actual y abre las opciones
    /// </summary>
    public void OpenOptions(){
        msg_Message.LoadKey(TKey.No, 0);
        OptionSystem.OpenClose(true);
    }

    /// <summary>
    /// Ajustará qué botones podrán ser interactuables y cuales no,
    /// dependiendo del estado de si el tutorial fue terminado o no.
    /// </summary>
    private void ButtonAdjust(bool adjust = false){
        Menu[] buttonsMenu = { Menu.Play, Menu.Achieve, Menu.Opt};

        for (int x = 0; x < btns_Menu.Length; x++){

            btns_Menu[x].interactable = true;

            if (adjust){
                //si encuentra que forma parte de los adjust
                foreach (Menu btn in buttonsMenu)
                {
                    if ((Menu)x == btn) btns_Menu[x].interactable = false;
                }
            }
        }
    }


    /// <summary>
    /// Aquí cargaremos cada cierto tiempo un mensaje
    /// </summary>
    IEnumerator MessageWait(float waitTime = 4){
        waitToLoad = true;

        yield return new WaitForSeconds(waitTime);

        //si estas en menu, no se ha cargado mensaje y el que estaba ya ha terminado
        if (MsgMessageReady()) LoadMessage();

        waitToLoad = false;

    }

    /// <summary>
    ///Carga algún mensaje del indice
    ///<para>si index es -1 entonces tomará aleatoriamente alguno</para>
    /// </summary>
    private void LoadMessage(int index=-1){
        index = index != -1 ? index : XavHelpTo.Know.DifferentIndex(keys_Msg.Length, index);
        msg_Message.LoadKey(keys_Msg[index], .075f);
    }


    


    /// <summary>
    /// Te saca de la aplicación
    /// </summary>
    public void ExitApp() => Application.Quit();
    #endregion
}

//Podríamos emitir un boolcuando terminamos de cargar el texto? en un updater?