#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
#endregion
public abstract class Ally : MonoX
{
    #region Variables
    [Header("Ally Settings")]
    public Character character;
    [Space]
    public Transform target;
    [Space]
    public NavMeshAgent navMeshAgent;
    [Space]
    public Destructure destructure;
    [Space]
    protected bool isDead = false;
    #endregion
    #region Events
    private void Awake()
    {
        Get(out navMeshAgent);
        GetAdd(ref destructure);
    }
    //DON'T USE UPDATE HERE
    #endregion
    #region Methods
    public void UpdateMesh()
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

/*

    TODO ver
    Comportamiento de cada aliado..

    Qué se asemejan los ally?

    - poseen un objetivo a donde dirigirse
    - poseen un rango para accionar algo
    - poseen


 */