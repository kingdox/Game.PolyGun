#region imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class ChildLimiter : MonoBehaviour
{
    #region Methods
    [Header("ChildLimit Settings")]
    public int limitChilds = 500; // hotline miami pero limitado >:(
    //cual empieza a eliminar...
    public bool StartByOld = true;

    private string savedName;
    #endregion
    #region Events
    private void Start()
    {
        savedName = name;
    }
    void Update()
    {
        name = $"{savedName} {transform.childCount} [{limitChilds}] ";
        if (transform.childCount >= limitChilds)
        {
            ClearSomeChilds();

        }

    }
    #endregion
    #region Methods


    /// <summary>
    /// Clear the qty of child
    /// </summary>
    private void ClearSomeChilds()
    {
        int qty2Clear = transform.childCount - limitChilds;

        int orientation = StartByOld ? 1 : -1;
        int startCount = StartByOld ? 0 : transform.childCount - 1;

        for (int i = 0; i < qty2Clear; i++)
        {   
            Destroy(transform.GetChild(i).gameObject);
        }
        //for (int i = startCount; qty2Clear <= 0; i += orientation){
        //    Destroy(transform.GetChild(i).gameObject);
        //}


    }
    #endregion
}