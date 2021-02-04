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

    /*
     * FIXME hay un error que sale cada x tiempo, intenta acceder al transform para destruirlo, pero no puede por que es nulo o ha sido destruido
     Your script should either check if it is null or you should not destroy the object.
    UnityEngine.Transform.get_position () (at <3d496c8e82ae4ebdb5d2754c57c486cf>:0)
    Minion.IsInRange (System.Single range) (at Assets/Scripts/Scenes/GameScene/Common/Minion.cs:90)
    PlurController.PathUpdate () (at Assets/Scripts/Scenes/GameScene/Enemy/PlurController.cs:122)
    PlurController.Update () (at Assets/Scripts/Scenes/GameScene/Enemy/PlurController.cs:35)

     
     
     */


    /// <summary>
    ///  Follow the nearest enemy, else it follow itself (does'nt move)
    /// </summary>
    private void PathUpdate()
    {
        if (Timer(ref refreshTargetCount, 1))
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
                //TODO aquí hay un error cada largor ato, pero se debe a falta del tyype transform 
                float targetDistance = Vector3.Distance(transform.position, target.position);
                if (playerDistance > targetDistance)    
                {
                    target = player;
                }

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