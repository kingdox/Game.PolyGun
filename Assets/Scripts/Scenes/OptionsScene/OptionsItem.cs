#region IMports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion
public class OptionsItem : MonoBehaviour{
    #region var
    [Header("Settings")]
    public Button btn_Left;
    public Button btn_Center;
    public Button btn_Right;
    [Space]
    public MsgController msg;
    #endregion
    #region Events

    #endregion
    #region Methods


    /// <summary>
    /// Refrescamos el <see cref="MsgController"/> hijo
    /// </summary>
    public void RefreshText() => msg.LoadKey(msg.key);

    /// <summary>
    /// Revisa si entre sus botones alguno está teniendo foco 
    /// </summary>
    public bool IsFocus(){
        Button[] _bts = { btn_Left, btn_Center, btn_Right };
        foreach (Button b in _bts) if (b.IsActive()) return true;
        return false;
    }   
    #endregion
}
/*
 TODO

    Encargado de llamar al manager de Options y, sobre un arreglo de opciones
    decidir si ir para adelante o para atrás, al hacer eso
    el Manager actualizará los valores y el Datapass
 
 */