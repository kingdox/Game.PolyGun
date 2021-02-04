#region Imports
using UnityEngine;
#endregion

public class PlurController : Minion
{

    #region Variables
    [Header("Plur Settings")]
    public float damageTimeCount = 0;
    public bool canDamage = true;
    private float refreshTargetCount;
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

            if (target != null)
            {


                if (target != transform)
                {
                    rotation.LookTo(target.position);

                }

                //if (IsInRange())
                //{

                //}

                //only moves if is ready to attack again
                if (canDamage)
                {
                    UpdateMovement();
                }
                else
                {
                    movement.StopMovement();
                }



                if (Timer(ref refreshTargetCount, 1))
                {
                    UpdateTarget();
                }
            }
            else
            {
                UpdateTarget();
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
                    sfx_items[(int)Sfx.Action].PlaySound();
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


    private void UpdateMovement()
    {
        //even if the pos in y is less than 3 then you can move it
        if (transform.position.y < 3)
        {
            //moves it
            if (!IsInRange())
            {
                movement.Move(transform.forward.normalized, character.speed);
            }
            else
            {
                movement.StopMovement();
            }
        }
        else
        {
            transform.position += Vector3.down * Time.deltaTime;
        }

    }

    private void UpdateTarget()
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
    #endregion
}