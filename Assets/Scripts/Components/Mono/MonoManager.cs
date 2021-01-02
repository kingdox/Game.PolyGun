#region imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;
using XavLib;
//using System;
//using System.Reflection;
//using UnityEngine.Analytics;
//using UnityEngine.Events;
#endregion
/// <summary>
/// Abstract de los Manager para administrar cosas que dependen de inicialización
/// <para>Heredan <seealso cref="MonoInit"/></para>
/// </summary>
public abstract class MonoManager : MonoInit, IManager
{
    #region Methods
    public void GoToScene(string name) => XavHelpTo.ChangeSceneTo(name);
    public void GoToScene(Scenes scene) => XavHelpTo.ChangeSceneTo(scene.ToString());
    #endregion
}
/// <summary>
/// Interface de las clases tipo <seealso cref="MonoManager"/>
/// </summary>
public interface IManager{
    /// <summary>
    /// Cambiamos a la escena
    /// </summary>
    void GoToScene(string name);
    /// <summary>
    /// Cambiamos a la escena
    /// </summary>
    void GoToScene(Scenes scene);
}
