using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Environment;
//using System;
namespace XavLib
{
    /// <summary>
    /// Herramientas para facilitar codigo
    /// <para>Aquí se poseerán funciones unicamente "static"</para>
    /// <see cref="XavHelpTo"/> Ultima Actualización => 3 ene 2021
    /// </summary>
    public struct XavHelpTo
    {
        #region Get
        /// <summary>
        /// Herramienta de obtención de valores basados en parametros otorgados
        /// </summary>
        public struct Get{

            /// <summary>
            /// Devuelve el ancho del porcentaje para conocerlo basado en la pantalla
            /// <para>Usa <seealso cref="KnowPercentOfMax(float, float)"/></para>
            /// </summary>
            public static float WidthOf(float w) => QtyOf(w, Screen.width);

            /// <summary>
            /// Devuelve el ancho del porcentaje para conocerlo basado en la pantalla
            /// <para>Usa <seealso cref="KnowPercentOfMax(float, float)"/></para>
            /// </summary>
            public static float HeightOf(float h) => QtyOf(h, Screen.height);

            /// <summary>
            /// Devuelve el ancho y alto del vector de porcentaje basado enla pantalla
            /// <para>Usa <seealso cref="GetWidthOf(float)"/> y <seealso cref="GetHeightOf(float)"/></para>
            /// </summary>
            public static Vector2 SizeOf(Vector2 s) => new Vector2(WidthOf(s.x), HeightOf(s.y));

            /// <summary>
            /// sacas el alto de una camara o la camara activa por defecto
            /// </summary>
            /// <para>Dependencia con <seealso cref="Camera"/> </para>
            /// <returns>el alto de <seealso cref="Camera"/> en unidades de Unity</returns>
            public static float ScreenHeightUnit(Camera camera = null) => camera ? camera.orthographicSize * 2f : Camera.main.orthographicSize * 2f;

            /// <summary>
            /// Sacas el ancho de la pantalla basado en el alto de la camara 
            /// <para>Dependencia con <seealso cref="Screen"/> </para>
            /// </summary>
            /// <returns>el Ancho de <seealso cref="Camera"/> en unidades Unity</returns>
            public static float ScreenWidthUnit(float camHeight) => camHeight * (Screen.width / Screen.height);


            #region KnowPercentOfMax //-> Saca el porcentaje del valor y el valor maximo
            /// <summary>
            ///  Saca el porcentaje de la cantidad y el maximo en caso de tener
            /// </summary>
            /// <returns>El porcentaje de count sobre el max</returns>
            public static float PercentOf(float count, float max) => count / max * 100;
            public static Vector2 PercentOf(Vector2 count, Vector2 max) => count / max * 100;
            #endregion
            #region KnowQtyOfPercent //-> Saca el valor mediante el porcentaje y el valor maximo 
            /// <summary>
            /// Basado en el porcentaje obtienes el valor mediante un maximo establecido
            /// </summary>
            public static float QtyOf(float percent, float max) => (max / 100) * percent;
            public static Vector2 QtyOf(Vector2 percent, Vector2 max) => (max / 100) * percent;
            #endregion
            /// <summary>
            /// Obtienes el valor del rango dado 
            /// </summary>
            /// <param name="range"></param>
            public static float Range(float[] range) => Random.Range(range[0], range[1]);
            public static int Range(int[] range) => Random.Range(range[0], range[1]);

            /// <summary>
            /// Tomas el valor entre el 0 y el maximo
            /// </summary>
            /// <returns></returns>
            public static int ZeroMax(int max) => Random.Range(0, max);

        }
        #endregion
        #region Set
        /// <summary>
        /// Herramienta para modificación del valor,devolviendo los cambios hechos al valor
        /// </summary>
        public struct Set
        {
            /// <summary>
            /// Asignas el valor a positivo en caso de ser negativo
            /// </summary>
            public static float Positive(float f) => f < 0 ? f * -1 : f;
            /// <summary>
            /// Obtenemos el valor dentro de los limites de la unidad de 0 y 1
            /// <para>tambien puede psoeer decimales</para>
            /// </summary>
            public static float InUnitBounds(float v) => Mathf.Clamp(v, 0, 1);

            /// <summary>
            /// Llenamos un arreglo con la condición
            /// </summary>
            //public static bool[] Bools(int lenght, bool condition = false)
            //{
            //    bool[] bools = new bool[lenght];
            //    for (int x = 0; x < lenght; x++) bools[x] = condition;
            //    return bools;
            //}

