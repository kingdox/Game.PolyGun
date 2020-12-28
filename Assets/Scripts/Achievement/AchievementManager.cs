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

    private readonly Achievement[] achievements = Data.data.achievement._GetAllAchievements();

    [Header("Settings")]
    //Donde poseeremos en orden los items de logros
    public AchievementItem[] items;

    private void Awake()
    {
            
    }
    private void Start()
    {
        //Achievement[] achievements = Data.data.achievementData._GetAllAchievements();

        //s._GetAllAchievements();
        //string ss = s.GetAchievements()[1].title;
        //Debug.Log(achievements[0].title);
        CheckAchievementSaved();
        AssignAchievementItem();
    }

    /// <summary>
    /// Revisamos el estado de los logros y si su dimension ha cammbiado
    /// </summary>
    private void CheckAchievementSaved()
    {
        SavedData _saved = DataPass.GetSavedData();

        //Si se posee distancias distintas se actualiza
        if (_saved.achievements.Length != achievements.Length)
        {
            //Asignamos el cambio de dimensión y guardamos
            //Se recomienda limpiar los datos en caso de que haysa hecho muchos cambios...
            _saved.achievements = XavHelpTo.UpdateLengthArray(_saved.achievements, achievements.Length);
            DataPass.SetData(_saved);
        }
        //TODO hay un problema con los limites del array revisar luego
    }

    /// <summary>
    /// Asignamos a cada item de achievement sus datos,
    /// en caso de encontrar problemas lo desactiva (suponiendo que no fuese exacto)
    /// </summary>
    private void AssignAchievementItem()
    {
        //actualizamos el tamaño del arreglo con el estado actual
        float[] _savedAchievement = DataPass.GetSavedData().achievements; 

        for (int x = 0; x < items.Length; x++)
        {
            //si existe el item
            bool condition = XavHelpTo.IsOnBoundsArr(x, achievements.Length);

            XavHelpTo.ObjOnOff(items[x].gameObject, condition);

            //Si está dentro de los limites entonces hace el pintado
            if (condition)
            {
                //el 0 representa los datos guardados, TODO
                //Se asigna los datos
                items[x].SetItem(new TextValBarItem(achievements[x].title, achievements[x].limit,
                    //TODO hacer que use los index del achievement para que en caso de cambios esto no sea inestable
                    _savedAchievement[ (int)(AchievementIndex)x ] ));

            }
        }
    }

}
#endregion

#region Model Achievement

#endregion