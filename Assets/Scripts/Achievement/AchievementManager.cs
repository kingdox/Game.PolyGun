#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Achievements;
#endregion
#region class AchievementManager
public class AchievementManager : MonoBehaviour
{
    #region Variables
    private readonly Achievement[] achievements = Data.data.achievement._GetAllAchievements();
    [Header("Settings")]
    //Donde poseeremos en orden los items de logros
    //public AchievementItem[] items;

    [Space]
    public AchievementPages[] pages;

    private bool init = false;

    private int itemsAssigned = 0;

    #endregion
    #region Events
    private void Awake(){
        init = false;
    }
    private void Update(){

        //Inicializamos Los valores
        if (!init && DataPass.IsReady()) Init();

    }
    #endregion
    #region Methods

    /// <summary>
    /// Inicializamos el achievementManager con los datos de datapass
    /// </summary>
    private void Init(){
        Debug.Log("Loading....");
        init = true;
        itemsAssigned = 0;
        CheckAchievementSaved();
        for (int i = 0; i < pages.Length; i++) AssignAchievementItem(i);
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
    }

    /// <summary>
    /// Asignamos a cada item de achievement sus datos,
    /// en caso de encontrar problemas lo desactiva (suponiendo que no fuese exacto)
    /// </summary>
    private void AssignAchievementItem(int pageIndex)
    {
        //actualizamos el tamaño del arreglo con el estado actual
        float[] _savedAchievement = DataPass.GetSavedData().achievements;

        AchievementItem[] items = pages[pageIndex].items;

        //Recorremos los items del UI y pintamos tantos como podamos del achievement
        for (int x = 0; x < items.Length; x++)
        {

            int itemIndex = itemsAssigned + x;

            //si existe el item no sale de los limites de los achievements
            bool condition = XavHelpTo.IsOnBoundsArr(itemIndex, achievements.Length);

            //muestra o esconde en caso de formar parte o no
            XavHelpTo.ObjOnOff(items[x].gameObject, condition);

            //Si está dentro de los limites entonces hace el pintado
            if (condition){

                //Se asigna los datos del titulo, el limite y el valor guardado
                items[x].SetItem(new TextValBarItem(
                    achievements[itemIndex].title,
                    achievements[itemIndex].limit,
                    _savedAchievement[itemIndex]
                ));

            }

            
        }

        itemsAssigned += items.Length;
    }



    #endregion
}
#endregion
