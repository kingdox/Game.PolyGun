#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
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
    /// <param name="newItem"></param>
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

/// <summary>
/// Modelo de los items de los logros
/// <para>Dependencia con <seealso cref="Limit"/></para>
/// </summary>
public struct TextValBarItem
{
    public string title;
    public int value;
    public Limit limit;
    //public (int, int, int) limits;
}


/// <summary>
/// Muestra los valores de cada etapa limite
/// <para>Nota: Hay partes sin uso, solo son para aprendizaje</para>
/// </summary>
public struct Limit{
    public float bronze, silver, gold;
    /// <summary>
    /// Construye el limite con los valores asignados
    /// </summary>
    public Limit(float bronze, float silver, float gold)
    {
        this.bronze = bronze;
        this.silver = silver;
        this.gold = gold;
    }
    /// <summary>
    /// Usas un limit para asignar un limit
    /// </summary>
    public Limit(Limit limits) => this = limits;
    /// <summary>
    /// Usas un arreglo de 3 valores para asignar un limit
    /// </summary>
    public Limit(float[] limits) => this = new Limit(limits[0], limits[1], limits[2]);
    /// <summary>
    /// Devuelve el Limit en un arreglo
    /// </summary>
    public float[] ToArray() => new float[] { bronze, silver, gold };
    /// <summary>
    /// Devuelve el valor especificado del limit
    /// </summary>
    private float Get(int i) => ToArray()[i];
    /// <summary>
    /// Asigna el nuevo valor al limit
    /// </summary>
    private float Set(int i, float val)
    {
        float[] _newLimit = ToArray();
        _newLimit[i] = val;
        bronze = _newLimit[0];
        silver = _newLimit[1];
        gold = _newLimit[2];
        return val;
    }
    /// <summary>
    /// Se crea un get set por indice
    /// </summary>
    public float this[int index]
    {
        get => Get(index);
        set => Set(index, value);
    }
}