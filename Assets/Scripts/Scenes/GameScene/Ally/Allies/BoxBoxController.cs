#region
using UnityEngine;
#endregion
public class BoxBoxController : Minion
{
    #region Variable

   
    [Header("BoxBox Settings")]
    public float damageTimeCount;
    public bool canDamage;
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
    private void OnCollisionEnter(Collision collision)
    {
        if (CanAttack(collision.transform))
        {
            canDamage = false;
            BoxBoxAttack(collision.transform);
        }
    }
    private void OnDrawGizmos()
    {
        if (target != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
    #endregion
    #region Methods
    private bool CanAttack(Transform tr) => GameManager.IsOnGame() && canDamage && tr.CompareTag("enemy");

    /// <summary>
    /// action to damage a minion enemy
    /// </summary>
    /// <param name="enemyInContact"></param>
    private void BoxBoxAttack(Transform enemyInContact)
    {
        Minion minion = enemyInContact.GetComponent<Minion>();
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
