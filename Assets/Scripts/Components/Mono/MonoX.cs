#region Imports
using UnityEngine;
using XavHelpTo.Look;
using XavHelpTo.Set;
#endregion
/// <summary>
/// MonoX poseerá todas las propiedades comunes para los objetos
/// para adiciones especiales y comunes
/// <para>
/// Util para herramientas...
/// </para>
/// </summary>
public abstract class MonoX : MonoBehaviour
{
    #region Variables-X
    #endregion
    #region Methods-X
    /// <summary>
    /// Tomamos el valor del mismo objeto y le asignamos el componente 
    /// </summary>
    public void Get<T>(out T t) => t = GetComponent<T>();

    /// <summary>
    /// Tomamos los componentes del mismo tipo del objeto
    /// </summary>
    public void Gets<T>(out T[] t) => t = GetComponents<T>();

    /// <summary>
    /// Buscamos de los hijos del target el que tenga la misma tag
    /// <para>sino devuelve falso </para>
    /// </summary>
    public bool TryGetChild(ref Transform t, Transform target, string tag)
    {
        bool finded = false;
        for (int i = 0; i < target.childCount; i++)
        {
            Transform child = target.GetChild(i);
            if (child.CompareTag(tag))
            {
                t = child;
                finded =  true;
                break;
            }
        }
        if (!finded)
        {
            t = null;
        }
        return finded;
    }
    /// <summary>
    /// Tomamos del arreglo los componentes hijos sin el componente actual
    /// <para>Este vendrá ordenadamente, su coste es alto</para>
    /// </summary>
    public void GetChilds<T>(out T[] t, Transform target = null){
        //if (target == null) target = transform;
        Default(ref target, transform);
        New(out t, target.childCount);
        for (int x = 0; x < target.childCount; x++){
            t[x] = target.GetChild(x).GetComponent<T>();
        }
    }


    /// <summary>
    /// Tomamos los hijos y este objeto en caso de incluir lo buscado
    /// </summary>
    public void GetInChilds<T>(out T[] t) => t = GetComponentsInChildren<T>();

    /// <summary>
    /// Añades el tipo al objeto y lo asignas
    /// </summary>
    public void Add<T>(out T t) {
        gameObject.AddComponent(typeof(T));
        Get(out t);
    }

    /// <summary>
    /// Añades el tipo al objeto y lo asignas, si hay uno solo lo asigna
    /// </summary>
    public void GetAdd<T>(ref T t)
    {
        //primero busca
        Get(out t);
        //si no encuentra añade
        if (IsNull(t))  
        {
            Add(out t);
        }
    }
    /// <summary>
    /// Pinta con un color el texto
    /// </summary>
    public static void PrintX(string txt) => print(Look.ColorPrint(txt, Look.RandomColor()));
    public static void PrintX<T>(T txt) => PrintX(txt.ToString());


    /// <summary>
    /// Creas una nueva dimension de arreglo del tipo que desees
    /// </summary>
    public void New<T>(out T[] t,int qty) => t = new T[qty];



    /// <summary>
    /// Hace el conteo y devuelve true cuando pasa una vuelta.
    /// <para>Se usa <see cref="XavHelpTo.Set.TimeCountOf(ref float, float) para aprovechar el codigo, y reducir texto"/></para>
    /// </summary>
    public bool Timer(ref float count,float timer) => Set.TimeCountOf(ref count, timer);


    /// <summary>
    /// Permite activar el flag "can"___ para poder volver a usarlo, este se mide por tiempo
    /// </summary>
    public bool CanPassedTime(ref bool flag, ref float count, float timer){
        if (!flag && Timer(ref count, timer)){
            flag = true;
        }   
        return flag;
    }


    /// <summary>
    /// Permite accionar algo y desactivar el flag debug ,
    /// Siempre y cuando estemos en modo "Debug"
    /// </summary>
    public bool DebugFlag(ref bool c, bool bypass = false){
        if (c && GameManager._onDebug || bypass){
            c = false;
            return true;
        }

        if (!GameManager._onDebug && c){
            PrintX("DebugFlag: Asignar a GameManager._onDebug como true para usarlo");
            c = false;
        }

        return false;
    }

    /// <summary>
    /// Preguntamos si es nulo el valor indicado
    /// </summary>
    public bool IsNull<T>(T t) => t == null;

    /// <summary>
    /// Coloca el valor puesto en caso de que sea null
    /// </summary>
    public void Default<T>(ref T value, T defaultVal) => value = IsNull(value) ? defaultVal : value;
    //public bool IsNull(Component t) => t ^ null;
    //protected?
    #endregion
}