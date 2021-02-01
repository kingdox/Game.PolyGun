#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
[RequireComponent(typeof(Shot))] //para cuando puede disparar...
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
    private Shot shot;
    private Transform _player;


    //cuando se vuelve true puede atacar cuando quiera, puesto que tiene el perseguir bala
    private bool canAttackInDistance = false;
    [Space]
    private float damageTimeCount;
    private bool canDamage;
    [Space]

    public Priority priority = Priority.ITEM;
    public enum Priority
    {
        ENEMY,
        ITEM,
        CRAFT
    }
    public enum AttackMode
    {
        CAC,
        RANGED
    }
    public AttackMode attackMode = AttackMode.CAC;
    #endregion
    #region
    private void Start()
    {
        Get(out shot);

        LoadMinion();

        //TODO ajustar el rango de cogida de items con char.range

        //TODO el Pol SIEMPRE ataca al enemigo sin importar su rango, puesto que este se rige por comsas como boxbox
        _player = TargetManager.GetPlayer();
    }
    private void Update()
    {
        if (UpdateMinion())
        {

            //bool modoDeAtaque=false;
            //if (modoDeAtaque)
            //{
            //    //CaC
            //}
            //else
            //{
            //    //Rango
            //}



            //bool modoTarget = false;
          


            /////Moverse a un item, moverse a un enemigo cercano
            //Vector3 moverse;

            ////mirar a un Item, mirar a el enemigo mas cercano
            ////
            //Quaternion rotart;


            ////siempre que peuda
            //bool estoyCercaDelItem = false;
            //if (estoyCercaDelItem)
            //{
            //    //coge el item
            //}


            //bool tengoBuffs = false;    
            //if (tengoBuffs)
            //{
            //    //cambia cosas para conveniencia del Pol
            //}

        }
    }
    #endregion
    #region 



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


        if (priority.Equals(Priority.ITEM))
        {
            //Buscas un item (prioridad)
        }
        else
        {
            //Buscas un enemigo (estos deben de estar MUY cerca)
        }

    }

    #endregion
}