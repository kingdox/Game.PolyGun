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
    [Header("MenuManager")]
    public Button[] btns_Menu;
    [Space]
    public MsgController msg_Message;

    /// <summary>
    /// Ordenamiento de los botones de menú
    /// </summary>
    private enum Menu{
        Play,
        Intro,
        Achieve,
        Opt,
        Exit
    }


    #endregion
    #region Events
    public override void Init(){
        SavedData saved = DataPass.GetSavedData();
        ButtonAdjust(!saved.isIntroCompleted);
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

    #endregion
}


