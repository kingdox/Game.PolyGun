#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Environment;
using XavLib;
#endregion
public class MenuInputController : MonoBehaviour
{
    #region Variables
    [Header("MenuInputController")]
    // => Los botones que vamos a evaluar 
    public Button[] buttons;
    public int lastIndex = 0;
    [Space]

    private readonly float keyTimeCD = 0.25f;
    private float keyTimeCount = 0;
    private readonly KeyPlayer[] movement ={KeyPlayer.UP,KeyPlayer.DOWN,KeyPlayer.LEFT,KeyPlayer.RIGHT};
    #endregion Events
    #region
    private void Update()
    {
        KeyDetection();
    }
    private void LateUpdate()
    {
        if (!ControlSystem.KnowKeyHold(movement).Equals(KeyPlayer.NO)) UpdateFocus();
    }
    #endregion
    #region Methods
    /// <summary>
    /// Revisamos si ha iniciado y si puede tocar los inputs
    /// <para>Maneja si la tecla fue presionada o si es tocada</para>
    /// </summary>
    private void KeyDetection()
    {
        if (!MonoInit.Inited) return;

        if (Input.anyKeyDown) ControlCheck();
        else if (Input.anyKey)
        {
            keyTimeCount = XavHelpTo.Set.TimeCountOf(keyTimeCount, keyTimeCD);
            if (keyTimeCount.Equals(0)) ControlCheck();
        }
        else keyTimeCount = 0;
    }

    /// <summary>
    /// Revisamos la tecla presionada
    /// y como comportarnos dependiendo de la tecla
    /// </summary>
    private void ControlCheck(){
        //Esto evita contactos por el click
        if (Input.GetKeyDown(KeyCode.Mouse0)) return;

        KeyPlayer keyPress = ControlSystem.KnowKeyHold(KeyPlayer.DOWN, KeyPlayer.UP);

        //si existe llave presionada
        if (ControlSystem.KeyExist(keyPress)){
            UpdateLastIndex(keyPress.Equals(KeyPlayer.DOWN));
        }

    }

    /// <summary>
    /// Actualizamos el ultimo indice en la dirección asignada
    /// </summary>
    private void UpdateLastIndex(bool c) => lastIndex = XavHelpTo.Know.NextIndex(c, buttons.Length, lastIndex);

    /// <summary>
    /// Actualizamos al ultimo indice
    /// </summary>
    private void UpdateFocus() => buttons[lastIndex].Select();

    #endregion
}