#region 
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
    public Item itemNear = null;

    #endregion
    #region Events

    private void Awake(){
        Get(out body);
    }
 
    private void OnTriggerStay(Collider other){
        if (XavHelpTo.Know.IsEqualOf(other.tag, triggerTags))
        {
            CheckDetection(other.transform);
        }
    }

    private void OnTriggerExit(Collider other){
        if (XavHelpTo.Know.IsEqualOf(other.tag, triggerTags))
        {
            if (itemNear == null || other.transform.Equals(itemNear.transform))
            {
                itemNear.Isselected = false;
                itemNear = null;
            }
        }
    }
    private void OnDrawGizmos(){
        if (itemNear != null){
            Gizmos.DrawLine(itemNear.transform.position, transform.position);
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
        Item itemOther = other.GetComponent<Item>();

        // si el objeto entrante es distinto que el ultimo
        if (!itemOther.Equals(itemNear)){

            //en caso de no tener
            if (itemNear == null)
            {
                itemNear = itemOther;
                itemNear.Isselected = true;
            }
            else
            {
                //Revisamos la distancia, sie s mas cercano que el anterior
                float oldSum = Vector3.Distance(itemNear.transform.position, transform.position);
                float entrySum = Vector3.Distance(other.position, transform.position);

                if (entrySum < oldSum)
                {
                    itemNear.Isselected = false;
                    itemOther.Isselected = true;
                    itemNear = itemOther;
                }

            }
        }
    }

  
    /// <summary>
    /// Colocamos el objeto más cercano a nuestro equipamiento
    /// <para>al hacerlo, este se elimina del mundo</para>
    /// </summary>
    public void SetNearestItemType(ref ItemContent slot) {
        if (itemNear != null){
            slot = itemNear.type;
            Destroy(itemNear.gameObject);
            itemNear = null;
        }
    }
    #endregion
}