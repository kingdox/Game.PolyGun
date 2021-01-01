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
public abstract class MonoManager : MonoBehaviour, IManager
{
    #region Variables
    [Header("MonoManager")]
    private readonly float waitTime = 0.01f;
    private bool _init = false;
    #endregion
    #region Events
    //Todo permitir usar más de 1 awake
    public void Awake() => StartCoroutine(CheckIniter());
    public abstract void Init();
    #endregion
    #region Methods
    public IEnumerator CheckIniter(){
        _init = false;
        yield return new WaitForSeconds(waitTime);
        if (!_init)
        {
            if (DataPass.IsReady())
            {
                _init = true;
                Init();
            }
            StartCoroutine(CheckIniter());
        }
    }
    public void GoToScene(string name) => XavHelpTo.ChangeSceneTo(name);
    public void GoToScene(Scenes scene) => XavHelpTo.ChangeSceneTo(scene.ToString());
    #endregion

}
/// <summary>
/// Enumerador de las clases tipo <seealso cref="MonoManager"/>
/// </summary>
public interface IManager{

    /// <summary>
    /// Revisamos cada <see cref="waitTime"/> para inicializar con <see cref="MonoManager_Initer"/>
    /// <para>Si no puede inicializar entonces cagamos de nuevo <see cref="CheckIniter"/></para>
    /// </summary>
    IEnumerator CheckIniter();

    /// <summary>
    /// <para>__</para>
    /// Evento para Inicializar las cosas de manager al ver que esta listo
    /// <para>Escribir así: =></para>
    /// <para>"public override void Init(){...}"</para>
    /// </summary>
    void Init();
    /// <summary>
    /// Cambiamos a la escena
    /// </summary>
    void GoToScene(string name);
    /// <summary>
    /// Cambiamos a la escena
    /// </summary>
    void GoToScene(Scenes scene);
}
