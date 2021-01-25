#region IMports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
public class Enemy : MonoX
{
    #region Variables

    public Destructure destructure;
    #endregion
    #region Events
    private void Start()
    {
        Get(out destructure);
    }
    #endregion
    #region Methods

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