#region Imports
using UnityEngine;
#endregion
/// <summary>
/// ROMB: Cada cierto tiempo crea piezas para dejarlas en el suelo. 
/// Mantiene grande distancia con el enemigo mas cercano, se mueve muy lento. no ataca
/// </summary>
public class RombController : Minion
{
    #region Variables

    [Header("Romb Settings")]

    //[Space]
    //private Vector3 lastPos;
    [Space]
    public ParticleSystem part_create;
    public float timerCreateCount = 0;
    [Space]
    public float minRangeSize = 2;

    #endregion
    #region Events
    private void Start()
    {

        LoadMinion();
        target = TargetManager.GetPlayer();
        
    }
    private void Update()
    {
        if (UpdateMinion())
        {
            if (target != null)
            {
                //Updates the movement
                UpdateMovement();

                
                //Sets a timer to see when you can  generate one of the items shapes
                if (Timer(ref timerCreateCount, character.atkSpeed))
                {
                    //generate the item in front of herself
                    UpdateCreation();
                }

            }
        }
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.magenta;
    //    Gizmos.DrawWireSphere(transform.position, minRangeSize);
    //}
    #endregion
    #region Methods

    /// <summary>
    /// Generates a item in front (transform.forward)
    /// </summary>
    private void UpdateCreation()
    {
        part_create.Play();
        Instantiate(
            ItemManager.GetRandomItemShape(),
           transform.position + transform.forward,
           Quaternion.identity, TargetManager.GetItemsContainer()
       );
    }


    /// <summary>
    /// updates the movement of the ally to the player OR try to run out of the nearest enemy
    /// TODO sería bueno hacer un manejo en caso de que un enemigo le bloquee el paso
    /// </summary>
    private void UpdateMovement()
    {

        //returns the nearest enemy if it exist
        Transform enemy = TargetManager.GetEnemy(transform);
        float distancePlayer = Vector3.Distance(transform.position, target.position);
        bool followPlayer = false;
        bool enemyExist = false;
        //Check if we found a enemy on Scene
        if (enemy != null)
        {
            enemyExist = true;
            //gets the distance between the object and the enemy
            float enemyDistance = Vector3.Distance(transform.position, enemy.position);

            //check if a enemy is too near
            if (enemyDistance < minRangeSize)
            {
                //gets the direction contraary of the enemy position
                Vector3 direction = transform.position - enemy.position;
                direction = Vector3.Normalize(direction);
                //move to the contrary direction
                movement.Move(direction, character.speed);
            }
            else
            {
                //if the nearest enemy is far....
                followPlayer = true;
                //enemyExist
                //TODO error al  combinar con player y enemy
            }
        }
        else
        {
            followPlayer = true;
        }


        //if we gonna follow the player then.....
        if (followPlayer)
        {

            //checks if the distance is major than the minimal
            if (distancePlayer > minRangeSize)
            {
                rotation.LookTo(target.position);
                Vector3 direction = target.position - transform.position;

                direction = Vector3.Normalize(direction);

                bool wantToMove = true;
                if (enemyExist)
                {
                    //in case that exist an enemy AND, this enemy is too near from player then stops the actions

                    if (Vector3.Distance(target.position, enemy.position) < minRangeSize)
                    {
                        wantToMove = false;
                    }

                }


                if (wantToMove)
                {
                    //moves the ally to the player
                    movement.Move(direction, character.speed);
                }

            }
           
        }
        

       
    }
    #endregion
}