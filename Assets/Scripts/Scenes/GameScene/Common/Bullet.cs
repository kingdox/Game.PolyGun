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
        Get(out movement);
        Get(out rotation);
        initPos = transform.position;
    }

    private void Start()
    {
        //Si puede seguir busca a un enemigo y se ajusta
        if (canFollow)
        {
            Transform tran_finder = GameManager.GetEnemiesContainer();
            direction = tran_finder.GetChild(tran_finder.childCount - 1).position;
            rotation.LookTo(direction);
        }
    }
    
    private void Update(){ 

        if (PassedRange()){
            Destroy(gameObject);
        }else{

            if (canFollow)
            {
                //updates the rotation based on the actual direction
                rotation.LookTo(direction);
                //movement following the actual directiong
                movement.Move(direction, bulletShot.speed, canFollow);
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

                Enemy enemy = other.GetComponent<Enemy>();
                enemy.CheckDamage(bulletShot.damage);
            }

            Destroy(gameObject);
        }
    }
    #endregion
    #region Methods

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