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
    [Space]
    private float refreshTargetCount;
    [Space]
    public float maxRangeSize = 2;
    public float minRangeSize = 4;

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

            //if exist a target then...
            if (target != null)
            {

                // Target refresher
                if (Timer(ref refreshTargetCount, 1))
                {
                    UpdateTarget();
                }

                //Rotation Refresh
                if (target != transform)
                {
                    // rotates to that target
                    rotation.LookTo(target.position);
                }


                //Player following
                UpdatePlayerFollow();

            }
            else
            {
                UpdateTarget();
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (_player != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_player.position, character.range);

            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(_player.position, character.range / maxRangeSize);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_player.position, character.range / minRangeSize);
        }
    }
    #endregion
    #region Methods


    private void UpdateTarget()
    {

    }

    private void UpdatePlayerFollow()
    {

        float distance = Vector3.Distance(transform.position, _player.position);
        int orientation = 1;
        bool canMove = false;
        bool isGoingToSafeArea = false;
        bool inRange = distance > character.range / maxRangeSize && (distance < character.range / minRangeSize);
        /*
         * TRI SHOT
         * 
         *  estara cerca del player hasta cierto punto,
         * 
         */


        //if (distance > character.range / maxRangeSize)
        //{
        //    canMove = true;
        //}
        //else
        //{
        //    if (distance < character.range / minRangeSize)
        //    {
        //        canMove = true;
        //        isGoingToSafeArea = true;
        //    }
        //    else
        //    {
        //        canMove = true;
        //    }

        //}

        //if (canMove)
        //{

        //}

        //movement.Move(orientation * transform.forward.normalized, character.speed);
    }

    #endregion
}
//Tri - Shot: Es una figura “Aéreo-levitando” que disparará cerca de tí, este te seguirá y procurará estar cerca. Escoge un enemigo y envía a veces triángulos, siendo este su ataque de un rango medio.