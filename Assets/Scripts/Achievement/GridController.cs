#region imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion
//[RequireComponent<VerticalLayoutGroup>]

public class GridController : MonoBehaviour
{
    #region Var

    //Donde adminsitraremos nuestros items con los tamaños esperados
    private GridLayoutGroup grid;
    // nuestro rect para ajustarnos a él
    private RectTransform rect;

    [Header("Settings")]
    //Conocimiento para saber el tamaño esperado
    public int countOfItems = 0;
    public Vector2 spacing = new Vector2(0,0);
    public Padding padding = new Padding(0,0,0,0);


    //Extra
    //con estos sabremos cuanto miden
    [Space]
    [Header("Tamano de el objeto")]
    public float objWidth = 0;
    public float objHeight = 0;
    [Space]
    [Header("Tamaño de hijos")]
    public Vector2 childSize = new Vector2(0, 0);


    #endregion
    #region Events
    private void Awake(){

        //Mejora el rendimiento si los asignas con el inspector
        grid = GetComponent<GridLayoutGroup>();
        rect = GetComponent<RectTransform>();
        countOfItems = transform.childCount;

        Vector2 unit_size = (rect.anchorMax - rect.anchorMin) * 100;
        Debug.Log(unit_size.ToString());

        objWidth = XavHelpTo.KnowQtyOfPercent(unit_size.x, Screen.width);
        objHeight = XavHelpTo.KnowQtyOfPercent(unit_size.y, Screen.height);
        Debug.Log($"H {objHeight}, W {objWidth}");
    }

    #endregion
    #region Methods








    #endregion
    /* 
     * Dependiendo de lo bien que vaya el script puede que se generalice
     * Aquí manejaremos el componente de verticalLayoutGroup
     * para que a nivel externo podamos ajustarlo por porcentajes 
     * en vez de manualmente en el inspecctor
     * 
     * Este script tiene como ideal de que si alguien externo lo llama
     * para modificarlo puede cambiar sus valores y así ajustará el item
     * lo mejor posible
     * 
     * el vertical es mas un contenedor.... que apila verticalmente
    */

}
public struct Padding{
    public float L;
    public float R;
    public float T;
    public float B;
    public Padding(float L, float R, float T, float B)
    {
        this.L = L;
        this.R = R;
        this.T = T;
        this.B = B;
    }
}