            /// <summary>
            /// Buscamos el parametro del <see cref="Color"/> que vas a cambiar
            /// <para>  el parametro debe estar entre los rangos de los parametros de color</para>
            /// <para>  [R == 0,G == 1,B == 2,A == 3] --> iniciando en 0.</para>
            /// <para>   Si i es == -1 entonces aplica a (RGB)</para>
            /// <para>Dependencia con <seealso cref="Color"/> </para>
            /// </summary>
            /// <returns>Devuelve el <see cref="Color"/> con los cambios</returns>
            public static Color ColorParam(Color c, int i, float val = 1)
            {
                //Si esta fuera de los limites del arreglo
                if (!Know.IsOnBounds(i, 4))
                {
                    if (i == -1)
                    {
                        for (int x = 0; x < 3; x++) c[x] = val;
                    }
                    else
                    {
                        Debug.LogError($"Indice errado ?, favor usar un enum de parametros de color o usarlo bien :(");
                    }
                }
                else c[i] = val;

                return c;
            }
            /// <summary>
            /// Actualizamos el arreglo para que posea el mismo tamaño que el nuevo,
            /// estos cambios pueden eliminar o añadir huecos, los nuevos iniciarán en 0
            /// </summary>
            public static float[] Length(float[] oldArr, int newLength)
            {

                //Si es igual no se hace nada
                if (oldArr.Length == newLength) return oldArr;


                //revisamos quién es mas grande
                bool condition = oldArr.Length > newLength;

                float[] newArr = new float[condition
                    ? (oldArr.Length - newLength)
                    : (newLength)
                ];


                //si hay menos datos llenamos el nuevo arreglo
                //Puede que perdamos algunos valores
                if (condition)
                {
                    for (int z = 0; z < newArr.Length; z++) newArr[z] = oldArr[z];
                }
                else
                {
                    for (int z = 0; z < newArr.Length; z++)
                    {
                        newArr[z] = z < oldArr.Length - 1
                            ? oldArr[z]
                            : 0
                        ;
                    }
                }

                return newArr;
            }
            /// <summary>
            /// Hacemos que el valor cambio dentro de un margen de tiempo,
            /// <para>
            /// si este valor se sobrepasa entre 0 y 1 los ajusta
            /// </para>
            /// </summary>
            public static float UnitInTime(float value, float toMax, float timeScale = 1) => Set.InUnitBounds(Mathf.MoveTowards(value, toMax, Time.deltaTime * timeScale * Set.Positive(toMax - value)));
        }
        #endregion
        #region Change
        /// <summary>
        /// Herramienta para la alteración de cosas
        /// </summary>
        public struct Change{
            /// <summary>
            /// Cambiamos a la escena indicada en numerico
            /// </summary>
            /// <param name="index"></param>
            public static void SceneTo(int index) => SceneManager.LoadScene(index);
            public static void SceneTo(string name) => SceneManager.LoadScene(name);
            /// <summary>
            /// Activa o desactiva el <seealso cref="GameObject"/> basado en una condición
            /// <para>Dependencia con <seealso cref="GameObject"/> </para>
            /// </summary>
            public static void ActiveObject(GameObject obj, bool condition) => obj.SetActive(condition);
            /// <summary>
            /// Activa unicamente el objeto indicado del arreglo
            /// <para>Por defecto el indice es el primero del arreglo</para>
            /// <para>Dependencia con <seealso cref="ObjOnOff(GameObject, bool)"/> </para>
            /// </summary>
            public static void ActiveObjectsExcept(GameObject[] arr, int index = 0) { for (int x = 0; x < arr.Length; x++) ActiveObject(arr[x], x == index); }
            /// <summary>
            /// Dependiendo de la condición determinamos si iniciar o apagar la animación
            /// <para>Dependencia con <seealso cref="ParticleSystem"/> </para>
            /// </summary>
            public static void ActiveParticle(ParticleSystem particle, bool condition)
            {
                if (condition && particle.isStopped) particle.Play();
                else if (!condition && particle.isPlaying) particle.Stop();
            }

        }
        #endregion
        #region Know
        /// <summary>
        /// Herramienta que devuelve valores booleanas o de indexación
        /// </summary>
        public struct Know
        {
            /// <summary>
            /// Busca cual es el valor del arreglo que supera al indicado
            /// <para>Retorna -1 si no encuentra alguno mayor que el mostrado</para>
            /// </summary>
            public static int FirstMajor(float val, float[] arr){
                for (int x = 0; x < arr.Length; x++) if (val < arr[x]) return x;
                return -1;
            }

            /// <summary>
            /// Devuelve el nombre de la escena activa
            /// <para>Dependencia con <seealso cref="Scenes"/> </para>
            /// </summary>
            public static Scenes ActiveScene() => (Scenes)SceneManager.GetActiveScene().buildIndex;

            /// <summary>
            /// Conoces el siguiente indice basado en la longitud del arreglo
            /// <para>Se le puede definir un inicio en caso de haber</para>
            /// </summary>
            public static int NextIndex(bool goNext, int indexLength, int index = 0) => goNext? (index == indexLength - 1 ? 0 : index + 1): (index == 0 ? indexLength - 1 : index - 1);
            /// <summary>
            /// Detecta si el indice está dentro del arreglo
            /// </summary>
            public static bool IsOnBounds(int i, int length) => i == Mathf.Clamp(i, 0, length - 1);
            /// <summary>
            /// Retorna un valor distinto al ultimo suponiendo que la dimension es mayor a 1
            /// </summary>
            /// <param name="lastInt"></param>
            /// <param name="max"></param>
            /// <returns></returns>
            public static int DifferentIndex(int max, int lastInt = -1)
            {
                int _newInt = lastInt;

                while (lastInt == _newInt && max > 1)
                {
                    _newInt = Get.ZeroMax(max);
                }

                return _newInt;
            }
        }
        #endregion
    }
}
#region Committed
//public static int[] GetIntsOnArray(int qty)
//{
//    int[] ints = new int[qty];
//    for (int x = 0; x < qty; x++) ints[x] = x;
//    return ints;
//}
#endregion