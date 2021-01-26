#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
#endregion
///Si colocas obstaculos cambiar a navMesh?
public class Ally : MonoX
{
    #region Variables
    [Header("Ally Settings")]
    public Character character;
    [Space]
    public Transform target;
    [Space]
    //TODO testeo
    public NavMeshAgent navMeshAgent;
    [Space]
    private Destructure destructure;
    #endregion
    #region Events
    private void Awake()
    {
        Get(out navMeshAgent);
        Get(out destructure);

        navMeshAgent.angularSpeed = 360;
        navMeshAgent.speed = character.speed;
        navMeshAgent.acceleration = navMeshAgent.speed;
        navMeshAgent.stoppingDistance = 5;//TODO temporal
        //navMeshAgent.
    }
    private void Update()
    {
        character.LessLife();

        if (character.IsAlive())
        {
            //set the new path b the target position
            navMeshAgent.SetDestination(target.position);
        }
        else
        {
            Delete();
        }
        
    }
    #endregion
    #region Methods

    public void Delete()
    {
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