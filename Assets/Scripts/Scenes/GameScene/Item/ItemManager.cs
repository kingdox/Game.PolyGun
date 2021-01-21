using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Spawner))]
public class ItemManager : MonoX
{
    /*
     * TODO
     * 
     * qué posee:
     *  - posee los prefabs 3D de cada uno
     *  - posee el controlador de spawns de Items (sus posiciones)
     * 
     * 
     * qué puede hacer?:
     */
    //⚠️Nescesario

    /// <summary>
    /// conjunto de items en un array y al llamaro tiene en un sitio y cosa distinta
    /// </summary>
    public GameObject[] prefs_Item;
    public Spawner p;
}
