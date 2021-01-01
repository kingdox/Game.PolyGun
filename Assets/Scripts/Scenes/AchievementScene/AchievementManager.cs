#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using Achievements;
using XavLib;
using Environment;
#endregion
#region class AchievementManager
public class AchievementManager : MonoManager
{
    #region Variables
    private readonly Achievement[] achievements = Data.data.GetAchievements();


    [Header("Settings")]
    [Space]
    public AchievementItem[] items;
    public int index = 0;
    private int indexlimit = 0;

    [Space]
    [Header("Navigator Buttons")]
    public Button btn_L;
    public Button btn_R;

    #endregion
    #region Events
    private void Update(){

        //Revisa los controles
        CheckControl();
    }
    public override void Init(){
        indexlimit = GetLimitIndex();
        CheckAchievementSaved();
        AssignAchievementItem();
    }
    #endregion
    #region Methods
    /// <summary>
    /// Revisa los controles y dependiendo del presionado hará algo
    /// </summary>
    private void CheckControl()
    {
        btn_L.interactable = index != 0;
        btn_R.interactable = index != indexlimit;
        //Salir
        if (ControlSystem.KeyDown(KeyPlayer.OK_FIRE) || ControlSystem.KeyDown(KeyPlayer.BACK)) GoToScene(Scenes.MenuScene);
        //Navegación
        if (btn_R.interactable && ControlSystem.KeyDown(KeyPlayer.RIGHT)) MoveTo(true);
        else if (btn_L.interactable && ControlSystem.KeyDown(KeyPlayer.LEFT)) MoveTo(false);
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
            DataPass.SaveLoadFile(true);
        }
    }

    /// <summary>
    /// Asignamos a cada item de achievement sus datos,
    /// en caso de encontrar problemas lo desactiva (suponiendo que no fuese exacto)
    /// </summary>
    private void AssignAchievementItem()
    {
        int count = index;

        //Recorremos los items del UI y pintamos tantos como podamos del achievement
        for (int x = 0; x < items.Length; x++)
        {
            //si existe el item no sale de los limites de los achievements
            bool condition = XavHelpTo.IsOnBoundsArr(count, achievements.Length);

            //muestra o esconde en caso de formar parte o no
            XavHelpTo.ObjOnOff(items[x].gameObject, condition);

            //Si está dentro de los limites entonces hace el pintado
            if (condition){

                //Se asigna los datos del titulo, el limite y el valor guardado
                items[x].SetItem(new TextValBarItem(
                    achievements[count].title,
                    achievements[count].limit,
                    DataPass.GetSavedData().achievements[count]
                ));
            }


            count++;
        }
    }


    /// <summary>
    /// Nos moveremos hacia adelante o hacia atras
    /// al hacerlo cargará los nuevos achivements
    /// </summary>
    /// <param name="goForward"></param>
    public void MoveTo( bool goForward){

        int distance = goForward ? items.Length : -items.Length;
        //el resultado
        int _newIndex = index + distance;

        //si no se ha salido asignamos el nuevo index
        if (XavHelpTo.IsOnBoundsArr(_newIndex, achievements.Length)) index = _newIndex;
        else index = _newIndex < 0 ? indexlimit : 0;
        AssignAchievementItem();
    }


    /// <summary>
    /// Conocemos el limite del index que contenga la misma distancia sin perder
    /// el ritmo de los bloques
    /// </summary>
    private int GetLimitIndex(){
        int newIndex = 0;
        int distance = items.Length;
        while (achievements.Length > newIndex) newIndex = distance + newIndex;
        return newIndex - distance;
    }

    #endregion
}
#endregion
