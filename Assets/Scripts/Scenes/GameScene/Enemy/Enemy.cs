#region IMports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
#endregion
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoX
{
    #region Variables
    private NavMeshAgent nav;
    public string targetTagName = "player";
    private Transform target;
    public Destructure destructure;
    #endregion
    #region Events
    private void Start()
    {
        Get(out nav);
        Get(out destructure);
        //CheckForTarget(targetTagName);
    }
    private void FixedUpdate()
    {
        //if (nav.enabled && target != null)
        //{
        //    nav.SetDestination(target.position);
        //}
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

        //TODO si es detruido por que se queda sin vida...
        if (true)
        {
            //permite añadir al checker de achieve
            Kil();
        }
    }


    /// <summary>
    /// Hace el proceso de eliminación del enemigo
    /// </summary>
    private void Kil()
    {
        destructure.DestructureThis();
        Destroy(gameObject);
    }

    #endregion
}

/// <summary>
/// Conocemos las diferencias entre los enemigos por el tipo
/// </summary>
public struct EnemyType{

    public EnemyName name;
    public bool isBoss;
}
/// <summary>
/// Los enemigos que hay en el juego
/// </summary>
public enum EnemyName{
    MOND,
    PLUR
}