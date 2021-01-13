#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
public class IntroductionPage : MonoBehaviour
{
    //Maneja y refresca toda la información dentro de estas
    //TODO aquí nos encargamos de refrescar todas las animación y textos..
    #region Variables
    [Header("IntroductionPage Setup")]
    [Space]
    public MsgController[] messagesC;
    [Space]
    public ImageController[] ImagesC;
    #endregion
    #region Events


    #endregion
    #region Methods

    public void ReloadPage()
    {
        foreach (ImageController imgC in ImagesC)
        {
            imgC.Refresh();
        }
    }
    #endregion
}