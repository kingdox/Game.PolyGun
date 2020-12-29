﻿#region imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
namespace Achievements
{
    #region AchievementIndex
    public enum AchievementIndex
    {
        // TODO aqui hay algo no optimo
        //Estos valores representan el ordenamiento de los datos guardados ?
        TOTAL_KILLS_ROBOT = 0,
        TOTAL_COLLECT_OBJ = 1,
        TOTAL_HEAL = 2,
        TOTAL_BOUNDSLIFE = 3,

        RECORD_KILLS_ROBOT = 4
    }
    #endregion
    #region class AchievementData
    /// <summary>
    /// Contenedor de la información de los logros
    /// aquí solo podrás extraer datos
    /// </summary>
    public class AchievementData
    {
        public static readonly Color[] colorSteps =
        {
            new Color(0.8f, 0.5f, 0.2f),
            new Color(0.75f, 0.75f, 0.75f),
            new Color(0.83f, 0.68f, 0.21f),
        };

        //Currificación Achievement 
        // 1 => Reporto los ingredientes de la receta
        private delegate Achievement AchievementRecipe(string title, Limit limit);
        // 2 => Declaro la preparación de la receta
        private readonly static AchievementRecipe achieve = (string title, Limit limit) => new Achievement(title, new Limit(limit));
        //CURRY END

        //Currificación Limit
        private delegate Limit LimitRecipe(float bronze, float silver, float gold);
        private readonly static LimitRecipe limit = (float bronze, float silver, float gold) => new Limit(bronze, silver, gold);
        //CURRY END
        

        private static readonly Achievement[] achievements;

        /// <summary>
        /// Constructor, Crea los achievements
        /// <para>este HACK debe llamarse una sola vez</para>
        /// </summary>
        static AchievementData()
        {
            achievements = new Achievement[]
            {
                //Pagina 1
                achieve("Robot eliminados en total", limit(5,250,500)),
                //achieve("Tiempo total jugando",new Limit(100,250,500)),
                achieve("Objetos recogidos en total", limit(250,500,1000)),
                achieve("Cantidad total de Curaciones", limit(50,100,500)),
                achieve("Tiempo total al borde de morir", limit(50,100,500)),
                achieve("Robot eliminados en una partida", limit(10,50,100)),

                //Pagina 2
                achieve("Testeo bien perrón", limit(2,5,10)),
                achieve("Testeo bien perrón", limit(2,5,10)),
                achieve("Testeo bien perrón", limit(2,5,10)),
                achieve("Testeo bien perrón", limit(2,5,10)),
                achieve("Testeo bien perrón", limit(2,5,10)),

                //Pagina 3
                achieve("Testeo bien perrón", limit(2,5,10)),
                achieve("Testeo bien perrón", limit(2,5,10)),


            };
        }

        /// <summary>
        /// Tomas los achievements del juego
        /// </summary>
        public Achievement[] _GetAllAchievements() => achievements;
    }
    #endregion
    #region AchievementPages
    [System.Serializable]
    public struct AchievementPages
    {
        //tambien podias hacer un matrix, pero esto es mas facil en inspector...
        public AchievementItem[] items;
    }
    #endregion
    #region Achievement
    /// <summary>
    /// Modelo de los logros
    /// <para>Dependencia con <seealso cref="Limit"/></para>
    /// </summary>
    public struct Achievement
    {
        public string title;
        public Limit limit;
        public Achievement(string title, Limit limit)
        {
            this.title = title;
            this.limit = limit;
        }
    }
    #endregion
    #region TextValBarItem
    /// <summary>
    /// Modelo de los items de los logros
    /// <para>Dependencia con <seealso cref="Limit"/>, <seealso cref="TextValBarItem"/></para>
    /// </summary>
    public struct TextValBarItem
    {
        public string title;
        public Limit limit;
        public float value;

        /// <summary>
        /// Asigna el titulo el limite y el valor que posee el player sobre ese logro
        /// </summary>
        public TextValBarItem(string title, Limit limit, float value)
        {
            this.title = title;
            this.limit = limit;
            this.value = value;
        }
        //public (int, int, int) limits;
    }
    #endregion
    #region Limit
    /// <summary>
    /// Muestra los valores de cada etapa limite
    /// <para>Nota: Hay partes sin uso, solo son para aprendizaje</para>
    /// </summary>
    public struct Limit
    {
        public float bronze, silver, gold;
        /// <summary>
        /// Construye el limite con los valores asignados
        /// </summary>
        public Limit(float bronze, float silver, float gold)
        {
            this.bronze = bronze;
            this.silver = silver;
            this.gold = gold;
        }
        /// <summary>
        /// Usas un limit para asignar un limit
        /// </summary>
        public Limit(Limit limits) => this = limits;
        /// <summary>
        /// Usas un arreglo de 3 valores para asignar un limit
        /// </summary>
        public Limit(float[] limits) => this = new Limit(limits[0], limits[1], limits[2]);
        /// <summary>
        /// Devuelve el Limit en un arreglo
        /// </summary>
        public float[] ToArray() => new float[] { bronze, silver, gold };
        /// <summary>
        /// Devuelve el valor especificado del limit
        /// </summary>
        private float Get(int i) => ToArray()[i];
        /// <summary>
        /// Asigna el nuevo valor al limit
        /// </summary>
        private float Set(int i, float val)
        {
            float[] _newLimit = ToArray();
            _newLimit[i] = val;
            bronze = _newLimit[0];
            silver = _newLimit[1];
            gold = _newLimit[2];
            return val;
        }
        /// <summary>
        /// Se crea un get set por indice
        /// </summary>
        public float this[int index]
        {
            get => Get(index);
            set => Set(index, value);
        }
    }
    #endregion
}