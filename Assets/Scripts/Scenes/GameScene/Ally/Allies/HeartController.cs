#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
/// <summary>
///  Heart: Éste se encarga de aumentar el “tiempo de vida” de la figura aliada más cercana,
///  dando prioridad al jugador si llega a un límite determinado,
///  este pierde parte de su “tiempo de vida” al recuperar a un aliado.la figura no ataca.
/// </summary>
public class HeartController : Minion
{
    #region Variables
    [Header("Heart Settings")]


    public float refreshTargetCount;
    public Transform player;
    
    [Space]
    //rangos para perseguir al target
    public float minRangeSize;
    public float maxRangeSize;

    #endregion
    #region Events
    private void Start()
    {
        LoadMinion();

        player = TargetManager.GetPlayer();
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

                //Rotation & attack Refresh
                if (target != transform)
                {
                    // rotates to that target
                    rotation.LookTo(target.position);
                }


            }
            else
            {
                UpdateTarget();
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (target != null)
        {
            //range to see the distance line
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, target.position);

            //max range to move
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(target.position, character.range / minRangeSize);

            //min range to back
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(target.position, character.range / maxRangeSize);
            
        }
    }
    #endregion
    #region Methods

    private void UpdateHeal()
    {

    }

    //TODO
    private void UpdateTarget()
    {
        target = TargetManager.GetAlly(transform);

        PlayerController comp = target.GetComponent<PlayerController>();

        if (target != null)
        {
            //busca un aliado cercano
        }
        else
        {

            //por defecto al player
            target = player;
        }
    }

    private void UpdateFollow()
    {

    }

    #endregion
}