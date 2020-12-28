using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



/// <summary>
/// Representa los datos basicos del enviroment
/// </summary>
public class Data 
{
    [HideInInspector]
    public static Data data = new Data();

    public readonly string savedPath = "saved.txt";
    public readonly string version = "v0.0";
}


/// <summary>
/// Herramientas para facilitar codigo
/// <para>Aquí se poseerán funciones unicamente "static"</para>
/// <see cref="XavHelpTo"/> Ultima Actualización => 23 dic 2020
/// </summary>
public struct XavHelpTo
{

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

    /// <summary>
    /// Cambiamos a la escena indicada en numerico
    /// </summary>
    /// <param name="index"></param>
    public static void ChangeSceneTo(int index) => SceneManager.LoadScene(index);
    /// <summary>
    /// Cambiamos a la escena indicada con el nombre
    /// </summary>
    /// <param name="name"></param>
    public static void ChangeSceneTo(string name) => SceneManager.LoadScene(name);
    /// <summary>
    /// Cambiamos a la escena indicada con el nombre
    /// </summary>
    /// <param name="name"></param>
    //public static void ChangeSceneTo(dynamic n) => SceneManager.LoadScene(n);

    /// <summary>
    /// Devuelve el nombre de la escena activa
    /// <para>Dependencia con <seealso cref="Scenes"/> </para>
    /// </summary>
    public static Scenes ActiveScene() => (Scenes)SceneManager.GetActiveScene().buildIndex;

    /// <summary>
    ///  Saca el porcentaje de la cantidad y el maximo en caso de tener
    /// </summary>
    /// <param name="count"></param>
    /// <param name="Max"></param>
    /// <returns>El porcentaje de count sobre el max</returns>
    public static float KnowPercentOfMax(float count, float max) => count / max * 100;

    /// <summary>
    /// Basado en el porcentaje
    /// //obtienes el porcentaje de curacion basado en tu max
    /// 
    /// </summary>
    /// <param name="percent"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float KnowQtyOfPercent(float percent, float max) => (max / 100) * percent;

    /// <summary>
    /// Detecta si el indice está dentro del arreglo
    /// </summary>
    /// <param name="i"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static bool IsOnBoundsArr(int i, int length) => i == Mathf.Clamp(i, 0, length - 1);

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
                for (int x = 0; x < 3; x++)
                {
                    _c[x] = val;
                }

            }
            else {
                Debug.LogError($"Indice errado ?, favor usar un enum de parametros de color o usarlo bien :(");
            }
        }
        else
        {
            _c[i] = val;
        }

        Color newColor = new Color(_c[0], _c[1], _c[2], _c[3]);
        return newColor;
    }


    /// <summary>
    /// Dependiendo de la condición determinamos si iniciar o apagar la animación
    /// <para>Dependencia con <seealso cref="ParticleSystem"/> </para>
    /// </summary>
    public static void ParticlePlayStop(ParticleSystem particle, bool condition) {

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
    /// Obtienes el valor del rango dado 
    /// </summary>
    /// <param name="range"></param>
    public static float Range(float[] range) => Random.Range(range[0], range[1]);

    /// <summary>
    /// Obtienes el valor del rango dado 
    /// </summary>
    /// <param name="range"></param>
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
    public static int GetDifferentInt(int max, int lastInt = -1)  {
        int _newInt = lastInt;

        while (lastInt == _newInt && max > 1)
        {
            _newInt = ZeroMax(max);
        }
        return _newInt;
    }

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
}

/// <summary>
/// Identificador de los colores
/// </summary>
public enum ColorType{r,g,b,a}

/// <summary>
/// Enumerador de los nombres de las escenas de este proyecto
/// Estos se colocan manualmente...
/// </summary>
public enum Scenes
{
    GameScene
}