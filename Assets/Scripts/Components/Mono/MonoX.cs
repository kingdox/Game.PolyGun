#region Imports
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavLib;
#endregion
/// <summary>
/// MonoX poseerá todas las propiedades comunes para los objetos
/// No usar MonoBehaviour, en cambio usar <see cref="MonoX"/>
/// para adiciones especiales y comunes
/// <para>
/// </para>
/// </summary>
public class MonoX : MonoBehaviour
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
    /// Tomamos del arreglo los componentes hijos sin el componente actual
    /// <para>Este vendrá ordenadamente, su coste es mayor</para>
    /// </summary>
    public void GetChilds<T>(out T[] t) {
        New(out t, transform.childCount);
        for (int x = 0; x < transform.childCount; x++){
            t[x] = transform.GetChild(x).GetComponent<T>();
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
    /// Pinta con un color el texto
    /// </summary>
    public static void PrintX(string txt) => print(XavHelpTo.Look.ColorPrint(txt, XavHelpTo.Look.RandomColor()));
    public static void PrintX<T>(T txt) => PrintX(txt.ToString());


    /// <summary>
    /// Creas una nueva dimension de arreglo del tipo que desees
    /// </summary>
    public void New<T>(out T[] t,int qty) => t = new T[qty];



    /// <summary>
    /// Hace el conteo y devuelve true cuando pasa una vuelta.
    /// <para>Se usa <see cref="XavHelpTo.Set.TimeCountOf(ref float, float) para aprovechar el codigo, y reducir texto"/></para>
    /// </summary>
    public bool Timer(ref float count,float timer) => XavHelpTo.Set.TimeCountOf(ref count, timer);


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
    //public bool IsNull<T>(T t) => t ^ null;
    //public bool IsNull(Component t) => t ^ null;
    //protected?
    #endregion
}