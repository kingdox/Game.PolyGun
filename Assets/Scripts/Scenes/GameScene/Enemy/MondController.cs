#region Imports
using UnityEngine;
#endregion
[RequireComponent(typeof(Shot))]
/// <summary>
/// Mond is like a shooter guy
/// </summary>
public class MondController : Minion
{

    #region Variables
    [Header("Mond Settings")]
    //public float damageTimeCount = 0;
    //public bool canDamage = true;
    //private bool canInteract;

    private float refreshTargetCount;
    [Space]
    public ParticleSystem par_explode;
    private Shot shot;
    [Space]
    private Transform _player;

    [Header("DEBUG")]
    public float radiusCenter = 0;
    public float minRangeSize = 4;
    [Space]
    public float stayRangeSize = 2;
    public float runRangeSize = 4;
    private bool wantGoMinRange = false;
    #endregion
    #region Events
    private void Start()
    {
        Get(out shot);
        if (shot == null)
        {
            Debug.LogError("Te falta añadir en el inspector el shot");
        }
        else
        {
            //COLOCAMOS LAS COSAS DE SHOT
            shot.timer_bullet = character.atkSpeed;

        }

        LoadMinion();
        //al inicio se carga target con el transform de este obj
        _player = TargetManager.GetPlayer();


        character.canExtraShots = isEnemyBoss;

    }
    private void Update()
    {

        if (UpdateMinion())
        {

            //if exist a target then...
            if (target != null)
            {
              

                //Rotation Refresh
                if (target != transform)
                {
                    // rotates to that target
                    rotation.LookTo(target.position);
                }

                // check if is in range
                if (IsInRange() )
                {
                    //if can attack then it shot
                    shot.ShotBullet(character, isEnemyBoss);

                }


                // MOVEMENT
                UpdateMovement();

                // Target refresher
                if (Timer(ref refreshTargetCount, 1))
                {
                    UpdateTarget();
                }
            }
            else
            {
                UpdateTarget();
            }
        }



    }

    private void OnDisable()
    {
        TargetManager.EffectInTime(par_explode);

    }
    //private void OnDrawGizmos()
    //{
    //    if (target != null)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawLine(transform.position, target.position);
    //        Debug.DrawLine(transform.position, target.position, Color.red);

    //        Gizmos.DrawWireSphere(transform.position, character.range);

    //        Gizmos.color = Color.white;
    //        Gizmos.DrawWireSphere(transform.position, character.range / stayRangeSize);

    //        Gizmos.color = Color.green;
    //        Gizmos.DrawWireSphere(transform.position, character.range / runRangeSize);
    //    }
    //    //radio del centro en el que puede moverse, pera evitar las esquinas...
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(Vector3.zero, radiusCenter);
    //    Gizmos.color = Color.magenta;
    //    Gizmos.DrawWireSphere(Vector3.zero, radiusCenter - radiusCenter / minRangeSize);

    //}
    #endregion
    #region Methods



    /// <summary>
    /// find the nearest target ally in scene, otherwise select the player
    /// </summary>
    private void UpdateTarget()
    {
        //revisamo el target actual, si es el transform entonces lo volvemos null

        //try to find the nearest ally
        Transform allyMinion = TargetManager.GetAlly(transform);

        //check if exist an ally minion
        if (allyMinion != null && !isEnemyBoss) 
        {
            //gets the distance of the ally and the player
            float allyDistance = Vector3.Distance(transform.position, allyMinion.position);
            float playerDistance = Vector3.Distance(transform.position, _player.position);
            // decide to take the most certainly
            target = allyDistance < playerDistance ? allyMinion : _player;
        }
        else
        {
            //se toma al player en caso de no haber minion
            target = _player;
        }

    }




    /// <summary>
    /// - Follow a enemy until the half of the real range, this range it's to shot withput troubles
    /// - If a "Ally" or "Player" is too near then it try to go back, but it keeps following to shot to it
    /// </summary>
    private void UpdateMovement()
    {
        // if the distance between th player and you is almost over
        //the half of your range then you can move into
        float distance = Vector3.Distance(transform.position, target.position);
        bool canMove = false;
        bool isGoingAway = false;

        /*
         * - si la persona esta fuera de sus rangos => persigue 
         * - si la persona está en el primer rango(rojo) => persigue
         * - si esta en la tercera parte  => no se debe mover;
         * - si la persona esta muy cerca => huir
         * 
         * Si sobrepasa el limite mientras intenta huir, trata de irse al centro
         */
        if (distance > character.range)
        {
            canMove = true;
        }
        else
        {
           //else is in the range red

            //*-si esta en la tercera parte => no se debe mover;
            if (distance < character.range / stayRangeSize)
            {
                // if the target is too near 
                if (distance < character.range / runRangeSize)
                {
                    canMove = true;
                    isGoingAway = true;
                }

            }
            else
            {
                canMove = true;
            }

        }



        if (canMove)
        {
            float distanceWithCenter = Vector3.Distance(transform.position, Vector3.zero);
            int orientation = 1;

            //if try to run
            if (isGoingAway)
            {
                float _minOutRange = radiusCenter - radiusCenter / minRangeSize;

                //si quiere ir al rango minimo y llega
                if (wantGoMinRange && distanceWithCenter < _minOutRange)
                {
                    wantGoMinRange = false;
                }
                else
                {

                    // si llego al limite querrá ir al rango minimo
                    if (distanceWithCenter > radiusCenter)
                    {
                        wantGoMinRange = true;
                    }
                    else
                    {
                        //sino solo tirará para atrás
                        orientation = -1;
                    }

                }

            }

            if (wantGoMinRange)
            {
                //sigue al centro de la pantalla
                movement.Move(Vector3.zero, character.speed, true);
            }
            else
            {

                //se mueve  basado en su orientación en Z 
                movement.Move(orientation * transform.forward.normalized, character.speed);
            }

        }
        else
        {
            movement.StopMovement();

        }
    }
    #endregion
}