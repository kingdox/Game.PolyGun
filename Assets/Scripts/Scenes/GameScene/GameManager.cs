#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavLib;
using Environment;
#endregion
public class GameManager : MonoManager
{
    #region Variable

    //poner privado luego..
    public Screen actualScreen;
    #endregion
    #region Events
    private void Update()
    {
    }
    public override void Init()
    {

    }
    #endregion
    #region Methods

    /// <summary>
    /// TODO
    /// Regresa de pausa a el juego 
    /// </summary>
    public void ResumeGame()
    {

    }

    /// <summary>
    /// Abre la pantalla de opxiones
    /// </summary>
    public void OptionOpen()
    {
        //msg_Message.LoadKey(TKey.No, 0);
        OptionSystem.OpenClose(true);
        //wantRefresh = true;
    }
    #endregion
}
