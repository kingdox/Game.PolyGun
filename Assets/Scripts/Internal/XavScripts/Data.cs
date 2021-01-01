#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Achievements;
using Key;
using Environment;
#endregion

namespace Environment
{
    #region Environment
    /// <summary>
    /// Representa los datos basicos del enviroment
    /// </summary>
    public class Data 
    {
        [HideInInspector]
        public static Data data = new Data();

        public readonly string savedPath = "saved1.txt";
        public readonly string version = "v0.0.4";

        /*
         Opciones Scene posibles actualmente
            Traduccion español / inglés
            Velocidad de los textos (low,normal,speed)
            Musica(No, Bajo, Medio, Alto)
            Sfx(No, Bajo, Medio, Alto)

         */

        //Extra

        [Header("Datos de AchievementData")]
        private readonly AchievementData achievement = new AchievementData();

        [Header("Datos de las Llaves")]
        private readonly KeyData keyData = new KeyData();

        public Achievement[] GetAchievements () => achievement._GetAllAchievements();
        public Key.Key[] GetKeys() => keyData._GetAllKeys();

    }
    #endregion
    #region Enums
    /// <summary>
    /// Enumerador de los nombres de las escenas de este proyecto
    /// Estos se colocan manualmente...
    /// </summary>
    public enum Scenes
    {
        AchievementsScene, // Donde están los logros
        MenuScene, //el menu principal apra acceder a los demás sitios
        GameScene
    }
    /// <summary>
    /// Son las llaves que posee el jugador en este proyecto
    /// </summary>
    public enum KeyPlayer
    {
        BACK,
        OK_FIRE,

        LEFT,
        RIGHT,
        UP,
        DOWN,

        C, V, B
    }
    #endregion
}


/// <summary>
/// Identificador de los colores
/// es solo un facilitador...
/// </summary>
public enum ColorType
{
    r,
    g,
    b,
    a,
    RGB = -1
}



