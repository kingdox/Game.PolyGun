#region 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;
using XavLib;
#endregion

public class InitialManager : MonoManager
{
    #region Variables

    #endregion
    #region  Events
    public override void Init()
    {

    }
    private void Update()
    {
        if (Input.anyKey && Inited)
        {
            CheckInit();
        }
    }
    #endregion
    #region Methods


    /// <summary>
    /// Revisamos si ha hecho tutorial o no, dependiendo de la respuesta
    /// se lleva la jugador al menu o a introducción
    /// </summary>
    private void CheckInit(){
        SavedData saved = DataPass.GetSavedData();

        if (saved.isIntroCompleted)
        {
            XavHelpTo.Look.Print("Cargando menu");
            GoToScene(Scenes.MenuScene);
        }
        else
        {
            XavHelpTo.Look.Print("Cargando Introducción");
            GoToScene(Scenes.MenuScene);
        }
    }

    #endregion
}
