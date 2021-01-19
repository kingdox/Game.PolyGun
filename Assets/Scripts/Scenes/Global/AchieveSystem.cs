#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Achievements;
using XavLib;
using Environment;
#endregion
public class AchieveSystem : MonoBehaviour
{
    #region Variables
    private readonly Achievement[] achievements = Data.data.GetAchievements();
    private static AchieveSystem _;


    [Header("Achieve Unlock Settings")]
    //Donde controlaremos lo que meustra
    //un manager o el unlocked se asignaa el achieveUnlock para que sepa cual tomar
    private AchievementItem achieveUnlockItem;
    private RectTransform rect_unlockItem;


    public static int achievementLenght;
    public static bool unlockShow = false;

    #endregion
    #region Events
    private void Awake()
    {
        //Singleton corroboration
        if (_ == null){
            DontDestroyOnLoad(gameObject);
            _ = this;
        }else if (_ != this){ Destroy(gameObject);}

        achievementLenght = achievements.Length;

    }
    private void Update()
    {
        if (_.achieveUnlockItem != null)
        {
            HideShowUnlock();
        }
    }
    #endregion
    #region Methods

    /// <summary>
    /// Colocamos la referencia del item unlocker de esta pantalla
    /// </summary>
    /// <param name="item"></param>
    public static void SetUnlockItem(AchievementItem item){
        unlockShow = false;
        _.achieveUnlockItem = item;
        _.rect_unlockItem = _.achieveUnlockItem.GetComponent<RectTransform>();

        //Colocamos las variables en un inicio sin mostrarse
        _.rect_unlockItem.anchorMin.Set(0, 1);
        _.rect_unlockItem.anchorMin = new Vector2(1, 2);

    }

    /// <summary>
    /// Esconde o muestra la pantalla
    /// </summary>
    private void HideShowUnlock(){

        int minY = unlockShow ? 0 : 1;
        int maxY = unlockShow ? 1 : 2;

        Vector2 newMin = new Vector2(0, minY);
        Vector2 newMax = new Vector2(1, maxY);

        //me las retorna en 0-1
        newMin.y = XavHelpTo.Set.UnitInTime(rect_unlockItem.anchorMin.y, minY, 1.5f);
        newMax.y = XavHelpTo.Set.UnitInTime(rect_unlockItem.anchorMax.y, maxY, 1.5f);

        //protejemos que se exceda...?
        newMin.y = Mathf.Clamp(newMin.y * 2 , 0, minY);
        newMax.y = Mathf.Clamp(newMax.y * 2 , 1, maxY);

        rect_unlockItem.anchorMin = newMin;
        rect_unlockItem.anchorMax = newMax;
    }
    /// <summary>
    /// Asigna al item los valores
    /// </summary>
    public static void Setitem(int index, AchievementItem item)
    {
        item.SetItem(new TextValBarItem(
            _.achievements[index].key,
            _.achievements[index].keyDesc,
            _.achievements[index].limit,
            DataPass.GetSavedData().achievements[index]
        ));
    }

    #endregion
}