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

    //public bool IsNull<T>(T t) => t ^ null;
    //public bool IsNull(Component t) => t ^ null;
    //protected?
    //TODO hacerte un getset getAdd ??
    #endregion
}