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
/// Abstract de los Manager
/// <para>Heredan <seealso cref="MonoBehaviour"/></para>
/// </summary>
public abstract class MonoManager : MonoBehaviour, IManager
{
    #region Variables
    [Header("MonoManager")]
    private readonly float waitTime = 0.01f;
    #endregion
    #region Events
    //Todo permitir usar más de 1 awake
    public void Awake() => Begin();
    public abstract void Init();
    #endregion
    #region Methods
    public void Begin() => StartCoroutine(CheckIniter());
    public IEnumerator CheckIniter(){
        yield return new WaitForSeconds(waitTime);
        //si DataPass esta listo dispara Init, sino vuelve a esperar...
        if (DataPass.IsReady()) Init();
        else StartCoroutine(CheckIniter());
    }
    public void GoToScene(string name) => XavHelpTo.ChangeSceneTo(name);
    public void GoToScene(Scenes scene) => XavHelpTo.ChangeSceneTo(scene.ToString());
    #endregion
}
/// <summary>
/// Interface de las clases tipo <seealso cref="MonoManager"/>
/// </summary>
public interface IManager{

    /// <summary>
    /// Iniciador del detector para iniciar
    /// <para>
    /// Si se modifica el awake sin asignar <see cref="Begin"/> entonces el disparador <see cref="Init"/> no podrá iniciar
    /// </para>
    /// </summary>
    void Begin();

    /// <summary>
    /// Revisamos cada <see cref="waitTime"/>  si <seealso cref="DataPass.IsReady()"/>
    /// <para>Cuando esta listo disparamos el evento <see cref="Init"/></para>
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
