#region Imports
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavLib;
#endregion

public class EnemyManager : MonoX
{

    #region Variables
    private static EnemyManager _;
    [Header("Enemy Manager Settings")]
    public GameObject[] prefs_Enemies;
    public Spawner spawner;

    [Space]

    private readonly SpawnOpt[] spawnPatron = { SpawnOpt.NEAR, SpawnOpt.NEAR, SpawnOpt.RANDOM, SpawnOpt.FAR, SpawnOpt.RANDOM };
    private int spawnOrder = 0;
    [Space]
    public float timer = 5f; private float count;

    [Space]
    [Header("Wave settings")]
    //Cantidad de enemigos que faltan por invocar
    public int enemiesLeft = 0;
    // Indicador de la oleada actual
    public static int waveActual = 0;
    [Space]
    public int bossWaves = 5;

    [Header("Debug")]
    public bool _Debug_SetWave = false;

    #endregion
    #region Events
    private void Awake(){
        _ = this;

        waveActual = 0;
        enemiesLeft = 0;

    }
    private void Start(){
        SetNewWave();
    }
    private void Update()
    {

        
        if (IsWaveEnd()){

            SetNewWave();

        }else{

            //Cuando pasaba el tiempo invocamos un enemigo
            if (!enemiesLeft.Equals(0) && Timer(ref count, timer)){
                SpawnEnemy();
            }

        }


        __Debug_SetWave();
    }
    #endregion
    #region Methods


    /// <summary>
    /// Se encarga de ajustar los datos para que se gestione la siguiente oleada
    /// enemiga
    /// </summary>
    private void SetNewWave(){
        waveActual++;
        PrintX($"////WAVE {waveActual}/////");

        //Conocemos el record
        int waveRecord = DataPass.GetSavedData().record_waves;
        // creamos un valor aleatorio a partir del record actual
        int recordRandom = XavHelpTo.Get.ZeroMax(waveRecord) + 1;//+1 añadido, HARDCODED
        // Tenemos los enemigos por oleada
        float enemyPerWave = 1.5f;// HARDCODED

        float result;

        //Si el valor random del record es mayor, es normal...
        if (recordRandom > waveActual){
            //Calcula sin redondear
            result = waveActual * enemyPerWave;
        }else{
            //Añade la diferencia entre la oleada actual y la aleatoria de la enemiga
            result = (recordRandom - waveActual) + waveActual * enemyPerWave;
        }

        //cuenta la cantidad de enemigos faltantes
        enemiesLeft = (int)Math.Round(result, 0);
        
    }

    /// <summary>
    /// Crea un Enemigo y lo posiciona en alguno de los sitios correspondientes
    /// llamando al <see cref="Spawner"/>
    /// </summary>
    private void SpawnEnemy(){
        enemiesLeft--;

        int selected = XavHelpTo.Get.ZeroMax(prefs_Enemies.Length);
        spawnOrder = XavHelpTo.Know.NextIndex(true, spawnPatron.Length, spawnOrder);

        spawner.Generate(prefs_Enemies[selected], spawnPatron[spawnOrder], TargetManager.GetEnemiesContainer());

       
    }
    /// <summary>
    /// Comprueba si NO hay enemigos faltantes por invocar ni enemigos en la escena
    /// </summary>
    private bool IsWaveEnd() => enemiesLeft.Equals(0) && TargetManager.GetEnemiesContainer().childCount.Equals(0);


    /// <summary>
    /// Permite añadir la siguiente oleada
    /// </summary>
    public void __Debug_SetWave(){
        if (!DebugFlag(ref _Debug_SetWave)) return;
        SetNewWave();

    }

    #endregion
}


/// <summary>
/// Conocemos las diferencias entre los enemigos por el tipo
/// </summary>
public struct EnemyType
{

    public EnemyName name;
    public bool isBoss;
}
/// <summary>
/// Los enemigos que hay en el juego
/// </summary>
public enum EnemyName
{
    MOND,
    PLUR
}


////TODO
////TODO revisar si es la oleada numero 5

//// Cada 5 oleadas se añade un Enemigo Jefe, TODO ver como implementar leugo
//if (waveActual % bossWaves == 0)
//{
//    PrintX("////BOSS/////");
//}