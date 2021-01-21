#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavLib;
using Environment;
#endregion
/// <summary>
/// TODO arreglar la camera en el Field Of View
/// </summary>
public class GameManager : MonoManager
{
    #region Variable

    [Header("Game Settigns")]
    //poner privado luego..
    public static Status gameStatus;
    private static GameManager _;



    public enum Status{
        ON_GAME,
        ON_PAUSE,
        ON_OPTIONS,
        //ON_STOP ? tipo para ahcer efecto de parado antes del end
        ON_END
    }

    [Header("Screens")]
    public GameObject pauseScreen;
    public GameObject HUDScreen;


    [Header("End Settings")]
    public AchievementItem[] endItems;


    [Header("Debug")]
    public static bool _onDebug = false;
    public bool _Debug_LoadEnd = false;

    #endregion
    #region Events
    private void Awake()
    {
        _ = this;
    }
    private void Update()
    {
#if DEBUG
        _Debug();
#endif
    }
    public override void Init()
    {

    }
    #endregion
    #region Methods

    /// <summary>
    /// Activa la pantalla de pausa
    /// </summary>
    public static void PauseGame(){

        _.pauseScreen.SetActive(true);
    }

    /// <summary>
    /// TODO
    /// Regresa de pausa a el juego 
    /// </summary>
    public void ResumeGame(){
        _.pauseScreen.SetActive(false);
    }

    /// <summary>
    /// Abre la pantalla de opxiones
    /// </summary>
    public void OptionOpen()
    {
        //msg_Message.LoadKey(TKey.No, 0);
        OptionSystem.OpenClose(true);
        //wantRefresh = true;
    }



    /// <summary>
    /// Acciones que se ejecutan cuando muere el jugador
    /// pasara x tiempo y luego mostrará la pantalla de final
    /// se deberá cargar los datos de los achievements
    /// </summary>
    public void GameOver()
    {
        //1. Calculamos la diferencia de los datos guardados
        //y los progresados

        //2. de ese calculo sacamos por porcentaje los que mas actividad han tenido
        // (priorizando si se cumplio un batido de record (poniendo los mas importantes de primero...)


        //3. Guardaremos los datos nuevos a la datapass

        //4. Pintamos los items con los datos
        //AchieveSystem.Setitem()
    }

#if DEBUG
    private void _Debug(){
        
        //Cargar de LoadEnd los logros, random
        if (_Debug_LoadEnd){
            _Debug_LoadEnd = false;
            foreach (AchievementItem i in endItems){
                AchieveSystem.Setitem(XavHelpTo.Get.ZeroMax(10), i);
            }

        }
    }
#endif
    #endregion
}
