using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que nos permite detectar cuando esta listo
/// <see cref="DataPass.IsReady"/>
/// <para>Al hacerlo emitimos un evento <see cref="Init"/></para>
/// <para>Los derivados de esta clase sabrán cuando iniciar cosas...</para>
/// </summary>
public abstract class MonoInit : MonoBehaviour, IInit
{
    #region Var
    private readonly float waitTime = 0.01f;
    #endregion
    #region Event
    public void Awake() => Begin();
    public abstract void Init();
    #endregion
    #region Method
    public void Begin() => StartCoroutine(CheckIniter());
    public IEnumerator CheckIniter(){
        yield return new WaitForSeconds(waitTime);
        //si DataPass esta listo dispara Init, sino vuelve a esperar...
        if (DataPass.IsReady()) Init();
        else StartCoroutine(CheckIniter());
    }
    #endregion

}
public interface IInit
{
    /// <summary>
    /// Iniciador del detector para iniciar
    /// <para>
    /// Si se modifica el awake sin asignar <see cref="Begin"/> entonces el disparador <see cref="Init"/> no podrá iniciar
    /// </para>
    /// </summary>
    void Begin();
    /// <summary>
    /// <para>__</para>
    /// Evento para Inicializar las cosas de manager al ver que esta listo
    /// <para>Escribir así: =></para>
    /// <para>"public override void Init(){...}"</para>
    /// </summary>
    void Init();
    /// <summary>
    /// Revisamos cada <see cref="waitTime"/>  si <seealso cref="DataPass.IsReady()"/>
    /// <para>Cuando esta listo disparamos el evento <see cref="Init"/></para>
    /// </summary>
    IEnumerator CheckIniter();
}