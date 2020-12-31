#region imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;
using XavLib;
#endregion
public class MonoManager : MonoBehaviour, IManager
{
    #region Variables
    [Header("MonoManager")]
    private readonly float waitTime = 0.01f; 
    private bool _init = false;
    private bool ready = false;
    //public UnityEvent whoa;
    public  bool ManagerReady { get => MonoManager_Ready(); }

    //TODO crear un evento para INIT
    public delegate void ClickAction();
    /// <summary>
    /// sss
    /// </summary>
    public static event ClickAction OnClicked;

    #endregion
    #region Events
    public void Awake() => StartCoroutine(CheckIniter());

    public virtual void Test()
    {
        Debug.Log("Holis TEST");
    }
    #endregion
    #region Methods
    /// <summary>
    /// Revisamos cada <see cref="waitTime"/> para inicializar con <see cref="MonoManager_Initer"/>
    /// <para>Si no puede inicializar entonces cagamos de nuevo <see cref="CheckIniter"/></para>
    /// </summary>
    private IEnumerator CheckIniter(){
        _init = false;
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Checking....");
        MonoManager_Initer();
        if (!_init) StartCoroutine(CheckIniter());
    }
    /// <summary>
    /// Preguntamos si <seealso cref="DataPass"/> ya está listo
    /// <para>Si esta listo asignamos a <see cref="_init"/> = true</para>
    /// </summary>
    private void MonoManager_Initer() { if (!_init && DataPass.IsReady()) _init = true; }
    /// <summary>
    /// Revisamos si <see cref="MonoManager"/> esta iniciado (<see cref="_init"/>) y No preparado (!<see cref="ready"/>)
    /// De ser así asignamos a <see cref="ready"/> = true
    /// </summary>
    /// <returns>Retornamos si Mono esta listo (solo devuelve true una vez)</returns>
    private bool MonoManager_Ready() {
        if (!ready && _init)
        {
            ready = true;

            OnClicked += Test;
            OnClicked();
            //MonoManager.OnClicked();

            return true;
        }
        return false;
    }

    #endregion
    #region Interface
    public void GoToScene(string name) => XavHelpTo.ChangeSceneTo(name);
    public void GoToScene(Scenes scene) => XavHelpTo.ChangeSceneTo(scene.ToString());
    #endregion

}
/// <summary>
/// Enumerador de las clases tipo <seealso cref="MonoManager"/>
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
