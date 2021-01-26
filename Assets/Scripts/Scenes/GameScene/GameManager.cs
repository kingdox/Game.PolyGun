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
    [Header("@Areas")]
    public Transform @LeftoverContainer;
    public Transform @EnemiesContainer;
    //public Transform @WORLD;

    [Space]

    [Header("Debug")]
    public bool onDebug = false;
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
        __onDebug();
        Cursor.visible = !gameStatus.Equals(GameStatus.ON_GAME);
    }
    public override void Init()
    {
        //Tomamos el ultimo guardado
        lastSaved = DataPass.GetSavedData();
    }
    #endregion
    #region Methods
    /// <summary>
    /// Tomamos el elemento del LeftoverCOntainer fisico 
    /// </summary>
    public static Transform GetLeftoverContainer() => _.LeftoverContainer;
    /// <summary>
    /// Tomamos el elemento EnemiesContainer del mundo fisico
    /// </summary>
    public static Transform GetEnemiesContainer() => _.EnemiesContainer;

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
    public void GameOver()
    {
        //TODO hacemos los manejos finales Y luego es que se muestra el estado final

        //gameStatus = Status.ON_END;
        //1. Calculamos la diferencia de los datos guardados
        //y los progresados

        //2. de ese calculo sacamos por porcentaje los que mas actividad han tenido
        // (priorizando si se cumplio un batido de record (poniendo los mas importantes de primero...)


        //3. Guardaremos los datos nuevos a la datapass

        //4. Pintamos los items con los datos
        //AchieveSystem.Setitem()
    }


    /// <summary>
    /// Cambiamos el modo debug o no
    /// </summary>
    public void __onDebug(){
        if (!DebugFlag(ref onDebug, onDebug)) return;
        _onDebug = !_onDebug;
        PrintX($"DEBUGMODE => {_onDebug}");
    }

    #endregion
}

public enum GameStatus
{
    ON_GAME,
    ON_PAUSE,
    ON_END
}