#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
#endregion
//[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Destructure))]
[RequireComponent(typeof(SaveVelocity))]
//[RequireComponent(typeof(Rigidbody))]
public abstract class Ally : MonoX
{
    #region Variables
    [Header("Ally Settings")]
    public Character character;
    public Transform target;
    protected bool isDead = false;
    [Space]
    [Header("Requirements")]
    //public NavMeshAgent navMeshAgent;
    public Destructure destructure;
    //public Rigidbody rigidbody;

    #endregion
    #region Methods

    /// <summary>
    /// Updates the basics of the allies and then returns true wether is alive 
    /// </summary>
    public bool UpdateAlly(){
        bool keepGoing = false;
        if (GameManager.IsOnGame())
        {
            //rigidbody.WakeUp();
            if (character.IsAlive())
            {
                //pierde vida
                character.LessLife();
                keepGoing = true;
            }
            else
            {
                Delete();
            }
        }
      

        return keepGoing;
    }
    ///// <summary>
    ///// Move it
    ///// </summary>
    //public void Move(Transform tr = null)
    //{
    //    //if (navMeshAgent == null) return;
    //    if (!navMeshAgent.isOnNavMesh) return;
    //    if (tr == null) tr = transform;
    //    navMeshAgent.SetDestination(tr.position);
    //}

    /// <summary>
    /// Destroy the ally
    /// </summary>
    public void Delete()
    {
        if (isDead) return;

        isDead = true;
        destructure.DestructureThis();
        Destroy(gameObject);
    }

   
    #endregion
}