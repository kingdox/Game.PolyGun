#region
using UnityEngine;
using UnityEngine.UI;
using Environment;
using XavHelpTo.Know;
#endregion
public class MenuInputController : MonoX
{
    #region Variables
    [Header("MenuInputController")]
    // => Los botones que vamos a evaluar 
    public Button[] buttons;
    [Space]
    public int lastIndex = 0;

    //private readonly float keyTimeCD = 0.25f;private float keyTimeCount = 0;

    #endregion Events
    #region
    private void OnEnable()
    {
        lastIndex = 0;
        //UpdateFocus();
        //buttons[0].Select();
    }
    private void Update(){
        KeyDetection();
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
        if (Input.GetKey(KeyCode.Mouse0)) return;

        //si toca una tecla en el FRAME
        VerticalCheck(ControlSystem.KnowKeyFrame(ControlSystem.keysForward));

        //si toca una tecla en el HOLD
        if (ControlSystem.IsKeyHold(ControlSystem.keysMovement)) UpdateFocus();
    }

    /// <summary>
    /// Revisamos la tecla presionada
    /// y como comportarnos dependiendo de la tecla
    /// </summary>
    private void VerticalCheck(KeyPlayer keyPress){
        //Si existe alguna llave
        if (ControlSystem.IsKeyExist(keyPress)){
            UpdateLastIndex(keyPress.Equals(KeyPlayer.DOWN));
        }

    }

    /// <summary>
    /// Actualizamos el ultimo indice en la dirección asignada
    /// </summary>
    private void UpdateLastIndex(bool c) => lastIndex = Know.NextIndex(c, buttons.Length, lastIndex);

    /// <summary>
    /// Actualizamos al ultimo indice
    /// </summary>
    private void UpdateFocus() => buttons[lastIndex].Select();

    #endregion
}