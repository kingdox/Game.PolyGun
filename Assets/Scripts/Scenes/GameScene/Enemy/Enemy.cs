#region IMports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
#endregion
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Destructure))]
public abstract class Enemy : MonoX
{
    #region Variables
    [Header("Enemy Settings")]
    public Character character;
    public Transform target;
    public bool isBoss=false;
    [Space]
    [Header("Requirements")]
    public NavMeshAgent navMeshAgent;
    public Destructure destructure;
    #endregion
    #region Methods

    public void InitEnemy()
    {
        navMeshAgent.angularSpeed = 360;
        navMeshAgent.speed = character.speed;
        navMeshAgent.acceleration = character.speed;
        navMeshAgent.stoppingDistance = 5;//TODO temporal
        navMeshAgent.updatePosition = true;
        navMeshAgent.updateRotation = true;
        navMeshAgent.updateUpAxis = true;
    }


    /// <summary>
    /// Updates the basics of the enemies and then returns true wether is alive 
    /// </summary>
    public bool UpdateEnemy()
    {
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
                Kill();
            }
        }

        return keepGoing;
    }
    /// <summary>
    /// Move it
    /// </summary>
    public void Move(Transform tr = null)
    {
        if (!navMeshAgent.isOnNavMesh) return;
        if (tr == null) tr = transform;

        navMeshAgent.SetDestination(tr.position);
    }

    /// <summary>
    /// Hace el proceso de eliminación del enemigo
    /// </summary>
    public void Kill()
    {
        destructure.DestructureThis();
        Destroy(gameObject);
    }

   
    #endregion
}

/// <summary>
///TODO esto no, muy costoso ?
/// 
/// Buscará un tag y el objetivo mas cercano
/// 
/// </summary>
//public void CheckForTarget(string tag)
//{
//    //Buscamos todos los objetos de la escena que contengan el tag indicado
//    GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);

//    //target = null;
//    foreach (GameObject g in targets)
//    {
//        if (IsNull(target))
//        {
//            target = g.transform;
//        }
//        else if (
//          //Si el actual es mas lejos que que el nuevo cambiamos
//          Vector3.Distance(transform.position, target.position) >
//          Vector3.Distance(transform.position, g.transform.position)
//        )
//        {
//            //Se asigna el nuevo, que es más cercano
//            target = g.transform;
//        }
//    }
//}