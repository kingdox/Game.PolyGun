#region imports
using Environment;
using XavHelpTo.Change;
#endregion
/// <summary>
/// Abstract de los Manager para administrar cosas que dependen de inicialización
/// <para>Heredan <seealso cref="MonoInit"/></para>
/// </summary>
public abstract class MonoManager : MonoInit, IManager
{
    #region Methods
    public void GoToScene(string name) => Change.SceneTo(name);
    public void GoToScene(Scenes scene) => Change.SceneTo(scene.ToString());

    //public void Singleton<T>(ref T _, T t)
    //{
    //    if (_ == null)
    //    {
    //        _ = t;
    //    }
    //    else if (_ != t)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

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
