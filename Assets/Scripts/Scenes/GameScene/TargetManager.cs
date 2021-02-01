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
    [Space]
    public Transform @ItemsContainer;
    public Transform @BulletsContainer;
    [Space]
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
    /// return the container
    /// </summary>
    public static Transform GetItemsContainer() => _.ItemsContainer;
    /// <summary>
    /// return the container
    /// </summary>
    public static Transform GetBulletsContainer() => _.BulletsContainer;
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
    public static Transform GetAlly(Transform tr = null) => GetFromContainer(_.AlliesContainer, tr);

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
    /// Gets a random, if exist a <see cref="Transform"/> as arg, then
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


                //new Transform[container.childCount];
                Transform[] childs;
                _.GetChilds(out childs, container);
                Transform nearestChild = null;
                float nearestChildValue = -1;

                foreach (Transform child in childs)   
                {
                    //if we don't have a child
                    if (nearestChild == null)
                    {
                        //default
                        nearestChild = child;
                        nearestChildValue = Vector3.Distance(target.position, child.position);
                    }
                    else
                    {
                        //if we have a saved child
                        float newChildDistance = Vector3.Distance(target.position, child.position);

                        //devolverá el más cercano  SIEMPRE que esté por debajo de los 5 metros de altura
                        if (newChildDistance < nearestChildValue && child.position.y < 5)
                        {
                            nearestChild = child;
                            nearestChildValue = newChildDistance;
                            //Debug.DrawLine(target.position.normalized, child.position,Color.white);
                        }

                    }
                }
                //(_.IsNull(trResult) || dist > lastDistance) && child.position.y < 5)
                trResult = nearestChild;






            }
        }

        return trResult;
    }



    public void __Debug_ClearCharacter(int type)
    {
        //(CharacterType)type

        switch ((CharacterType)type)    
        {
            case CharacterType.MINIONS:
                __Debug_ClearMinion(AlliesContainer);
                __Debug_ClearMinion(EnemiesContainer);
                break;
            case CharacterType.PLAYER:
                GetPlayer().GetComponent<PlayerController>().SetDead();
                break;
            case CharacterType.ENEMY:
                __Debug_ClearMinion(EnemiesContainer);
                break;
            case CharacterType.ALLY:
                __Debug_ClearMinion(AlliesContainer);
                break;

            case CharacterType.BOSS:
                //Nothing...
                break;

            default:
                break;
        }


    }
    /// <summary>
    /// Destructure the cointainer of minions
    /// </summary>
    private void __Debug_ClearMinion(Transform container)
    {
        Minion[] minions = container.GetComponentsInChildren<Minion>();

        foreach (Minion minion in minions)
        {
            minion.Delete();
        }
    }

    /// <summary>
    /// Destroys al the objects in leftover container
    /// codigo repetido..
    /// </summary>
    public void __Debug_DestroyLeftover()
    {
        Transform container = LeftoverContainer;

        for (int x = 0; x < container.childCount; x++)
        {
            Destroy(container.GetChild(x).gameObject);
        }
    }
    #endregion
}
/* Old
 
                //float lastDistance = -1;
                //int lastIndex = -1;

                ////Recorremos el contenedor y por cada elemento
                //for (int x = 0; x < distances.Length; x++)
                //{
                //    //obtenemos el elemento
                //    Transform child = container.GetChild(x);
                //    if (child.position.y < 5)
                //    {
                //        //preguntamos la distancia entre el elemento y el player
                //        distances[x] = Vector3.Distance(target.position, child.position);
                //    }
                //    else
                //    {
                //        distances[x] = -1;
                //    }


                //    //if ((_.IsNull(trResult) || dist > lastDistance) && child.position.y < 5)
                //    //{
                //    //    // asign the nearest enemy
                //    //    trResult = child;
                //    //}
                //}

                //for (int z = 0; z < distances.Length; z++)
                //{

                //    if (lastDistance > distances[z] && lastDistance > 0 && distances[z] != -1)
                //    {
                //        lastDistance = distances[z];
                //        lastIndex = z;
                //    }

                //}

                //if (lastIndex != -1)
                //{
                //    trResult = container.GetChild(lastIndex);
                //}
                ////foreach (float distance in distances)
                ////{
                ////    //if the last distance is greater than the actual then overwrites it
                ////    if (lastDistance > distance)
                ////    {
                ////        lastDistance = distance;
                ////    }


                ////}
 
 */