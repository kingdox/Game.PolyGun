#region IMports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
#endregion
public class OptionsItem : MonoBehaviour{

    //private static EventSystem eventSystem;

    #region var
    [Header("Settings")]
    public Button btn_Left;
    public Button btn_Center;
    public Button btn_Right;
    [Space]
    public MsgController msg;

    public bool amIFocused = false;
    #endregion
    #region Events
    private void Awake(){

        //esto inicializará el eventSystem, problemas si cambio de escena?
        //if (!eventSystem){
        //    eventSystem = EventSystem.current;
        //}
    }
    private void Start()
    {
        amIFocused = false;
    }

    private void Update(){

        
    }
    #endregion
    #region Methods

    /// <summary>
    /// Enfocas al boton correspondiente
    /// </summary>
    /// <param name="i"></param>
    public void FocusButton(int i){
        //Seleccionamos
        switch (i){
            case 0:
                btn_Left.Select();
                break;
            case 1:
                btn_Center.Select();
                break;
            case 2:
                btn_Right.Select();
                break;
        }
    }

    /// <summary>
    /// Refrescamos el <see cref="MsgController"/> hijo
    /// </summary>
    public void RefreshText() => msg.LoadKey(msg.key);

    /// <summary>
    /// Revisa si entre sus botones alguno está teniendo foco 
    /// </summary>
    public bool IsFocus(){
        return false;
        //foreach (Button btn in collection)
        //{

        //}



        ////Event.current
        //Button[] _bts = { btn_Left, btn_Center, btn_Right };
        //foreach (Button b in _bts) if (false) return true;
        ////TODO
        ////_bts[0]
        ////btn_Center.interactable
    }   
    #endregion
}
/*
 TODO

    Encargado de llamar al manager de Options y, sobre un arreglo de opciones
    decidir si ir para adelante o para atrás, al hacer eso
    el Manager actualizará los valores y el Datapass
 
 */