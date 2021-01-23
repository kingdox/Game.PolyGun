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
    private readonly string[] CollisionTags = { "enemy"};
    private readonly string[] triggerTags = { "item" };


    [Header("Detector Item")]

    //Ultimo Item cercano
    public Transform nearestItem = null;
    #endregion
    #region Events

    private void Awake(){
        Get(out body);
    }
    
    //private void OnCollisionEnter(Collision collision){
    //    if (XavHelpTo.Know.IsEqualOf(collision.gameObject.tag, tags)){
    //        PrintX($"OnCollisionEnter{collision.gameObject.name}");
    //    }
    //}
    private void OnTriggerStay(Collider other){
        if (XavHelpTo.Know.IsEqualOf(other.tag, triggerTags))
        {
            CheckDetection(other.transform);
        }
    }

    private void OnTriggerExit(Collider other){
        if (XavHelpTo.Know.IsEqualOf(other.tag, triggerTags))
        {
            if (other.transform.Equals(nearestItem))
            {
                nearestItem = null;
            }
        }
    }
    private void OnDrawGizmos(){
        if (nearestItem != null){
            Gizmos.DrawLine(nearestItem.position, transform.position);
        }
    }

    

    #endregion
    #region Methods
    /// <summary>
    /// Checkea el objeto con el más cercano guardado y guarda el más cercano
    /// <para>Si no hay guarda por defecto</para>
    /// </summary>
    private void CheckDetection(Transform other)
    {
        // si el objeto entrante es distinto que el ultimo
        if (!other.gameObject.Equals(nearestItem)){

            //en caso de no tener
            if (nearestItem == null)
            {
                nearestItem = other;
            }
            else
            {
                //Revisamos la distancia, sie s mas cercano que el anterior
                float oldSum = Vector3.Distance(nearestItem.position, transform.position);
                float entrySum = Vector3.Distance(other.position, transform.position);

                if (entrySum < oldSum)
                {
                    nearestItem = other;
                }

            }
        }
    }

  
    /// <summary>
    /// Colocamos el objeto más cercano a nuestro equipamiento
    /// <para>al hacerlo, este se elimina del mundo</para>
    /// </summary>
    public void SetNearestItemType(ref ItemContent slot) {
        if (nearestItem != null){
            slot = nearestItem.GetComponent<Item>().type;
            Destroy(nearestItem.gameObject);
            nearestItem = null;
        }
    }
    #endregion
}