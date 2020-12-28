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

    [Space]
    public Image img_bar_last;
    [Space]
    public Image img_bar_actual;
    public RectTransform rect_bar_actual;


    #endregion
    #region Events
    #endregion
    #region Methods

    /// <summary>
    /// Se asigna los datos del modelo de los valores del item
    /// </summary>
    public void SetItem(TextValBarItem newItem) {
        item = newItem;
        DrawItem();
    }

    /// <summary>
    /// Se encarga de pintar el Item con los valores que posee de <see cref="TextValBarItem"/>
    /// </summary>
    private void DrawItem()
    {
        txt_title.text = item.title;
        txt_value.text = item.value.ToString() + ( item.value > item.limit.gold ? "" : " / " + item.limit.gold.ToString());

        float[] _limits = item.limit.ToArray();
        int _limitIndex = XavHelpTo.KnowFirstMajorIndex(item.value, item.limit.ToArray());
        //ajustamos el progreso de la barra

        if (_limitIndex != -1){

            rect_bar_actual.anchorMax = new Vector2(XavHelpTo.KnowPercentOfMax(item.value, _limits[_limitIndex]) / 100, 1);

        }
        else
        {
            //Si supera los limites
            img_bar_last.color = XavHelpTo.SetColorParam(img_bar_last.color, (int)ColorType.RGB, 0);
            img_bar_actual.color = colorSteps[_limitIndex];
            rect_bar_actual.anchorMax = new Vector2(1, 1);
        }
    }
    #endregion
}