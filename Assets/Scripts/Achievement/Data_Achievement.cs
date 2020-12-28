#region imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
namespace Achievement
{
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
    #region AchievementModel
    /// <summary>
    /// Modelo de los logros
    /// <para>Dependencia con <seealso cref="Limit"/></para>
    /// </summary>
    public struct AchievementModel
    {
        public string title;
        public Limit limit;
        public AchievementModel(string title, Limit limit)
        {
            this.title = title;
            this.limit = limit;
        }
    }
    #endregion
    #region TextValBarItem
    /// <summary>
    /// Modelo de los items de los logros
    /// <para>Dependencia con <seealso cref="Limit"/></para>
    /// </summary>
    public struct TextValBarItem
    {
        public string title;
        public int value;
        public Limit limit;
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