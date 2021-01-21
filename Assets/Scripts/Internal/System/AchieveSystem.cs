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

    [Header("Debug")]
    public bool _Debug_ShowUnlock = false;
    public bool _Debug_SetRandomAchieve = false;


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

#if DEBUG
        _Debug();
#endif
    }
    #endregion
    #region Methods

    /// <summary>
    /// Colocamos la referencia del item unlocker de esta pantalla
    /// </summary>
    ///= default
    public static void SetUnlockItem(AchievementItem item){
        Debug.Log("Set item UNlock");
        //unlockShow = false;
        _.achieveUnlockItem = item;
        _.rect_unlockItem = _.achieveUnlockItem.GetComponent<RectTransform>();

        //Colocamos las variables en un inicio sin mostrarse
        _.rect_unlockItem.anchorMin.Set(0, 1);
        _.rect_unlockItem.anchorMax = new Vector2(1, 2);

    }

    /// <summary>
    /// Esconde o muestra la pantalla
    /// </summary>
    private void HideShowUnlock(){

        int minY = unlockShow ? 0 : 1;
        //int maxY = unlockShow ? 1 : 2;

        Vector2 newMin = new Vector2(0, minY);
        Vector2 newMax = new Vector2(1, 1);

        //me las retorna en 0-1
        newMin.y = XavHelpTo.Set.UnitInTime(rect_unlockItem.anchorMin.y, minY);

        rect_unlockItem.anchorMin = newMin;
        rect_unlockItem.anchorMax = newMin + newMax;
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

    #if DEBUG

    private void _Debug()
    {
        //si es true solamente
        if (_Debug_ShowUnlock)
        {
            _Debug_ShowUnlock = false;
            unlockShow = !unlockShow;
        }

        if (_Debug_SetRandomAchieve)
        {
            _Debug_SetRandomAchieve = false;
            Setitem(XavHelpTo.Get.ZeroMax(10), achieveUnlockItem);
        }
    }
    #endif
    #endregion
}