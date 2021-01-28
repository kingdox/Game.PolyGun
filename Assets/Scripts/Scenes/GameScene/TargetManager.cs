#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
public class TargetManager : MonoX
{
    #region Variables
    private static TargetManager _;

    [Header("@Areas")]
    public Transform @AlliesContainer;
    public Transform @EnemiesContainer;
    public Transform @ItemsContainer;
    public Transform @LeftoverContainer;
    [Space]
    public Transform Player;
    #endregion
    #region Events
    private void Awake(){
        _ = this;
    }
    #endregion
    #region Methods
    /// <summary>
    /// return the container of the leftover thing (caused by <seealso cref="Destructure"/>)
    /// </summary>
    public static Transform GetLeftoverContainer() => _.LeftoverContainer;
    /// <summary>
    /// return the container of the enemies
    /// </summary>
    public static Transform GetEnemiesContainer() => _.EnemiesContainer;
    /// <summary>
    /// return the container of the allies
    /// </summary>
    public static Transform GetAlliesContainer() => _.AlliesContainer;
    /// <summary>
    /// return the container of the enemies
    /// </summary>
    public static Transform GetItemsContainer() => _.ItemsContainer;
    /// <summary>
    /// return the player
    /// </summary>
    public static Transform GetPlayer() => _.Player;

    /// <summary>
    /// Gets a random enemy, if exist a <see cref="Transform"/> as arg, then
    /// it is the most Near;
    /// </summary>
    public static Transform GetEnemy(Transform tr = null) => GetFromContainer(_.EnemiesContainer, tr);

    /// <summary>
    /// Gets a random ally, if exist a <see cref="Transform"/> as arg, then
    /// it is the most Near;
    /// </summary>
    public static Transform GetAlly(Transform tr = null) => GetFromContainer(_.EnemiesContainer, tr);

    /// <summary>
    /// Find the nearest item
    /// </summary>
    public static Transform GetItem(Transform tr = null) => GetFromContainer(_.ItemsContainer, tr);

    /// <summary>
    /// select as target the boss
    /// </summary>
    public static Transform GetBoss()
    {
        //TODO aquí tienes que buscar con excepcion dentro de los enemies...

        return _.transform;
    }




    /// <summary>
    /// Gets a random enemy, if exist a <see cref="Transform"/> as arg, then
    /// it is the most Near;
    /// </summary>
    private static Transform GetFromContainer(Transform container, Transform target = null){

        Transform trResult = null;

        //if exist enemy in scene
        if (!container.childCount.Equals(0))
        {
            //if get a random or exist a target to get the nearest
            if (_.IsNull(target))
            {
                int random = Random.Range(0, container.childCount);
                trResult = container.GetChild(random);
                //normal, it's random 
            }
            else
            {
                float lastDistance = -1;

                for (int x = 0; x < container.childCount; x++)
                {

                    Transform child = container.GetChild(x);
                    float dist = Vector3.Distance(target.position, child.position);

                    if (_.IsNull(trResult) || dist > lastDistance)
                    {
                        // asign the nearest enemy
                        trResult = child;
                    }
                }
            }
        }

        return trResult;
    }
    #endregion
}
