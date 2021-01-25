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
    private Rigidbody body;
    private Vector3 lastVel;

    #endregion
    #region Events
    private void Awake(){
        Get(out body);
    }
    private void Update()
    {
        CheckBody();
    }
    #endregion
    #region Methods

    private void CheckBody()
    {
        //TODO puede que entre para MONOX ? TODO
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

[SerializeField]
public enum ItemContent{
    NO = -1,

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


//public enum ItemType2
//{
//    NO = -1,

//    SHAPE,
//    BUFF
//}
////Test
//public struct ItemContentType2
//{
//    ItemType2 type;
//    BuffType buff;
//}

