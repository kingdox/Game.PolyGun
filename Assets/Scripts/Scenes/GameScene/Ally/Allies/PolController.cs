#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
//[RequireComponent(typeof(Shot))] //para cuando puede disparar...
[RequireComponent(typeof(Equipment))]
/// <summary>
/// Este recoge los fragmentos y mejoras más cercanos,
/// - sus ataques son de corto rango
/// - se encarga de crear figuras o mejoras al conseguir 3 fragmentos,
/// - atacará a los enemigos débilmente.
/// </summary>
public class PolController : Minion
{

    #region
    [Header("Pol Settings")]

    //attack
    private float damageTimeCount;
    private bool canDamage;
    // target refresher
    private float refreshTargetCount;
    ///Equipments
    public float minRangeSize = 2.5f;
    public Equipment equipment;
    //[Space]
    //ranged attack
    //private Shot shot;
   
    #endregion
    #region
    private void Start()
    {
        //Get(out shot);
        Get(out equipment);

        LoadMinion();
    }
    private void Update()
    {
        if (UpdateMinion())
        {

            if (target != null)
            {
                AttackUpdate();

                if (target != transform)
                {
                    //movements
                    UpdateMovement();
                    //rotates
                    rotation.LookTo(target.position);
                    //check for the item
                    ItemChecker();
                }

                //Refresh the target
                if (Timer(ref refreshTargetCount, 1)  )
                {
                    UpdateTarget();
                }
            }
            else
            {
                UpdateTarget();
            }


            ////siempre que peuda
            //bool estoyCercaDelItem = false;
            //if (estoyCercaDelItem)
            //{
            //    //coge el item
            //}

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, character.range / minRangeSize);
        if (target != null && target != transform)
        {
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
    #endregion
    #region 


    /// <summary>
    /// Updates the attack of BoxBox
    /// </summary>
    private void AttackUpdate()
    {
        if (!canDamage && Timer(ref damageTimeCount, character.atkSpeed))
        {
            //can attack again
            canDamage = true;
        }
    }



    /// <summary>
    /// Checks the target wheter is 
    /// </summary>
    private void ItemChecker()
    {
        bool isOnRange = Vector3.Distance(transform.position, target.position) > character.range / minRangeSize;

        //if the actual target is the item and if is on the range
        if (target.CompareTag("item") && isOnRange)
        {

            //1 - Revisamos si tenemos espacio para incluirlo
            //tomaremos los slots y veremos si, hay espacio vacío, en caso de estar lleno usaremos un buff para llenarlo con el item target
            int index = equipment.GetVoidSlotIndex();

            // si están llenos los huecos de items
            if (index.Equals(-1))
            {
                // tomaremos el indice que contenga un buff para consumirlo
                index = equipment.GetBuffSlotIndex();
            }

            //si hay un hueco o un buff entonces hace acciones ahí
            if (!index.Equals(-1))
            {
                //TODO
                ActionType action = equipment.Action(index);
                PrintX($"{action.item} {action.used}");

            }


            // 2 - Tomamos el objeto, y lo colocamos en un hueco vacío


        }
    }



    /// <summary>
    /// Updates the movement of the minion
    /// </summary>
    private void UpdateMovement()
    {
        bool canMove = true;

        //if you're following a enemy...
        if (target.CompareTag("enemy"))
        {
            canMove = canDamage;
        }


        if (canMove)
        {
            Vector3 direction = target.position - transform.position;
            direction = Vector3.Normalize(direction);
            direction.y = 0;
            movement.Move(direction, character.speed);
        }
        else
        {

            movement.StopMovement();
        }
    }

    /// <summary>
    /// refresh the target to get a item or an enemy as a target depending with the status and the nearest level..
    /// </summary>
    private void UpdateTarget()
    {
        //poly hará:
        /* 
         * buscará el item más cercano y el enemigo más cercano, dependiendo de cual esté más cerca, 
         * poly asignará al más cerca
         * 
        */

        Transform nearest_enemy = TargetManager.GetEnemy(transform);
        Transform nearest_item = TargetManager.GetItem(transform);
        Transform newtarget = null;

        //if exist all the "nearest"
        if (nearest_item && nearest_enemy)   
        {
            float distance_item = Vector3.Distance(transform.position, nearest_item.position);
            float distance_enemy = Vector3.Distance(transform.position, nearest_enemy.position);

            if (distance_enemy > distance_item)
            {
                //get item target
                newtarget = nearest_item;

            }
            else
            {
                //get enemy target
                newtarget = nearest_enemy;
            }

        }
        else
        {
            if (nearest_item)
            {
                //go to the item
                newtarget = nearest_item;
            }
            else if (nearest_enemy)
            {
                newtarget = nearest_enemy;  

            }
        }

        target = newtarget;
    }

    #endregion
}