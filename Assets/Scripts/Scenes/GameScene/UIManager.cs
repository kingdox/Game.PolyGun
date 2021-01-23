#region Imports
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion
/// <summary>
/// Encargado del manejo de pintado con respecto al HUD de la GameScene
/// </summary>
public class UIManager : MonoBehaviour
{
    #region Variables
    private static UIManager _;
    //Revisar cuando puede disparar:
    public Text text_weaponCD;

    #endregion
    #region Events
    private void Awake(){
        _ = this;
    }
    private void Update()
    {
        
    }
    #endregion
    #region MEthods






    /// <summary>
    /// Actualiza toda la parte de Armas del HUD, permite hasta 2 redpmdeps
    /// </summary>
    public static void Refresh_weaponCD(float time){
        _.text_weaponCD.text = Math.Round(time,2).ToString();
    }
    #endregion
}