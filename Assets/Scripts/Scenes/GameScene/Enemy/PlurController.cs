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
            AttackUpdate();

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
    private void OnCollisionEnter(Collision collision)
    {
        //si es un enemigo le hará daño y reseteara el timer de daño
        if (GameManager.IsOnGame() && (canDamage | isEnemyBoss)) 
        {

            switch (collision.transform.tag)
            {
                case "ally":
                    PlurAttack(collision.transform);
                    canDamage = false;
                    par_attack.Play();
                    //Minion minion = collision.transform.GetComponent<Minion>();
                    //MinionDamage(minion);
                    //minion.character.timeLife -= character.damage;
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
    /// action to damage a minion enemy
    /// </summary>
    /// <param name="enemyInContact"></param>
    private void PlurAttack(Transform tr)
    {
        Minion minion = tr.GetComponent<Minion>();
        MinionDamage(minion);
    }

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
        //TODO Tratar de buscar al más cercano,
        //TODO aquí se queda pegado en player
        //if can't find a target then try to look another
        if (target == null || target == transform)
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
        }
        else
        {
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
    }
    #endregion
}