#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
public class BoxBoxController : Ally
{
    #region Variable

    private Rotation rotation;

    [Header("BoxBox Settings")]
    
    public float damageTimeCount;
    public bool canDamage;

    #endregion
    #region Events
    private void Start()
    {
        

        GetAdd(ref rotation);
        UpdateMesh();
    }
    private void Update()
    {
        if (GameManager.IsOnGame())
        {
            character.LessLife();

            //si pasa este tiempo puede volver a atacar
            if (!canDamage && Timer(ref damageTimeCount, character.atkSpeed))
            {
                //PrintX("_______________________________-");
                canDamage = true;
            }
        

            if (character.IsAlive())
            {
                if (target != null && target != transform)
                {   
                    //set the new path b the target position
                    navMeshAgent.SetDestination(target.position);
                    rotation.LookTo(target.position);
                }
                else
                {
                    //BoxBox everytime try to find a enemy
                    GetNearEnemy();
                }
            }
            else
            {
                Delete();
            }
        }
        else
        {
            //la dejo en su mismo punto
            navMeshAgent.SetDestination(transform.position);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        //si es un enemigo le hará daño y reseteara el timer de daño
        if (canDamage && collision.transform.CompareTag("enemy"))
        {
            canDamage = false;
            Enemy enemy = collision.transform.GetComponent<Enemy>();

            enemy.CheckDamage(character.damage);
            //PrintX($"Ataca");
        }
    }
    #endregion
    #region Methods

   


    #endregion
}

/*
 * TODO
//Este se encarga de seguir al enemigo más cercano y atacar,
tiene un rango de corto alcance(ataca cuerpo a cuerpo,
con sus puños que son cubos).




- Busca al enemigo mas cercano, sino al player
 */