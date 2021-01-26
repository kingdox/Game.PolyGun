#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavLib;
#endregion
public class AllyManager : MonoX
{
    #region Variable
    private static AllyManager _;
    //container de ally
    public Transform @allyContainer;
    public GameObject[] prefs_Allies;

    [Header("Debug")]
    public bool _Debug_Invoke = false;
    public Transform _Debug_player;

    #endregion
    #region Events
    private void Awake()
    {
        Get(out _);
    }
    private void Update()
    {
        __Debug_Invoke();
    }
    #endregion
    #region Methods

    //Crear caja en la pos otorgada, tambien puede tener un impulso
    //setea en su sitio


    public static void GenerateAlly(Transform target, AllyType type)
    {


        int range = XavHelpTo.Get.RandomBool() ? 1 : -1;//distancia del creador y la cración

        Vector3 distance = Vector3.one;

        distance.x = range;
        distance.y = 0;
        distance.z = range;

        int allyIndex = type.Equals(AllyType.NO)
            ? XavHelpTo.Get.ZeroMax(_.prefs_Allies.Length)
            : (int)type
        ;

        Instantiate(
            _.prefs_Allies[allyIndex],
            target.position + distance ,
            Quaternion.LookRotation(target.forward), _.@allyContainer
        );


    }


    /// <summary>
    /// Invocamos aleatoriamente un Aliado al lado del player
    /// </summary>
    private void __Debug_Invoke(){
        if (!DebugFlag(ref _Debug_Invoke) )return;

        PrintX("Creando player");
        GenerateAlly(_Debug_player, AllyType.NO);



    }

    #endregion
}

/// <summary>
/// Aliados posibles en el juego
/// </summary>
public enum AllyType
{
    NO = -1,

    BOXBOX,
    TRI_SHOT,
    HEARTH,
    ROMB,
    POL
}
