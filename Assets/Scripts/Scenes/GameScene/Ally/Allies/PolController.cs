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
    [Space]
    [Header("Buffs")]
    public Transform buffList;
    public ItemBuff[] buffs;
    [Space]
    public ParticleSystem par_explode;
    public ParticleSystem part_attack;


    #endregion
    #region
    private void Start()
    {
        Get(out equipment);
        GetChilds(out buffs, buffList);
        LoadMinion();
    }
    private void Update()
    {
        if (UpdateMinion())
        {
            BuffsUpdate();

            //Check the craft
            equipment.WaitedCraft(ref character, ref buffs);

            if (target != null)
            {
                AttackUpdate(ref canDamage, ref damageTimeCount);


                if (target != transform)
                {
                    //movements
                    UpdateMovement();
                    //rotates
                    rotation.LookTo(target.position);

                    if (target.CompareTag("item"))
                    {
                        //check for the item
                        ItemChecker();
                    }
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
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, character.range / minRangeSize);
    //    if (target != null && target != transform)
    //    {
    //        Gizmos.DrawLine(transform.position, target.position);
    //    }
    //}
    private void OnCollisionStay(Collision collision)
    {
        if (CanAttack(collision.transform, canDamage, "enemy"))
        {
            canDamage = false;
            part_attack.Play();
            MinionAttackMinion(collision.transform);
        }

    }
    private void OnDisable()
    {
        par_explode.Play();
        TargetManager.EffectInTime(par_explode);
    }
    #endregion
    #region  Method


    /// <summary>
    /// Recorre la lista de buffs existentes y aplica sus propiedades en caso
    /// de que exista
    /// </summary>
    private void BuffsUpdate()
    {
        for (int x = 0; x < buffs.Length; x++)
        {
            //revisamos si NO ha podido aplicar el buff
            buffs[x].CanApplyBuff(ref character);
        }
    }


    /// <summary>
    /// Checks the target wheter is 
    /// </summary>
    private void ItemChecker()
    {
        //if the distance between the item and pol is inferior of the 
        bool isOnRange = Vector3.Distance(transform.position, target.position) < character.range / minRangeSize;
        if (!isOnRange) return;

        //if the actual target is the item and if is on the range
        //Debug.Log("Pol: Estoy en contacto Poly, buscaré recoger un item...");
        //1 - Revisamos si tenemos espacio para incluirlo
        //tomaremos los slots y veremos si, hay espacio vacío, en caso de estar lleno usaremos un buff para llenarlo con el item target
        int index = equipment.GetVoidSlotIndex();

        //TODO tambien hacer logica en caso de que nesecitemos comida para continuar....


        // si están llenos los huecos de items
        if (index.Equals(-1))
        {
            // tomaremos el indice que contenga un buff para consumirlo
            index = equipment.GetBuffSlotIndex();
        }

        //si hay un hueco o un buff entonces hace acciones ahí
        if (!index.Equals(-1))
        {
            ActionType action = equipment.Action(index);
            //PrintX($"{action.item} {action.used}");

        }
        else
        {
            Debug.Log("Hola");
        }

        /*
        //si hay algo por fabricar
            equipment.WaitedCraft(ref character, ref buffs);
        */



        // 2 - Tomamos el objeto, y lo colocamos en un hueco vacío

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

            //mira cual está mas cerca
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