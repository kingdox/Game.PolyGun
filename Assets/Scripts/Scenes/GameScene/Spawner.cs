#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavLib;
#endregion
public class Spawner : MonoX
{
    #region Variables

    /// <summary>
    /// Lugar donde tendremos los hijos y sus posiciones
    /// </summary>
    [Header("Prefabs to create")]
    public GameObject[] prefs;
    [Space]
    public Transform target;
    public Transform parent;
    public Transform[] childs;


    [Header("Debug")]
    public bool _Debug_Spawn = false;
    public SpawnOpt _Debug_SpawnOpt = SpawnOpt.RANDOM;
    #endregion
    #region Events
    private void Start(){
        GetChilds(out childs);

        //TODO por los momentos irá dirigido al player el target

    }
    private void Update()
    {
#if DEBUG
        __Debug_Spawn();
#endif
    }
    #endregion
    #region Methods


    /// <summary>
    /// Revisamos la opcion de invocación y, dependiendo de la selecta se tomará
    /// en cuenta  ciertas cosas, el objetivo influye para los NEAR o FAR
    /// <para>Por defecto el target es el Spawner(Suponiendo que está en el centro del mapa)</para>
    /// </summary>
    public void Generate(GameObject pref, SpawnOpt opt= SpawnOpt.RANDOM){
        //Hay que crear un objeto ? o añadirle eso...
        int i = GetIndexOfDistanceType(opt);

        Instantiate(pref, childs[i].transform.position, Quaternion.identity, parent);
    }


    /// <summary>
    /// Toma el indice mas apropiado basado en el tipo de spawn
    /// </summary>
    private int GetIndexOfDistanceType(SpawnOpt opt){

        if (opt.Equals(SpawnOpt.RANDOM))   {
            return XavHelpTo.Get.ZeroMax(childs.Length);
        }
        else
        {
            int nearIndex = 0 ;
            float nearest = Vector3.Distance(childs[0].position, target.position); ;

            for (int i = 0; i < childs.Length; i++){
                float distance = Vector3.Distance(childs[i].position, target.position);
                //si la distancia es menor que la supuesta "mas cercana" la sustituye
                if (
                    (opt.Equals(SpawnOpt.NEAR) && distance < nearest)
                    || (opt.Equals(SpawnOpt.FAR) && distance > nearest)
                ){
                    nearIndex = i;
                    nearest = distance;
                }
            }
            return nearIndex;
        }

    }


#if DEBUG
    /// <summary>
    /// Nos permite spawnaear cosas
    /// </summary>
    public void __Debug_Spawn(){
        if (_Debug_Spawn){
            //interruptor
            _Debug_Spawn = false;

            //Obtenemos el
            Generate(XavHelpTo.Get.Range(prefs), _Debug_SpawnOpt);


            //TODO
            //Ejecuta la acción

        }
    }
#endif
    #endregion
}
/// <summary>
/// Revisamos el comportamiento del spawn, como queremos que invoque
/// </summary>
public enum SpawnOpt
{
    RANDOM,
    NEAR,
    FAR
}