#region 
using UnityEngine;
using XavHelpTo.Build;
using XavHelpTo.Look;
#endregion
[RequireComponent(typeof(SphereCollider))]
public class PlayerDetector : MonoX
{
    #region Variables

    //private readonly string[] CollisionTags = { "enemy"};
    private readonly string[] triggerTags = { "item" };


    [Header("Detector Item")]

    //Ultimo Item cercano
    public Item itemNear = null;
    //public SphereCollider sphereCollider_detector;
    #endregion
    #region Events
    //private void Awake()
    //{
    //    Get(out sphereCollider_detector);
    //}
    private void OnTriggerStay(Collider other){
        if (other.tag.IsEqualOf(triggerTags))
        {
            CheckDetection(other.transform);
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.tag.IsEqualOf(triggerTags))
        {
            bool isItemNearNull = itemNear == null;

            if (isItemNearNull || other.transform.Equals(itemNear.transform))
            {
                //TODO Patch, refactor this. ?
                if (!isItemNearNull)
                {
                    itemNear.Isselected = false;
                }

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