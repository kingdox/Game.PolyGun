#region imports
using UnityEngine;
using xGet = XavHelpTo.Get;
#endregion
/// <summary>
/// Cambia el "Color deseado" de <see cref="ImageController"/>
/// Cada cierto tiempo
/// <para>Dependencia con <see cref="ImageController"/></para>
/// </summary>
public class MultiColor : MonoX
{
    #region Variable
    [Header("Settings")]
    public ImageController imgController;
    [Space]
    public Color[] colors;
    [Header("Time")]
    public float tick = 1;
    private float count = 0;

    //guardamos el wanted inicial
    private Color wantedInit;
    public bool isPlaying = true;

    #endregion
    #region Events
    private void Start(){
        if (!imgController) Get(out imgController);
        if (!imgController) Add(out imgController);

        wantedInit = imgController.color_want;

    }
    private void Update()
    {

        if (isPlaying && Timer(ref count, tick)) {
            ChangeColor();
        }
        else if(!isPlaying && imgController.color_want != wantedInit){
            //asigna el valor del color_want
            imgController.color_want = wantedInit;
        }

    }
    #endregion
    #region Methods
    /// <summary>
    /// Cambiamos a uno de los colores escogidos
    /// </summary>
    public void ChangeColor(){
        imgController.color_want = colors[xGet.Get.ZeroMax(colors.Length)];
    }
    #endregion
}
