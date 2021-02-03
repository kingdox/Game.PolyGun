#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class PlurController : Minion
{

    #region Variables
    [Header("Plur Settings")]
    public float damageTimeCount = 0;
    public bool canDamage = true;
    [Space]
    public ParticleSystem par_explode;
    public ParticleSystem par_attack;
    [Space]
    [Header("Plur Extra")]
    public SphereCollider boss_trigger;
    public float radius_explode = 3;
    #endregion
    #region Events
    private void Start()
    {
        LoadMinion();
    }
    private void Update()
    {

        if (UpdateMinion())
        {
            AttackUpdate(ref canDamage, ref damageTimeCount);

            if (canDamage)
            {
                PathUpdate();
            }
            else
            {
                movement.StopMovement();
            }

        }
    }
    private void OnDisable()
    {
        TargetManager.EffectInTime(par_explode);

        //si es un enemigo...
        if (isEnemyBoss)
        {
            boss_trigger.radius *= radius_explode;
        }

    }
    private void OnCollisionStay(Collision collision)
    {
        //si es un enemigo le hará daño y reseteara el timer de daño
        if (GameManager.IsOnGame() && (canDamage | isEnemyBoss)) 
        {

            switch (collision.transform.tag)
            {
                case "ally":
                    MinionAttackMinion(collision.transform);
                    canDamage = false;
                    par_attack.Play();
                    
                    break;
                case "player":
                    //Exception
                    canDamage = false;
                    PlayerController player = collision.transform.GetComponent<PlayerController>();
                    player.character.timeLife -= character.damage;
                    par_attack.Play();

                    break;
                default:
                    break;
            }
        }
    }
    #endregion
    #region Methods

   

    /// <summary>
    ///  Follow the nearest enemy, else it follow itself (does'nt move)
    /// </summary>
    private void PathUpdate()
    {

        //Buscar el player o un ally
        Transform player = TargetManager.GetPlayer();
        //busca uno de los aliados
        target = TargetManager.GetAlly(transform);

        float playerDistance = Vector3.Distance(transform.position, player.position);

        //si es nulo la busqueda entonces por defecto agarra al player
        if (target == null || isEnemyBoss)
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

      
        if (transform.position.y < 3)
        {
            //moves it
            if (!IsInRange())
            {
                rotation.LookTo(target.position);
                movement.Move(transform.forward.normalized, character.speed);
            }
            else
            {
                movement.StopMovement();
            }
        }
    }
    #endregion
}