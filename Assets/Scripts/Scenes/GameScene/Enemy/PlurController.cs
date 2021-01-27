#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
public class PlurController : Enemy
{

    #region Variables

    #endregion
    #region Events
    private void Update()
    {
        if (GameManager.IsOnGame())
        {
            character.LessLife();
            if (!character.IsAlive())
            {
                Kill();
            }
        }
        else
        {
            navMeshAgent.SetDestination(transform.position);
        }
    }
    #endregion
    #region Methods

    #endregion
}