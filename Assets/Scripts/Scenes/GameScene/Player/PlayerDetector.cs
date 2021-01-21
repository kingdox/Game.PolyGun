#region 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavLib;
#endregion

public class PlayerDetector : MonoX
{
    #region Variables

    private Rigidbody body;
    private readonly string[] tags = { "enemy", "item" };


    [Header("Detector Item")]

    ///Vemos si tenemos un item cerca a lo esperdo
    public bool isAnItemNear = false;
    public Vector3 lastNearPos;
    public GameObject lastObj;
    public float _distance;

    #endregion
    #region Events

    private void Awake(){
        Get(out body);

    }
    private void OnTriggerStay(Collider other){

        //PrintX($"En contacto con algo {collision.gameObject.name}");
       
        if (XavHelpTo.Know.IsEqualOf(other.tag, tags)){


            // si el objeto entrante es distinto que el ultimo
            if (other.gameObject != lastObj){
                //recogemos el objeto mas cercano

                float _newDistance = Vector3.Distance(lastNearPos, other.transform.position);

                ///Si encuentro uno más cerca del player
                if (_newDistance < _distance ){
                    _distance = _newDistance;
                    lastNearPos = other.transform.position;
                    lastObj = other.gameObject;

                    PrintX($"Encontrado {_distance}");
                }


            }




            Contact(other);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //collision.collider
    }
    #endregion
    #region Methods

    private void Contact<T>(T t){

        //PrintX($"En contacto con {typeof(T)} {t}");
    }
    #endregion
}

/*
 Debe detectar si hay un item, de ser así si está en el rango más cercano
Y que sea el más cercano de los encontrados...
 
 
 */

