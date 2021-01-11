#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavLib;
#endregion

public class ImageController : MonoBehaviour , IImageController
{
    #region
    [Header("Setting")]
    public Image img;
    [Header("Want Settings")]
    public Color color_want; // => El color que queremos apuntar
    [Space]
    public bool keepUpdate = true;
    public float scaleSpeed = 1;
    [Space]
    [Header("Disabler, default -1")]
    public  float timeToDisable = -1;
    #endregion
    #region
    private void Awake()
    {
        if (!img) img = GetComponent<Image>();
        if (!timeToDisable.Equals(-1)) StartCoroutine( DisableOn());
    }
    private void Update() {
        if (keepUpdate) UpdateColor();
    }
    #endregion
    #region

    /// <summary>
    /// Actualiza a lo largo del tiempo y la velocidad aplicada
    /// </summary>
    private void UpdateColor()
    {
        Color _c = img.color;
        for (int x = 0; x < 4; x++) _c[x] = XavHelpTo.Set.UnitInTime(_c[x], color_want[x], scaleSpeed);
        img.color = _c;

        //Si se tiene que desactivar al terminar y ha terminado
        //if (stopIfEnd && IsEnd()) keepUpdate = false;
    }

    /// <summary>
    /// Desactivas el objeto tras x tiempo
    /// </summary>
    private IEnumerator DisableOn(){
        yield return new WaitForSeconds(timeToDisable);
        gameObject.SetActive(false);
    }
    public bool IsEnd() => img.color.Equals(color_want);
    #endregion
}

interface IImageController
{
    /// <summary>
    ///  Revisa si ha terminado
    ///  TODO no funciona XD
    /// </summary>
    bool IsEnd();
}