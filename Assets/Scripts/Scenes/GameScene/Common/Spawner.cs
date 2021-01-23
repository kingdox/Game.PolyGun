#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavLib;
#endregion
public class Spawner : MonoX
{
    #region Variables

    [Header("Prefabs to create")]
    [Space]
    /// <summary>
    /// Lugar objetivo del jugador
    /// </summary>
    public Transform target;
    /// <summary>
    /// DOnde se creara como hijo de este objeto
    /// </summary>
    public Transform parent;
    public float range= 3f;
    private Vector3[] childsPos;

    [Header("Debug")]
    public bool _Debug_Spawn = false;
    public GameObject[] _Debug_prefs;
    public SpawnOpt _Debug_SpawnOpt = SpawnOpt.RANDOM;
    #endregion
    #region Events
    private void Start(){
        Transform[] childs;

        GetChilds(out childs);

        New(out childsPos, childs.Length);

        for (int i = 0; i < childs.Length; i++){
            childsPos[i] = childs[i].position;
        }
    }
    private void Update()
    {

        __Debug_Spawn();
    }
    #endregion
    #region Methods


    /// <summary>
    /// Revisamos la opcion de invocación y, dependiendo de la selecta se tomará
    /// en cuenta  ciertas cosas, el objetivo influye para los NEAR o FAR
    /// <para>Por defecto el target es el Spawner(Suponiendo que está en el centro del mapa)</para>
    /// </summary>
    public void Generate(GameObject pref, SpawnOpt opt){
        //Hay que crear un objeto ? o añadirle eso...
        int i = GetIndexOfDistanceType(opt);

        Vector3 pos = XavHelpTo.Get.MinusMax(childsPos[i], range, 1);

        Instantiate(pref, pos, Quaternion.identity, parent);
    }

    public int GetActualQty() => parent.childCount;

    /// <summary>
    /// Toma el indice mas apropiado basado en el tipo de spawn
    /// </summary>
    private int GetIndexOfDistanceType(SpawnOpt opt){

        if (opt.Equals(SpawnOpt.RANDOM))   {
            return XavHelpTo.Get.ZeroMax(childsPos.Length);
        }
        else
        {
            int nearIndex = 0 ;
            float nearest = Vector3.Distance(childsPos[0], target.position); ;

            for (int i = 0; i < childsPos.Length; i++){
                float distance = Vector3.Distance(childsPos[i], target.position);
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
        if (!DebugFlag(ref _Debug_Spawn)) return;


        Generate(XavHelpTo.Get.Range(_Debug_prefs), _Debug_SpawnOpt);
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