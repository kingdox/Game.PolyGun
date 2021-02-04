#region
using UnityEngine;
#endregion
public class BoxBoxController : Minion
{
    #region Variable


    [Header("BoxBox Settings")]
    public bool canDamage;
    private float damageTimeCount;
    [Space]
    public ParticleSystem par_explode;
    public ParticleSystem part_attack;

    #endregion
    #region Events
    private void Start()
    {
        LoadMinion();
        //target = transform;
    }
    private void Update()
    {

        if (UpdateMinion())
        {
            AttackUpdate(ref canDamage, ref damageTimeCount);


            //se mueve solo cuando puede volver a hacer daño
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
        TargetManager.EffectInTime(par_explode,part_attack);
    }
    #endregion
    #region Methods


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
            //el buscado no puede estar en el aire
            //BoxBox everytime try to find a enemy
            target = TargetManager.GetEnemy(transform);

            
        }

    }


    

    #endregion
}
