#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Achievements;
#endregion

public class AchievementItem : MonoBehaviour
{
    #region Variables
    //Los colores para cada etapa en orden
    private readonly Color[] colorSteps =
    {
        new Color(1,1,1),
        new Color(1,1,1),
        new Color(1,1,1),
    };
    private TextValBarItem item;

    [Header("Settings")]

    public Text txt_title;
    public Text txt_value;

    public RectTransform rect_bar_last;
    public GameObject obj_bar_last;
    public GameObject obj_bar_actual;


    #endregion
    #region Events
    private void Awake()
    {
        
    }
    #endregion
    #region Methods

    /// <summary>
    /// Se asigna los datos del modelo de los valores del item
    /// </summary>
    public void SetItem(TextValBarItem newItem) => item = newItem;

    /// <summary>
    /// Se encarga de pintar el Item con los valores que posee de <see cref="TextValBarItem"/>
    /// </summary>
    public void DrawItem()
    {
        txt_title.text = item.title;
        txt_value.text = item.value.ToString() + ( item.value > item.limit.gold ? "" : " / " + item.limit.gold.ToString());

        float _limitValue = GetActualLimit(); 

        rect_bar_last.anchorMax =
            new Vector2(
                XavHelpTo.KnowPercentOfMax(item.value, _limitValue) / 100,
                rect_bar_last.anchorMax.y)
            ;
    }

    /// <summary>
    /// Devuelve el limite actual basado en los valores dados
    /// </summary>
    private float GetActualLimit()
    {
        //Recorro los limites del item
        foreach (float limitval in item.limit.ToArray())
        {
            //si el limite es mayor que el valorguarda y retorna
            if (item.value <= limitval) return limitval;
        }
        //En caso de no encontrar retorna el item.value, pues ya ha superado las metas
        return item.value;
    }
    #endregion
}