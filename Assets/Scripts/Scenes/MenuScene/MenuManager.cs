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
        TKey.MSG_CYBORG_WHERE
    };
    [Header("MenuManager")]
    public Button[] btns_Menu;
    public MsgController msg_Message;

    #endregion
    #region Events
    public override void Init(){
        SavedData saved = DataPass.GetSavedData();
        ButtonAdjust(!saved.isIntroCompleted);
        LoadMessage();
    }
    #endregion
    #region Methods

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
    /// Cargas el mensaje especificado
    /// </summary>
    private void LoadMessage(){
        msg_Message.LoadKey(keys_Msg[0],.075f);

    }


    //TODO, hay que hacer un efecto de mostrar
    //y desaparecer  el texto cambiandole el alpha



    /// <summary>
    /// Aquí cargaremos cada cierto tiempo un mensaje
    /// </summary>
    IEnumerator MessageLoader(){
        yield return new WaitForSeconds(0);
        //Cargas cada uno del arreglo
        LoadMessage();
    }
    #endregion
}

//Podríamos emitir un boolcuando terminamos de cargar el texto? en un updater?