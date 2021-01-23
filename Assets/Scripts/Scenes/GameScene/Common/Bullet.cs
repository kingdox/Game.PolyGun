#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Bullet : MonoX
{
    #region Variables

    private Movement movement;
    private Vector3 initPos;
    [Header("Bullet Settings")]
    [Space]
    public BulletShot bulletShot;
    public Vector3 direction;

    #endregion
    #region Events
    private void Awake(){
        Get(out movement);
        initPos = transform.position;
    }
    private void Start()
    {
        movement.SetAxis(Vector3.Normalize(direction));
        movement.speed = bulletShot.speed;
    }
    private void Update(){

        if (PassedRange()){
            Destroy(gameObject);
        }else{
            movement.Move(Vector3.Normalize(direction), bulletShot.speed);
        }
    }
    #endregion
    #region Methods


    /// <summary>
    /// Preguntamos si ha pasado el rango desde el punto inicial con el actual
    /// </summary>
    private bool PassedRange() => Vector3.Distance(initPos, transform.position) > bulletShot.range;

    #endregion
}