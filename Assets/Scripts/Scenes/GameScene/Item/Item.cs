#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Item : MonoX
{
    #region Variables
    [Header("Item Settings")]
    public ItemContent type;
    [Space]
    public Rigidbody body;
    private Vector3 lastVel;
    //TODO revisar los fallos

    #endregion
    #region Events
    private void Awake(){
        //Get(out body);
    }
    private void Update()
    {
        CheckBody();
    }
    #endregion
    #region Methods

    private void CheckBody()
    {
        if (GameManager.IsOnGame()){
            //si se reanuda y andaba durmiendo...
            if (body.IsSleeping()) { body.velocity = lastVel; }
            lastVel = body.velocity;
            body.WakeUp();
        }else{
            body.Sleep();
        }
    }
    #endregion
}

public enum ItemContent
{
    SQUARE,
    CIRCLE,
    TRIANGLE,

    /// Mejoras posibles en el juego
    ATK_SPEED,
    TARGET_SHOT,
    FROST,
    STREGHT,
    SPEED
}