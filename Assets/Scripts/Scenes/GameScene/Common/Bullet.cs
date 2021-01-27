#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Bullet : MonoX
{
    #region Variables

    private Movement movement;
    private Rotation rotation;
    private Destructure destructure;
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
        initPos = transform.position;
    }

    private void Start()
    {
        //Si puede seguir busca a un enemigo y se ajusta
        if (canFollow)
        {
            Transform tran_enemy = EnemyManager.GetEnemy(transform);

            //si no consigue enemigo...
            if (tran_enemy == null) 
            {
                canFollow = false;


            }
            else
            {
                direction = tran_enemy.position;
                //direction = transform.position + transform.forward.normalized;
                //canFollow = false;
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, direction);
    }
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.transform.tag;
        
        if (XavLib.XavHelpTo.Know.IsEqualOf(tag, "obstacle", "enemy"))
        {
            if (tag.Equals("enemy")){
                //TODO
                Enemy enemy = other.GetComponent<Enemy>();
                //si no hay enemigo?
                enemy.CheckDamage(bulletShot.damage);
            }
            DeleteBullet();
        }
    }
    #endregion
    #region Methods

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

    #endregion
}