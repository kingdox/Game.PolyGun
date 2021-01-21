#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
/// <summary>
/// Objeto que tenemos en la escena de juego que nos dará la información
/// del item, su tipo, y su numero (1,3 si es shape) (1,5 si es Buff)
/// </summary>
public class Item : MonoBehaviour
{
    #region Variables
    //public int itemN;
    public ItemType type;
    public ItemContent content;

    #endregion
    #region Events

    #endregion
    #region Methods

    #endregion
}

/*
 TODO
Qué es un Item ?
Item es un objeto que está en el suelo, posee fisicas de
    tipo Trigger y tambien de colisión con el suelo,
    con el trigger manejaremos las entradas con el player

El objeto por su cuenta no ejecuta nada, solo es un detector
 
 

TODO

Usar suscripciones?

 */