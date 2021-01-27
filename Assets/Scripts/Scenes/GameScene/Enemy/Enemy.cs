#region IMports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
#endregion
[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : MonoX
{
    #region Variables
    [Header("Enemy Settings")]
    public Character character;
    [Space]
    public Transform target;
    [Space]
    public NavMeshAgent navMeshAgent;
    [Space]
    public Destructure destructure;
    [Space]
    public bool isBoss=false;
    private readonly string targetTagName = "player";
    #endregion
    #region Events
    private void Start()
    {
        Get(out navMeshAgent);
        GetAdd(ref destructure);
        //CheckForTarget(targetTagName);
    }
   
    #endregion
    #region Methods

    /// <summary>
    /// Buscará un tag y el objetivo mas cercano 
    /// </summary>
    public void CheckForTarget(string tag)
    {
        //Buscamos todos los objetos de la escena que contengan el tag indicado
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);

        //target = null;
        foreach (GameObject g in targets)
        {
            if (target == null)
            {
                target = g.transform;
            }
            else if (
              //Si el actual es mas lejos que que el nuevo cambiamos
              Vector3.Distance(transform.position, target.position) >
              Vector3.Distance(transform.position, g.transform.position)
          )
            {
                //Se asigna el nuevo, que es más cercano
                target = g.transform;


            }
        }
    }

    public void CheckDamage(float damage)
    {
        //
        character.timeLife -= damage;

        //TODO si es detruido por que se queda sin vida...
        if (!character.IsAlive())
        {
            //permite añadir al checker de achieve
            Kill();
        }
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
