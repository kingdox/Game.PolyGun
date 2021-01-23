using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}



/// <summary>
/// Conocemos las diferencias entre los enemigos por el tipo
/// </summary>
public struct EnemyType{

    public EnemyName name;
    public bool isBoss;

    /*
     *  - Tenemos que saber qué Enemigo fue (enum)
     *  - Tenemos que saber si era un boss o no (bool)
     *  
     */
}

/// <summary>
/// Los enemigos que hay en el juego
/// </summary>
public enum EnemyName{
    MOND,
    PLUR
}