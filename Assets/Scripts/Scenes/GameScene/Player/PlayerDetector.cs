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

    //Ultimo Item cercano
    public Transform nearestItem = null;
    #endregion
    #region Events

    private void Awake(){
        Get(out body);
    }
    private void OnTriggerStay(Collider other){
        CheckDetection(other.transform);
    }

    private void OnTriggerExit(Collider other){
        if (other.transform.Equals(nearestItem)){
            nearestItem = null;
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
    /// Detecta el objeto entrante y, si nota que es un objeto
    /// revisará las distancia s para guardarlo
    /// </summary>
    private void CheckDetection(Transform other)
    {
        if (XavHelpTo.Know.IsEqualOf(other.tag, tags))
        {
            // si el objeto entrante es distinto que el ultimo
            if (!other.gameObject.Equals(nearestItem))
            {

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
    }


    #endregion
}