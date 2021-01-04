#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Achievements;
using Key;
using Environment;
using Translate;
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

       
        //TODO esto va en opciones.... ?
        //la cadencia de cada letra puesta
        public readonly float[] textSpeed = { 0, .005f, .015f, .025f };

        //

        //Extra
        [Header("Datos de TranslateData")]
        private readonly TranslateData translate = new TranslateData();

        [Header("Datos de AchievementData")]
        private readonly AchievementData achievement = new AchievementData();

        [Header("Datos de las Llaves")]
        private readonly KeyData keyData = new KeyData();

        public Achievement[] GetAchievements () => achievement._GetAllAchievements();
        public Key.Key[] GetKeys() => keyData._GetAllKeys();

        /// <summary>
        /// Obtenemos el <see cref="Language"/> guardado en <see cref="DataPass"/>
        /// <para>También se puede especificar el <see cref="Idiom"/> que queremos buscar</para>
        /// </summary>
        public static Language Translated(Idiom idiom) => data.translate.Get(idiom);
        public static Language Translated(int i = -1) => Translated((Idiom)(i != -1 ? i : DataPass.GetSavedData().idiom));
        public static int GetLangLength() => data.translate.GetLangLength();

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
        OptionsScene, // las opciones de juego para modificarle al jugador
        GameScene,
    }
    /// <summary>
    /// Son las llaves que posee el jugador en este proyecto
    /// </summary>
    public enum KeyPlayer
    {
        NO = -1,

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



