#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
public class Spawner : MonoX
{
    /*
     * TODO
     * 
     * Lugar donde poseeremos un arreglo de objetos que
     * nos dirán los puntos en donde se puede invocar el objeto <T>
     * 
     * 
     */
    #region Variables

    /// <summary>
    /// Lugar donde tendremos los hijos y sus posiciones
    /// </summary>
    public Transform[] childs;

    /// <summary>
    /// Revisamos el comportamiento del spawn, como queremos que invoque
    /// </summary>
    public enum SpawnOpt
    {
        RANDOM,
        NEAR, //cerca de un target
        FAR
    }
    #endregion
    #region Events
    private void Awake(){
        
        GetChilds(out childs);

        PrintX(childs.Length);
    }
    #endregion
    #region Methods


    /// <summary>
    /// Revisamos la opcion de invocación y, dependiendo de la selecta se tomará
    /// en cuenta  ciertas cosas, el objetivo influye para los NEAR o FAR
    /// <para>Por defecto el target es el Spawner(Suponiendo que está en el centro del mapa)</para>
    /// </summary>
    public void Generate<T>(SpawnOpt opt= SpawnOpt.RANDOM, Transform target = default)
    {

    }
    #endregion
}
/// <summary>
/// TODO
/// Interface que nos permitirá invocar las nesecidades pero con
/// el tipo deseado
/// </summary>
public interface ISpawner<T>
{

}