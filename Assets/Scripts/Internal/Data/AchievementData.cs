#region imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Translate;
using XavLib;
#endregion
namespace Achievements
{
    #region class AchievementData
    /// <summary>
    /// Contenedor de la información de los logros
    /// aquí solo podrás extraer datos
    /// </summary>
    public class AchievementData
    {
        //Pose las posiciones los logros, esta debe ir acorde a la cantidad
        //y no salirse de los limites

        public static readonly Color[] colorSteps =
        {
            new Color(0.8f, 0.5f, 0.2f), // broncze
            new Color(0.75f, 0.75f, 0.75f), // silver
            new Color(0.83f, 0.68f, 0.21f), // gold fachero
        };

        //START Achievement 
        // 1 => Reporto los ingredientes de la receta
        private delegate Achievement AchievementRecipe(TKey key, Limit limit);
        // 2 => Declaro la preparación de la receta
        private readonly static AchievementRecipe achieve = (TKey key, Limit limit) => new Achievement(key, new Limit(limit));
        //CURRY END

        //START Limit
        private delegate Limit LimitRecipe(params float[] limits);
        private readonly static LimitRecipe limit = (float[] limits) => new Limit(limits);
        //CURRY END
        

        private static readonly Achievement[] achievements;

        /// <summary>
        /// Constructor, Crea los achievements
        /// <para>este HACK debe llamarse una sola vez</para>
        /// </summary>
        static AchievementData()
        {
            //TODO balancear los limites de cada uno
            achievements = new Achievement[]
            {
                //Pagina 1
                achieve(TKey.ACHIEVE_KILLS_ROBOT, limit(30,100,300)),
                achieve(TKey.ACHIEVE_KILLS_BOSS, limit(5,25,50)),
                achieve(TKey.ACHIEVE_WAVES_ENEMIES, limit(3,15,30)),
                achieve(TKey.ACHIEVE_OBJECTS_COLLECTED, limit(50,100,300)),
                achieve(TKey.ACHIEVE_HEALS_GAME, limit(30,120,300)),

                //Pagina 2 
                achieve(TKey.ACHIEVE_TIME_DEATHLIMIT, limit(5,20,60)),
                achieve(TKey.ACHIEVE_METTERS_GAME, limit(2,5,10)),
                achieve(TKey.ACHIEVE_CREATIONS_GAME, limit(2,5,10)),
                achieve(TKey.ACHIEVE_ROBOTS_ALIVE, limit(2,5,10)),
                achieve(TKey.ACHIEVE_ESPECIAL_READ, limit(2,5,20)),

            };
        }

        /// <summary>
        /// Tomas los achievements del juego
        /// </summary>
        public Achievement[] _GetAllAchievements() => achievements;

    }
    #endregion
    #region Achievement
    /// <summary>
    /// Modelo de los logros
    /// <para>Dependencia con <seealso cref="Limit"/> y <seealso cref="TKey"/></para>
    /// </summary>
    public struct Achievement
    {
        public TKey key;
        public Limit limit;
        public Achievement(TKey key, Limit limit)
        {
            this.key = key;
            this.limit = limit;
        }
    }
    #endregion
    #region TextValBarItem
    /// <summary>
    /// Modelo de los items de los logros
    /// <para>Dependencia con <seealso cref="Limit"/>, <seealso cref="TKey"/></para>
    /// </summary>
    public struct TextValBarItem
    {
        public TKey key;
        public Limit limit;
        public float value;

        /// <summary>
        /// Muestra el limite alcanzado basado en los valores proporcionados
        /// </summary>
        public int LimitReached => XavHelpTo.KnowFirstMajorIndex(value, limit.ToArray());

        public string TextValue => $" {value} {ShowLimiters} ";

        private string ShowLimiters => value > limit.gold ? "" : "/ " + limit[LimitReached];

        /// <summary>
        /// Asigna el titulo el limite y el valor que posee el player sobre ese logro
        /// </summary>
        public TextValBarItem(TKey key, Limit limit, float value)
        {
            this.key = key;
            this.limit = limit;
            this.value = value;
        }
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