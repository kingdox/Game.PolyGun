﻿#region imports
using UnityEngine;
using XavHelpTo.Build;
using XavHelpTo.Change;
#endregion
/// <summary>
/// Encargarse de las actualización de cada pantallas y HUD correspondientemente
/// </summary>
public class ScreenManager : MonoX
{
    #region variables

    private static ScreenManager _;
    [Header("Screen Settings")]
    public GameStatus lastGameStatus = GameStatus.ON_GAME;

    [Header("Screens")]
    public GameObject HUDScreen;
    public GameObject pauseScreen;
    public GameObject endScreen;
    public GameObject debug_Screen;


    [Header("End Settings")]
    public AchievementItem[] endItems;

    [Space]
    [Header("Pause Settings")]
    public MsgController[] pause_msgController;
    public bool pauseNeedRefresh = false;

    [Header("Debug")]
    public bool _Debug_LoadEnd = false;
    #endregion
    #region events
    private void Awake()
    {
        if (_ == null)
        {
            _ = this;
        }
        else if (_ != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start(){
        ActiveScreenOf();
        lastGameStatus = GameStatus.ON_GAME;
    }
    private void Update(){

        //if is on debug mode and is on game then activates the debug screen
        debug_Screen.SetActive(GameManager.IsOnGame() && GameManager._onDebug);

        StatusChange();
        _Debug();

        if (!OptionSystem.isOpened && lastGameStatus.Equals(GameStatus.ON_PAUSE) && pauseNeedRefresh)
        {
            pauseNeedRefresh = false;
            RefreshPause();
        }

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

            //TODO si es pause actualizarlo

            //Actualizamos el estado, dependiendo del cambio
            //Time.timeScale = GameManager.IsOnGame() ? 1 : 0;



        }
    }
    /// <summary>
    /// Abre la pantalla de opxiones
    /// </summary>
    public void OptionOpen()
    {
        pauseNeedRefresh = true;
        OptionSystem.OpenClose(true);
    }

    /// <summary>
    /// Refresh the pause screen
    /// </summary>
    public static void RefreshPause()
    {
        foreach (MsgController msg in _.pause_msgController)
        {   
            msg.LoadKey(msg.key);
            
        }
    } 

    /// <summary>
    /// Activamos la pantalla correspondiente al estado que se encuentran
    /// </summary>
    private void ActiveScreenOf(int v) => Change.ActiveObjectsExcept(v, HUDScreen, pauseScreen, endScreen);
    private void ActiveScreenOf(GameStatus v = GameStatus.ON_GAME) => ActiveScreenOf((int)v);



    /// <summary>
    /// Implementation of the resume of the achievements
    /// </summary>
    public static void SetEndItems(int[] bestAchieves)
    {
        //recorremos los endItems y añadimos del indice el record correspondiente
        for (int x = 0; x < _.endItems.Length; x++)
        {
            AchieveSystem.Setitem(bestAchieves[x], _.endItems[x]);
        }


    }

    /// <summary>
    /// Revisa los debugs...?
    /// </summary>
    private void _Debug()
    {
        if (!DebugFlag(ref _Debug_LoadEnd)) return;

        foreach (AchievementItem i in endItems)
        {
            AchieveSystem.Setitem(10.ZeroMax(), i);
        }
    }
    #endregion
}