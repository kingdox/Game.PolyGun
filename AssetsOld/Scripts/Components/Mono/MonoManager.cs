#region imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;
using XavLib;
#endregion
/// <summary>
/// Abstract de los Manager para administrar cosas que dependen de inicialización
/// <para>Heredan <seealso cref="MonoInit"/></para>
/// </summary>
public abstract class MonoManager : MonoInit, IManager
{
    #region Methods
    public void GoToScene(string name) => XavHelpTo.Change.SceneTo(name);
    public void GoToScene(Scenes scene) => XavHelpTo.Change.SceneTo(scene.ToString());
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
