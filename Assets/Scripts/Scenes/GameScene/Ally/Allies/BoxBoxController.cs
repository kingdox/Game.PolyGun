#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
public class BoxBoxController : Ally
{
    #region Variable

    public float timer;public float timeCount;

    /// <summary>
    /// Luego de que pase el tiempo de ataque, si alguien hace contacto aplica el daño, desactiva la colision y espera el timer
    /// </summary>
    public bool canDamage;
    #endregion
    #region Events
    private void Start()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {

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
 */