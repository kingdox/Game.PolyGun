#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Achievements;
using Key;
using Translate;
using Crafts;
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

        public readonly string savedPath = "saved103.txt";
        public readonly string version = "v0.9.1";

        //TODO poner parametros de character con enums y arreglos de valores si hay tiempo...

        /// <summary>
        /// Cantidad que cura cada shape respectivo
        /// </summary>
        public readonly float[] healShape =
        {
            2f,
            3.5f,
            5.5f
        };

        //qué es mejor? un readonly o una constante?
        public const float ENEMY_PER_WAVE = 1.5f;
        //public readonly float ENEMY_PER_WAVE = 1.5f;

        public const float RECORD_ACHIEVE_TIMER = 5;

        public readonly float itemShapeRate = 0.80f; // %

        public readonly float bulletSpeed = 20f;

        public readonly float timeToCraft = 0.1f;

        public readonly float pctMinionDeathItem = .25f;

        [Header("Datos de CraftData")]
        private readonly CraftData craft = new CraftData();

        [Header("Datos de TranslateData")]
        private readonly TranslateData translate = new TranslateData();

        [Header("Datos de AchievementData")]
        private readonly AchievementData achievement = new AchievementData();

        [Header("Datos de las Llaves")]
        private readonly KeyData keyData = new KeyData();

        
        public Achievement[] GetAchievements () => achievement._GetAllAchievements();
        public Key.Key[] GetKeys() => keyData._GetAllKeys();


        /// <summary>
        /// Search in the craft data and try to match
        /// </summary>
        public CraftType SlotsMatch(ItemContent[] slots) => craft.MatchType(slots);

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
        InitialScene,//Pantalla inicial del lore...
        MenuScene, //el menu principal apra acceder a los demás sitios
        AchievementsScene, // Donde están los logros
        IntroductionScene, //donde sale el manual y eso
        GameScene, //Pantalla de juego

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

        SLOT_1,
        SLOT_2,
        SLOT_3
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



