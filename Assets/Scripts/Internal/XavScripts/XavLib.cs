using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Environment;
//using System;

namespace XavLib
{
    #region Tools
    /// <summary>
    /// Herramientas para facilitar codigo
    /// <para>Aquí se poseerán funciones unicamente "static"</para>
    /// <see cref="XavHelpTo"/> Ultima Actualización => 29 dic 2020
    /// </summary>
    public struct XavHelpTo
    {
        /// <summary>
        /// Devuelve el ancho del porcentaje para conocerlo basado en la pantalla
        /// <para>Usa <seealso cref="KnowPercentOfMax(float, float)"/></para>
        /// </summary>
        public static float GetWidthOf(float w) => KnowQtyOfPercent(w, Screen.width);

        /// <summary>
        /// Devuelve el ancho del porcentaje para conocerlo basado en la pantalla
        /// <para>Usa <seealso cref="KnowPercentOfMax(float, float)"/></para>
        /// </summary>
        public static float GetHeightOf(float h) => KnowQtyOfPercent(h, Screen.height);

        /// <summary>
        /// Devuelve el ancho y alto del vector de porcentaje basado enla pantalla
        /// <para>Usa <seealso cref="GetWidthOf(float)"/> y <seealso cref="GetHeightOf(float)"/></para>
        /// </summary>
        public static Vector2 GetSizeOf(Vector2 s) => new Vector2(GetWidthOf(s.x), GetHeightOf(s.y));

        /// <summary>
        /// sacas el alto de una camara o la camara activa por defecto
        /// </summary>
        /// <para>Dependencia con <seealso cref="Camera"/> </para>
        /// <returns>el alto de <seealso cref="Camera"/> en unidades de Unity</returns>
        public static float GetScreenHeightUnit(Camera camera = null) => camera ? camera.orthographicSize * 2f : Camera.main.orthographicSize * 2f;

        /// <summary>
        /// Sacas el ancho de la pantalla basado en el alto de la camara 
        /// <para>Dependencia con <seealso cref="Screen"/> </para>
        /// </summary>
        /// <returns>el Ancho de <seealso cref="Camera"/> en unidades Unity</returns>
        public static float GetScreenWidthUnit(float camHeight) => camHeight * (Screen.width / Screen.height);

        /// <summary>
        /// Activa o desactiva el <seealso cref="GameObject"/> basado en una condición
        /// <para>Dependencia con <seealso cref="GameObject"/> </para>
        /// </summary>
        public static void ObjOnOff(GameObject obj, bool condition) => obj.SetActive(condition);

        /// <summary>
        /// Activa unicamente el objeto indicado del arreglo
        /// <para>Por defecto el indice es el primero del arreglo</para>
        /// <para>Dependencia con <seealso cref="ObjOnOff(GameObject, bool)"/> </para>
        /// </summary>
        public static void ActiveThisObject(GameObject[] arr, int index = 0) { for (int x = 0; x < arr.Length; x++) ObjOnOff(arr[x], x == index); }

        /// <summary>
        /// Busca cual es el valor del arreglo que supera al indicado
        /// <para>Retorna -1 si no encuentra alguno mayor que el mostrado</para>
        /// </summary>
        /// <returns>Devuelve el indice del superado</returns>
        public static int KnowFirstMajorIndex(float val, float[] arr)
        {
            for (int x = 0; x < arr.Length; x++) if (val < arr[x]) return x;

            //si index es -1 entonces ha sobrepasado el limite 
            return -1;
        }

        /// <summary>
        /// Te permite ir hacia adelante o hacia atrás en un arreglo sin salirte de los limites
        /// donde indexLength es el rango posible del index (arr.length - 1);
        /// _index es la pos actual en el arreglo, en caso de no haber es 0
        /// </summary>
        /// <returns>la nueva posición en el arreglo</returns>
        public static int TravelArr(bool goNext, int indexLength, int _index = 0)
        {
            int i = goNext
                ? (_index == indexLength - 1 ? 0 : _index + 1)
                : (_index == 0 ? indexLength - 1 : _index - 1)
            ;
            return i;
        }

        #region ChangeSceneTo //-> Cambias de escena a la que quieras
        /// <summary>
        /// Cambiamos a la escena indicada en numerico
        /// </summary>
        /// <param name="index"></param>
        public static void ChangeSceneTo(int index) => SceneManager.LoadScene(index);
        public static void ChangeSceneTo(string name) => SceneManager.LoadScene(name);
        //public static void ChangeSceneTo(T d) => SceneManager.LoadScene(name);
        
        #endregion

        #region ActiveScene //-> Devuelve al escena activa
        /// <summary>
        /// Devuelve el nombre de la escena activa
        /// <para>Dependencia con <seealso cref="Scenes"/> </para>
        /// </summary>
        public static Scenes ActiveScene() => (Scenes)SceneManager.GetActiveScene().buildIndex;
        #endregion

