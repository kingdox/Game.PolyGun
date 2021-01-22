#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Item : MonoX
{
    #region Variables
    [Header("Item Settings")]

    private Rigidbody body;

    public Vector3 lastVel;

    #endregion
    #region Events
    private void Awake(){
        Get(out body);
    }
    private void Update()
    {
        if (GameManager.IsOnGame()){

            //si se reanuda y andaba durmiendo...
            if (body.IsSleeping()){
                body.velocity = lastVel;
            }
            lastVel = body.velocity;
            body.WakeUp();
        }
        else{
            body.Sleep();
        }
    }
    #endregion
    #region Methods

    #endregion
}
