#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Achievement;
#endregion
#region class AchievementManager
public class AchievementManager : MonoBehaviour
{

    public AchievementData s = new AchievementData();

    private void Start()
    {
        //s._GetAllAchievements();
        //string ss = s.GetAchievements()[1].title;
        //Debug.Log(ss);
    }
    //TODO poder controlar las pantallas..
    //crearse un manager de playercontroller standalone ??

}
#endregion
#region class AchievementData
/// <summary>
/// Contenedor de la información de los logros
/// aquí solo podrás extraer datos
/// </summary>
public class AchievementData
{
    //Currificación START 
    // 1 => Reporto los ingredientes de la receta
    private delegate AchievementModel Recipe(string title, Limit limit);
    // 2 => Declaro la preparación de la receta
    private readonly static Recipe achieve = (string title, Limit limit) => new AchievementModel(title, new Limit(limit));
    //CURRY END

    private static readonly AchievementModel[] achievements;

    /// <summary>
    /// Constructor, Crea los achievements
    /// <para>este HACK debe llamarse una sola vez</para>
    /// </summary>
    static AchievementData()
    {
        achievements = new AchievementModel[]
        {
            achieve("Robot eliminados en una partida",new Limit(10,50,100)),
            achieve("Robot eliminados en total",new Limit(100,250,500)),


        };

        Debug.Log(achievements[0].title);
    }

    /// <summary>
    /// Tomas los achievements del juego
    /// </summary>
    public AchievementModel[] _GetAllAchievements() => achievements;
}
#endregion
#region Model Achievement

#endregion