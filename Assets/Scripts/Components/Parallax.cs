#region
using UnityEngine;
using XGet = XavHelpTo.Get.Get;
using XavHelpTo.Build;
#endregion

//Este paralax solo usa el eje X
public class Parallax : MonoX
{
    #region Var
    //Atenuaciond el movimiento horizontal
    public float attenX = 0.01f;

    //Atenuacion del movimiento vertical
    public float attenY = 0;

    public float speed;
    [Space]
    public float timeRangeMax;
    //Control de offset de la textura
    private Vector2 pos = Vector2.zero;
    private float count;
    private float timer = 3f;
    //Referencia el renderer del BG
    private Renderer rend;
    #endregion
    #region Events

    private void Start() {
        rend = GetComponent<Renderer>();

        RefreshParallax();
    }
    #endregion
    #region Methods

    private void Update() {

        //cada vez que pasa ese tiempo...
        if (Timer(ref count, timer)) RefreshParallax();


        pos += new Vector2(attenX, attenY) * Time.deltaTime * speed;
        rend.material.SetTextureOffset("_MainTex", pos);
    }

    /// <summary>
    /// Updates the parallax with new things..
    /// </summary>
    private void RefreshParallax()
    {
        timer = timeRangeMax.ZeroMax();

        ChangeMinusPlus(ref attenX);
        ChangeMinusPlus(ref attenY);
    }


    /// <summary>
    /// Change sometimes the value - or +
    /// </summary>
    private void ChangeMinusPlus(ref float val) => val = val * (XGet.RandomBool() ? 1 : -1);
    #endregion



}
