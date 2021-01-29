#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
[RequireComponent(typeof(Shot))]
/// <summary>
/// TriShot keep following the player and it try to keep focus to attack the nearest enemies
/// </summary>
public class TriShotController : Minion
{
    #region Variables
    [Header("TriShot Settings")]
    private Shot shot;

    [Space]
    private Transform _player;
    #endregion
    #region Events
    private void Start()
    {
        Get(out shot);

        LoadMinion();
        //al inicio se carga target con el transform de este obj
        _player = TargetManager.GetPlayer();
    }
    private void Update()
    {
        if (UpdateMinion())
        {





        }
    }
    #endregion
    #region Methods


    private void UpdateTarget()
    {

    }



    #endregion
}
//Tri - Shot: Es una figura “Aéreo-levitando” que disparará cerca de tí, este te seguirá y procurará estar cerca. Escoge un enemigo y envía a veces triángulos, siendo este su ataque de un rango medio.