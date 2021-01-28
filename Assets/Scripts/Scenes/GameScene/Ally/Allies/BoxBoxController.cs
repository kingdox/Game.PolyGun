#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
[RequireComponent(typeof(Rotation))]
public class BoxBoxController : Ally
{
    #region Variable

    private Rotation rotation;
    private Movement movement;
    [Header("BoxBox Settings")]
    public float damageTimeCount;
    public bool canDamage;

    #endregion
    #region Events
    private void Start()
    {
        GetAdd(ref rotation);
        GetAdd(ref movement);

        target = transform;
    }
    private void Update()
    {

        if (UpdateAlly())
        {
            AttackUpdate();
            PathUpdate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //si es un enemigo le hará daño y reseteara el timer de daño
        if (GameManager.IsOnGame() && canDamage && other.transform.CompareTag("enemy"))
        {
            canDamage = false;
            Enemy enemy = other.transform.GetComponent<Enemy>();
            enemy.character.timeLife -= character.damage;
        }
    }
    #endregion
    #region Methods


    /// <summary>
    /// Check if the ally is on the range between he and the target
    /// </summary>
    private bool IsInRange()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance < character.range;
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
    ///  rotates if exist a enemy target
    /// </summary>
    private void PathUpdate()
    {
        //if can't find a target then try to look another
        if (target != null && target != transform)
        {

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
        else
        {
            //BoxBox everytime try to find a enemy
            target = TargetManager.GetEnemy(transform);
        }

    }

    #endregion
}
