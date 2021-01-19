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
    }
    #endregion
    #region Methods



    public static void Setitem(int index)
    {

        //achieveItem.SetItem(new TextValBarItem(

        )

    }

    #endregion
}