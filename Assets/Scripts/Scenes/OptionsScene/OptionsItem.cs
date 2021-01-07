#region IMports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Options;
#endregion
public class OptionsItem : MonoBehaviour{

    #region var
    public MsgController msg;
    public Option opt;
    public Button btn;
    #endregion
    #region Events
    #endregion
    #region Methods
    /// <summary>
    /// Enviamos a <see cref="OptionSystem"/>
    /// el comportamiento del botón
    /// </summary>
    public void SendButtonAction(bool condition) => OptionSystem.Actions(opt,condition, true);
    /// <summary>
    /// Refrescamos el <see cref="MsgController"/> hijo
    /// </summary>
    public void RefreshText() => msg.LoadKey(msg.key);

    /// <summary>
    /// Hacemos foco del botón principal
    /// </summary>
    public void FocusCenterButton() => btn.Select();
    #endregion
}