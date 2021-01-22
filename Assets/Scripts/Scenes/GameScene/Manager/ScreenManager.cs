#region imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavLib;
#endregion
public class ScreenManager : MonoBehaviour
{
    #region variables

    [Header("Screen Settings")]
    private GameStatus lastGameStatus = GameStatus.ON_GAME;

    [Header("Screens")]
    public GameObject HUDScreen;
    public GameObject pauseScreen;
    public GameObject endScreen;


    [Header("End Settings")]
    public AchievementItem[] endItems;


    [Header("Debug")]
    public bool _Debug_LoadEnd = false;
    #endregion
    #region events
    private void Start(){
        ActiveScreenOf();
        lastGameStatus = GameStatus.ON_GAME;
    }
    private void Update()
    {
        StatusChange();

#if DEBUG
        _Debug();
#endif
    }
    #endregion
    #region methods

    /// <summary>
    /// Revisa si ha cambiado el estado de <see cref="GameStatus"/> de <see cref="GameManager"/>,
    /// de ser así ejecuta lo correspondiente
    /// </summary>
    private void StatusChange(){
        //si el estado NO es igual entonces cambia
        if (!lastGameStatus.Equals(GameManager.GetGameStatus())){
            //actualizamos la variable
            lastGameStatus = GameManager.GetGameStatus();

            //Activamos la pantalla
            ActiveScreenOf(lastGameStatus);

            //Actualizamos el estado, dependiendo del cambio
            //Time.timeScale = GameManager.IsOnGame() ? 1 : 0;



        }
    }
    /// <summary>
    /// Abre la pantalla de opxiones
    /// </summary>
    public void OptionOpen()
    {
        OptionSystem.OpenClose(true);
    }


    /// <summary>
    /// Activamos la pantalla correspondiente al estado que se encuentran
    /// </summary>
    private void ActiveScreenOf(int v) => XavHelpTo.Change.ActiveObjectsExcept(v, HUDScreen, pauseScreen, endScreen);
    private void ActiveScreenOf(GameStatus v = GameStatus.ON_GAME) => ActiveScreenOf((int)v);



#if DEBUG
    private void _Debug()
    {
        //detector de si esta debugeando
        if (!GameManager._onDebug) return;  

        //Cargar de LoadEnd los logros, random
        if (_Debug_LoadEnd)
        {
            _Debug_LoadEnd = false;
            foreach (AchievementItem i in endItems)
            {
                AchieveSystem.Setitem(XavHelpTo.Get.ZeroMax(10), i);
            }

        }
    }
#endif
    #endregion
}