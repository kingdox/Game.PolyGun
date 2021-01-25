#region
using UnityEngine;
using UnityEngine.UI;
using System;
#endregion

public class BuffHUD : MonoX
{
    #region
    [Header("BuffHUD settings")]
    public Image img_HUD_buff;
    public Text text_HUD_timeCount;
    [Space]

    private ItemBuff item;
    #endregion
    #region events
    private void Start()
    {
        Get(out item);
    }
    #endregion
    #region Methods

    /// <summary>
    /// Pinta el buff correspondiente
    /// </summary>
    public void Draw(Color color)
    {
        float count_hud = item.timer - item.count;
        text_HUD_timeCount.text = $"{Math.Round(count_hud, 1)} s";
        img_HUD_buff.color = color;
    }

    /// <summary>
    /// Limpia el HUD como si no hubiera....
    /// </summary>
    public void Clear(float disabledAlpha)
    {
        img_HUD_buff.color = new Color(1,1,1, disabledAlpha);
        text_HUD_timeCount.text = "";
    }

    /// <summary>
    /// Devuelve si esta ejecutando un buff o no
    /// </summary>
    public bool IsRunning() => item.isRunning;

    #endregion
}