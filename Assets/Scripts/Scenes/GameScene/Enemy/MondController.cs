#region Imports
using System.Collections;
using System.Collections.Generic;
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
    private bool canInteract;

    [Space]
    public ParticleSystem par_explode;
    private Shot shot;
    [Space]
    private Transform _player;
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

    }
    private void Update()
    {

        if (UpdateMinion())
        {
            /*
             * Mond debe:
             * 
             * - Buscar un Aliado o el player, el más cercano SIEMPRE
             * - Revisar si puede atacar, en caso de poder, si esta en el rango apuntará y disparará
             * - Si un enemigo se acerca "Demasiado 0-5" entonces retrocede a los puntos de spawn (o retroceder para atrás?, manejar raycast?)
             * 
             */


            UpdateTarget();

            //if exist a target then...
            if (target != null)
            {
                // rotates to that target
                rotation.LookTo(target.position);

                //TODO refactor this
                canInteract = movement.IsOnFloor();

                // check if is in range
                if (IsInRange() && canInteract) //TODO check the ranges
                {
                    //if can attack then it shot
                    shot.ShotBullet(character);

                }



                // MOVEMENT
                UpdateMovement();


            }
        }
    }

    private void OnDisable()
    {
        par_explode.transform.parent = TargetManager.GetLeftoverContainer();
        par_explode.Play();
        Destroy(par_explode.gameObject, par_explode.main.duration);
    }
    private void OnDrawGizmos()
    {
        if (target != null)
        {
        Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, target.position);
            Gizmos.DrawWireSphere(transform.position, character.range);
        Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, character.range  / 2);
        Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, character.range / 3);
        }
    }
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
        if (allyMinion != null) 
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
    /// TODO UpdateMovement can
    /// - Follow a enemy until the half of the real range, this range it's to shot withput troubles
    /// - If a "Ally" or "Player" is too near then it try to go back, but it keeps following to shot to it
    /// </summary>
    private void UpdateMovement()
    {
        // if the distance between th player and you is almost over
        //the half of your range then you can move into
        float distance = Vector3.Distance(transform.position, target.position);
        bool canMove = false;
        //if the distance is lower than the half of the range from this character
        if (distance  < character.range / 2 )
        {
            if (distance < character.range / 2.5f) return;

            //if the distance is lower than third part of the range from the character
            if (distance < character.range / 3)
            {
                    movement.Move(-transform.forward.normalized, character.speed);
            }
            else
            {
                movement.Move(transform.forward.normalized, character.speed);

            }
        }
        else
        {
            //Stops the movement
            movement.StopMovement();
        }



        /*
         * - si la persona esta fuera de sus rangos => persigue 
         * - si la persona está en el primer rango(rojo) => persigue
         * - si esta en la tercera parte  => no se debe mover;
         * - si la persona esta muy cerca => huir
         */

         //* - si la persona esta fuera de sus rangos => persigue
        if (distance > character.range)
        {

            // si la persona está en el primer rango(rojo) => persigue
            if (distance < character.range) 
            {

                //*-si esta en la tercera parte => no se debe mover;
                if (distance < character.range / 2.5f )    {

                    if (distance < character.range / 2.5f)
                    {

                    }

                }
                else
                {

                }
            }



        //if (canMove)
        //{
        //    movement.Move(transform.forward.normalized, character.speed);
        //}
        //else
        //{
        //    movement.StopMovement();

        //}
    }
    #endregion
}