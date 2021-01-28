#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Destructure))]
[RequireComponent(typeof(SaveVelocity))]
public class Bullet : MonoX
{
    #region Variables

    private Movement movement;
    private Rotation rotation;
    private Destructure destructure;
    private SaveVelocity saveVelocity;
    private Vector3 initPos;
    [Header("Bullet Settings")]
    [Space]
    public BulletShot bulletShot;
    public Vector3 direction;

    [Space]
    public bool canFollow = false;
    #endregion
    #region Events
    private void Awake(){
        Get(out destructure);
        Get(out movement);
        Get(out rotation);
        GetAdd(ref saveVelocity);

        initPos = transform.position;
    }

    private void Start()
    {
        //Si puede seguir busca a un enemigo y se ajusta
        if (canFollow)
        {
            Transform tran_enemy = TargetManager.GetEnemy(transform);

            //si no consigue enemigo...
            if (tran_enemy == null) 
            {
                canFollow = false;
            }
            else
            {
                direction = tran_enemy.position;
                rotation.LookTo(direction);
            }
        }
    }
    
    private void Update(){ 

        if (PassedRange()){
            DeleteBullet();
        }
        else
        {

            if (canFollow)
            {
                //updates the rotation based on the actual direction
                rotation.LookTo(direction);
                //movement following the actual directiong
                if (movement.Move(direction, bulletShot.speed, canFollow))
                {
                    DeleteBullet(); 
                }
            }
            else
            {
                //Lo movemos con el ejeZ
                movement.Move(transform.forward.normalized, bulletShot.speed, canFollow);
            }
        }
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position, direction);
    //}


    //TODO refactorizar
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.transform.tag;

        if (!IsTag(tag, "obstacle", "enemy", "player", "ally")) return;

        if (IsTag(tag,"obstacle"))
        {   
            DeleteBullet();
            return;
        }

        switch (bulletShot.owner)
        {
            case CharacterType.ALLY:
            case CharacterType.PLAYER:
                if (!IsTag(tag, "enemy")) return;

                ShotByAllyOrPlayer(other.transform);
                break;
            case CharacterType.ENEMY:
                if (!IsTag(tag, "player", "ally")) return;
                
                ShotByEnemy(other.transform);
                break;
            default:
                //otro..... 
                break;
        }

        DeleteBullet();
    }
    #endregion
    #region Methods



    /// <summary>
    /// Check if the enemy is the selected, then it inflict the damage
    /// </summary>
    private void ShotByAllyOrPlayer( Transform enemy)
    {

        Minion minion = enemy.GetComponent<Minion>();
        if (minion != null)
        {
            minion.character.timeLife -= bulletShot.damage;
            minion.body.AddForce(transform.forward * 5, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("Este enemy no tiene asignado el Minion component");
        }
    }


    /// <summary>
    /// TODO
    /// </summary>
    private void ShotByEnemy(Transform allyOrPlayer)
    {

    }






    public void DeleteBullet()
    {
        destructure.DestructureThis();
        Destroy(gameObject);
    }

    /// <summary>
    /// Set the direction 
    /// </summary>
    public void SetDirection(Transform trans) {
        rotation.SetDirection(trans.rotation);
        direction = trans.position;
    }

    /// <summary>
    /// Preguntamos si ha pasado el rango desde el punto inicial con el actual
    /// </summary>
    private bool PassedRange() => Vector3.Distance(initPos, transform.position) > bulletShot.range;



    /// <summary>
    ///  Checks for on of the tags
    /// </summary>
    private bool IsTag(string tag, params string[] tags) => XavLib.XavHelpTo.Know.IsEqualOf(tag, tags);
    #endregion
}