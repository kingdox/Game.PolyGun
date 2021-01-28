#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
public class PlurController : Enemy
{

    #region Variables

    public float damageTimeCount;
    public bool canDamage;
    #endregion
    #region Events
    private void Start()
    {

        InitEnemy();
    }
    private void Update()
    {

        if (UpdateEnemy())
        {
            AttackUpdate();
            PathUpdate();
        }
        else
        {
            //pausa el movimiento
            Move();
        }
        //if (!navMeshAgent.isOnNavMesh)
        //{
        //    XavLib.XavHelpTo.Look.WithColor("No nos encontramos en la malla, ver como recalcular");
        //    navMeshAgent.ResetPath();
        //}
        
        //if (navMeshAgent)
        //{

        //}
        //navMeshAgent.ResetPath
    }
    private void OnCollisionEnter(Collision collision)
    {
        //si es un enemigo le hará daño y reseteara el timer de daño
        if (canDamage) 
        {
            canDamage = false;

            switch (collision.transform.tag)
            {
                case "ally":
                    Ally ally = collision.transform.GetComponent<Ally>();
                    ally.character.timeLife -= character.damage;
                    break;
                case "player":
                    PlayerController player = collision.transform.GetComponent<PlayerController>();
                    player.character.timeLife -= character.damage;
                    break;
                default:
                    break;
            }
        }
    }
    #endregion
    #region Methods
    /// <summary>
    /// Updates the attack of BoxBox
    /// </summary>
    private void AttackUpdate()
    {
        if (!canDamage && Timer(ref damageTimeCount, character.atkSpeed))
        {
            canDamage = true;
        }
    }

    /// <summary>
    ///  Follow the nearest enemy, else it follow itself (does'nt move)
    /// </summary>
    private void PathUpdate()
    {
        //if can't find a target then try to look another
        if (target == null || target == transform)
        {
            //Buscar el player o un ally
            Transform player = TargetManager.GetPlayer();
            target = TargetManager.GetEnemy(transform);

            float playerDistance = Vector3.Distance(transform.position, player.position);

            //si es nulo la busqueda entonces por defecto agarra al player
            if (target == null)
            {
                //busca el player
                target = player;
            }
            else
            {
                float targetDistance = Vector3.Distance(transform.position, target.position);
                if (playerDistance > targetDistance)    
                {
                    target = player;
                }

            }
            Move(target);
        }
    }
    #endregion
}