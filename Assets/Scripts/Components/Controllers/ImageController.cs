#region
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo.Set;
#endregion

public class ImageController : MonoBehaviour , IImageController
{
    #region
    private Color color_initial;
    [Header("Setting")]
    public Image img;
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
    }
  
    private void Start()
    {
        img.enabled = true;
        color_initial = img.color;
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
        for (int x = 0; x < 4; x++) _c[x] = Set.UnitInTime(_c[x], color_want[x], scaleSpeed);
        img.color = _c;
    }

    /// <summary>
    /// Desactivas el objeto tras x tiempo
    /// </summary>
    private IEnumerator DisableOn(){
        yield return new WaitForSeconds(timeToDisable);
        gameObject.SetActive(false);
    }
    
    public void Refresh() => img.color = color_initial;
    public bool IsEnd() => img.color.Equals(color_want);
    #endregion
}

interface IImageController
{
    /// <summary>
    ///  Revisa si ha terminado
    /// </summary>
    bool IsEnd();

    /// <summary>
    /// Cargamos el color inicial como valor a buscar
    /// </summary>
    void Refresh();
}
