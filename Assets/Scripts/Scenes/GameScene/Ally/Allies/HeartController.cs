#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
/// <summary>
///  Heart: Éste se encarga de aumentar el “tiempo de vida” de la figura aliada más cercana,
///  dando prioridad al jugador si llega a un límite determinado,
///  este pierde parte de su “tiempo de vida” al recuperar a un aliado.la figura no ataca.
/// </summary>
public class HeartController : Minion
{
    #region Variables
    [Header("Heart Settings")]


    public float refreshTargetCount;
    public ParticleSystem part_heal;
    public float minRangeSize;
    [Space]
    private Transform player;
    ////rangos para perseguir al target
    
    private float timeHealCount = 0;
    private bool canHeal = false;

    #endregion
    #region Events
    private void Start()
    {
        LoadMinion();

        player = TargetManager.GetPlayer();
    }
    private void Update()
    {
        if (UpdateMinion())
        {

           
            //if exist a target then...
            if (target != null)
            {

                

                ////follow the target
                //UpdateFollow();

                // Target refresher
                if (Timer(ref refreshTargetCount, 1))
                {
                    UpdateTarget();
                }

                //Rotation & attack Refresh
                if (target != transform)
                {
                    // rotates to that target
                    rotation.LookTo(target.position);

                    //updates the follow 
                    UpdateFollow();


                    //if passes the time then you can heal again
                    CanPassedTime(ref canHeal, ref timeHealCount, character.atkSpeed);

                    //if (!canHeal && CanPassedTime(ref canHeal, ref timeHealCount, character.atkSpeed))
                    //{
                    //    movement.Move(transform.forward.normalized, character.speed);

                    //}

                }
                else
                {
                    movement.StopMovement();
                }


            }
            else
            {
                UpdateTarget();
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (target != null && target != transform)
        {
            //range to see the distance line
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, target.position);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(target.position, minRangeSize);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //if can heal and the collision it's same than target then you can heal it
        if (canHeal && collision.transform == target)
        {
            canHeal = false;
            part_heal.Play();
            UpdateHeal();
        }
    }
    #endregion
    #region Methods

    /// <summary>
    /// Heal the target
    /// </summary>
    private void UpdateHeal()
    {

        float magnitude = character.damage;

        //if is the player
        if (target == player)
        {
            PlayerController playerC = player.GetComponent<PlayerController>();

            //we heal with our "Damage"
            playerC.character.timeLife += magnitude;
        }
        else{
            //if is a minion
            Minion ally = GetComponent<Minion>();
            ally.character.timeLife += magnitude;
        }

        //Reduce the life of the ally
        character.timeLife -= magnitude;

    }


    /// <summary>
    /// Updates the target with the nearest transform
    /// </summary>
    private void UpdateTarget()
    {
        //refresh the ally target
        target = TargetManager.GetAlly(transform);
        PlayerController playerC = player.GetComponent<PlayerController>();


        //if cannot found another ally then set the player as target
        if (target == null || target == transform || playerC.character.timeLife < 10)
        {
            target = player;
        }
        else
        {
            //check if this ally is more nearest than player
            float distanceAlly = Vector3.Distance(transform.position, target.position);
            float distancePlayer = Vector3.Distance(transform.position, player.position);

            //si el ally está mas lejos que el player entonces asignamos el player
            if (distanceAlly > distancePlayer)
            {
                target = player;
            }
        }
    }

    /// <summary>
    /// Updates the follow to the target position
    /// </summary>
    private void UpdateFollow()
    {

        float minRange = canHeal ? 0 : minRangeSize;

        if (Vector3.Distance(target.position, transform.position ) > minRange)   
        {

            Vector3 direction = target.position - transform.position;
            direction = Vector3.Normalize(direction);
            movement.Move(direction, character.speed);
        }
        else
        {
            movement.StopMovement();
        }
    }

    #endregion
}