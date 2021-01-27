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
    public Transform @LeftoverContainer;
    public Transform @EnemiesContainer;

    #endregion
    #region Events
    private void Awake(){
        _ = this;
    }
    #endregion
    #region Methods
    /// <summary>
    /// Tomamos el elemento del LeftoverCOntainer fisico 
    /// </summary>
    public static Transform GetLeftoverContainer() => _.LeftoverContainer;
    /// <summary>
    /// Tomamos el elemento EnemiesContainer del mundo fisico
    /// </summary>
    public static Transform GetEnemiesContainer() => _.EnemiesContainer;



    /// <summary>
    /// Gets a random enemy, if exist a <see cref="Transform"/> as arg, then
    /// it is the most Near;
    /// </summary>
    public static Transform GetEnemy(Transform tr = null)
    {
        Transform trEnemy = null;

        //if exist enemy in scene
        if (!_.@EnemiesContainer.childCount.Equals(0))
        {
            //if get a random or exist a target to get the nearest
            if (tr == null)
            {
                int random = Random.Range(0, _.@EnemiesContainer.childCount);
                trEnemy = _.@EnemiesContainer.GetChild(random);
                //normal, it's random enemy
            }
            else
            {
                float distance = -1;

                for (int x = 0; x < _.@EnemiesContainer.childCount; x++)
                {

                    Transform enemy = _.@EnemiesContainer.GetChild(x);
                    Vector3 pos = enemy.position;
                    float dist = Vector3.Distance(tr.position, pos);

                    if (trEnemy == null || dist > distance)
                    {
                        // asign the nearest enemy
                        trEnemy = enemy;
                    }
                }
            }
        }

        return trEnemy;
    }


    /// <summary>
    /// Find the nearest item
    /// </summary>
    public static Transform GetNearItem(Transform tr)
    {
        return _.transform;
    }
    /// <summary>
    /// select as target the boss
    /// </summary>
    public static Transform GetBoss()
    {
        return _.transform;
    }
    /// <summary>
    /// target the player
    /// </summary>
    public static Transform GetPlayer()
    {
        return _.transform;
    }
    #endregion
}
