#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Achievements;
#endregion
#region class AchievementManager
public class AchievementManager : MonoBehaviour
{
    //TODO esto es peligroso, solo se activará en logros?
    //qué ocurrirá si se nesecita  en otro sitio?, llevarlo a DATA?

    private Achievement[] achievements = Data.data.achievement._GetAllAchievements();


    private void Awake()
    {
            
    }
    private void Start()
    {
        //Achievement[] achievements = Data.data.achievementData._GetAllAchievements();

        //s._GetAllAchievements();
        //string ss = s.GetAchievements()[1].title;
        Debug.Log(achievements[0].title);
    }
    //TODO poder controlar las pantallas..
    //crearse un manager de playercontroller standalone ??

}
#endregion

#region Model Achievement

#endregion