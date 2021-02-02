#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavLib;
using Environment;
#endregion
public class GameManager : MonoManager
{
    #region Variable
    private static GameManager _;
    //SOLO para lectura y refresco, no guardado
    private static SavedData lastSaved;

    [Header("Game Settigns")]
    public GameStatus gameStatus;
    [Space]

    //[Header("Debug")]
    //public bool onDebug = false;
    public static bool _onDebug = false;


    #endregion
    #region Events
    private new void Awake()
    {
        _ = this;


        gameStatus = GameStatus.ON_GAME;
        Time.timeScale = 1f;
        Cursor.visible = false;

        Begin();

    }
    private void Start()
    {
        //Mantiene unicamente HUD activa

    }
    private void Update()
    {
        Cursor.visible = !IsOnGame() || _onDebug;
    }
    public override void Init()
    {
        //Tomamos el ultimo guardado de inicio.
        lastSaved = DataPass.GetSavedData();
    }
    #endregion
    #region Methods

    /// <summary>
    /// Buscamos el estado actual del juego 
    /// </summary>
    public static GameStatus GetGameStatus() => _.gameStatus;
    /// <summary>
    /// Preguntamos si estamos en un estado de jugando o no
    /// </summary>
    public static bool IsOnGame() => _.gameStatus.Equals(GameStatus.ON_GAME) && !OptionSystem.isOpened;
    /// <summary>
    /// Asignamos un nuevo estado al juego
    /// </summary>
    public static void SetGameStatus(GameStatus status) => _.gameStatus = status;
    public static void SetGameStatus(int status) => _.gameStatus = (GameStatus)status;

    /// <summary>
    /// Acciones que se ejecutan cuando muere el jugador
    /// pasara x tiempo y luego mostrará la pantalla de final
    /// se deberá cargar los datos de los achievements
    /// </summary>
    public static void GameOver()
    {
        _.StartCoroutine(SetGameEnd());

        
        //1. Calculamos la diferencia de los datos guardados
        //y los progresados

        //2. de ese calculo sacamos por porcentaje los que mas actividad han tenido
        // (priorizando si se cumplio un batido de record (poniendo los mas importantes de primero...)


        //3. Guardaremos los datos nuevos a la datapass

        //4. Pintamos los items con los datos
        //AchieveSystem.Setitem()
    }

    /// <summary>
    /// Configurations to set the Game End Screen
    /// </summary>
    private static IEnumerator SetGameEnd()
    {
        PrintX($"GG !, setting GameEnd Screen with results...");

        yield return new WaitForSeconds(3);// es 5,
        _.gameStatus = GameStatus.ON_END;
        yield return new WaitForSeconds(.5f);// es 5,


        //nos devolvera organizadamente los indices de los achievements,
        //mostrando el que ruco mayores cambios en esta partida, a excepcion de los uq poseen -1
        int[] ach_pct = AchieveSystem.GetBestAchievements(lastSaved.achievements);
        //se envia los achievements ordenados por lo mejor, ya el tomará los primeros que quiera....
        ScreenManager.SetEndItems(ach_pct);




        SavedData saved = DataPass.GetSavedData();

        if (saved.record_waves < EnemyManager.waveActual)
        {
            //Actualizamos los records
            saved.record_waves = EnemyManager.waveActual;
            DataPass.SetData(saved);
        }



        //Terminamos guardando los cambios
        DataPass.SaveLoadFile(true);
    }





    /// <summary>
    /// Cambiamos el modo debug o no
    /// </summary>
    public void __onDebug(){
        //if (!DebugFlag(ref onDebug, onDebug)) return;
        _onDebug = !_onDebug;
        PrintX($"DEBUGMODE => {_onDebug}");
    }

    /// <summary>
    /// set the unit percentaje of timescale,
    /// </summary>
    /// <param name="pct"></param>
    public static void __Debug_SetTimeScale(float pct)
    {
        Time.timeScale = pct;

    }

    /// <summary>
    /// Clear the saved data
    /// </summary>
    public static void __ClearData()
    {
        SavedData saved = default;
        saved.achievements = new float[AchieveSystem.achievementLenght];
        saved.isIntroCompleted = true;
        DataPass.SetData(saved);
        DataPass.SaveLoadFile(true);
    }
    #endregion
}

public enum GameStatus
{
    ON_GAME,
    ON_PAUSE,
    ON_END
}