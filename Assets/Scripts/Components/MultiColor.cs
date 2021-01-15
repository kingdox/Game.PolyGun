#region imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavLib;
#endregion
/// <summary>
/// Cambia el "Color deseado" de <see cref="ImageController"/>
/// Cada cierto tiempo
/// <para>Dependencia con <see cref="ImageController"/></para>
/// </summary>
public class MultiColor : MonoBehaviour
{
    #region Variable
    [Header("Settings")]
    public ImageController imgController;
    [Space]
    public Color[] colors;
    [Header("Time")]
    public float tick = 1;
    private float count = 0;
    #endregion
    #region Events
    private void Start(){
        if (!imgController) imgController = transform.GetComponent<ImageController>();
        if (!imgController) imgController = this.gameObject.AddComponent<ImageController>();

    }
    private void Update()
    {
        count = XavHelpTo.Set.TimeCountOf(count, tick);
        if (count.Equals(0)) {
            ChangeColor();
        };
    }
    #endregion
    #region Methods
    /// <summary>
    /// Cambiamos a uno de los colores escogidos
    /// </summary>
    public void ChangeColor(){
        imgController.color_want = colors[XavHelpTo.Get.ZeroMax(colors.Length)];
    }
    #endregion
}