        #region KnowPercentOfMax //-> Saca el porcentaje del valor y el valor maximo
        /// <summary>
        ///  Saca el porcentaje de la cantidad y el maximo en caso de tener
        /// </summary>
        /// <returns>El porcentaje de count sobre el max</returns>
        public static float KnowPercentOfMax(float count, float max) => count / max * 100;
        public static Vector2 KnowPercentOfMax(Vector2 count, Vector2 max) => count / max * 100;
        #endregion

        #region KnowQtyOfPercent //-> Saca el valor mediante el porcentaje y el valor maximo 
        /// <summary>
        /// Basado en el porcentaje obtienes el valor mediante un maximo establecido
        /// </summary>
        public static float KnowQtyOfPercent(float percent, float max) => (max / 100) * percent;
        public static Vector2 KnowQtyOfPercent(Vector2 percent, Vector2 max) => (max / 100) * percent;
        #endregion

        #region IsOnBoundsArr //-> Revisa si el indice está dentro del arreglo
        /// <summary>
        /// Detecta si el indice está dentro del arreglo
        /// </summary>
        public static bool IsOnBoundsArr(int i, int length) => i == Mathf.Clamp(i, 0, length - 1);
        #endregion

        #region SetColorParam //-> Asigna un valor a un parametro del Color
        /// <summary>
        /// Buscamos el parametro del <see cref="Color"/> que vas a cambiar
        /// <para>  el parametro debe estar entre los rangos de los parametros de color</para>
        /// <para>  [R == 0,G == 1,B == 2,A == 3] --> iniciando en 0.</para>
        /// <para>   Si i es == -1 entonces aplica a (RGB)</para>
        /// <para>Dependencia con <seealso cref="Color"/> </para>
        /// </summary>
        /// <returns>Devuelve el <see cref="Color"/> con los cambios</returns>
        public static Color SetColorParam(Color c, int i, float val = 1)
        {
            float[] _c = { c.r, c.g, c.b, c.a };

            //(float r, float g, float b, float a) pp = (c.r, c.g, c.b, c.a);

            //Si esta fuera de los limites del arreglo
            if (!IsOnBoundsArr(i, _c.Length))
            {
                if (i == -1)
                {
                    for (int x = 0; x < 3; x++) _c[x] = val;
                }
                else
                {
                    Debug.LogError($"Indice errado ?, favor usar un enum de parametros de color o usarlo bien :(");
                }
            }
            else _c[i] = val;

            Color newColor = new Color(_c[0], _c[1], _c[2], _c[3]);
            return newColor;
        }
        #endregion

        /// <summary>
        /// Dependiendo de la condición determinamos si iniciar o apagar la animación
        /// <para>Dependencia con <seealso cref="ParticleSystem"/> </para>
        /// </summary>
        public static void ParticlePlayStop(ParticleSystem particle, bool condition)
        {

            if (condition && particle.isStopped)
            {
                particle.Play();
            }
            //si no estas con poder y esta corriendo las particulas las frenas
            else if (!condition && particle.isPlaying)
            {
                particle.Stop();
            }
        }

        /// <summary>
        /// Actualizamos el arreglo para que posea el mismo tamaño que el nuevo,
        /// estos cambios pueden eliminar o añadir huecos, los nuevos iniciarán en 0
        /// </summary>
        public static float[] UpdateLengthArray(float[] oldArr, int newLength)
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


        /// <summary>
        /// Retorna un valor distinto al ultimo suponiendo que la dimension es mayor a 1
        /// </summary>
        /// <param name="lastInt"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GetDifferentInt(int max, int lastInt = -1)
        {
            int _newInt = lastInt;

            while (lastInt == _newInt && max > 1)
            {
                _newInt = ZeroMax(max);
            }
            return _newInt;
        }

        /// <summary>
        /// Llenamos un arreglo con la condición
        /// </summary>
        public static bool[] FillWithBools(int lenght, bool condition = false)
        {
            bool[] bools = new bool[lenght];
            for (int x = 0; x < lenght; x++) bools[x] = condition;
            return bools;
        }

        // TEST LAB AREA


        /// <summary>
        /// Revisa el valor y en caso de poseer -1 que, en este caso
        /// significa "Por defecto" entonces mantiene el valor anterior
        /// [TODO ver si remover o si es util....]
        /// </summary>
        /// <param name="val"></param>
        /// <param name="lastVal"></param>
        /// <returns>Devuelve un nuevo valor o el anterior del contexto</returns>
        private static float SetOrLet(float val, float lastVal) => val == -1 ? lastVal : val;

        //private static bool Singleton(GameObject _, dynamic @this )
        //{
        //    if (_ == null)
        //    {
        //        DontDestroyOnLoad(gameObject);
        //        _ = this;
        //    }
        //    else if (_ != this)
        //    {
        //        Destroy(gameObject);
        //    }
        //}


        public static int[] GetIntsOnArray(int qty)
        {
            int[] ints = new int[qty];
            for (int x = 0; x < qty; x++) ints[x] = x;
            return ints;
        }

    }
    #endregion



}

public interface ISingletTest<in T>
{
    //Generic<float> LL = new Generic<float>();
    
}
//public T
//public static void ChangeSceneTo(T d) => SceneManager.LoadScene(name);