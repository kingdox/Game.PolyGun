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
    public static int achievementLenght;

    //Donde controlaremos lo que meustra
    //un manager o el unlocked se asignaa el achieveUnlock para que sepa cual tomar
    public AchievementItem achieveUnlockItem;

    #endregion
    #region Events
    private void Awake()
    {
        //Singleton corroboration
        if (_ == null)
        {
            DontDestroyOnLoad(gameObject);
            _ = this;
        }
        else if (_ != this)
        {
            Destroy(gameObject);
        }

        achievementLenght = achievements.Length;

    }
#endregion
#region Methods


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