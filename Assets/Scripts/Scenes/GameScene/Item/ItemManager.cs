#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavLib;
#endregion
[RequireComponent(typeof(Spawner))]
public class ItemManager : MonoX
{
#region
    [Header("Item Manager Settings")]

    public GameObject[] prefs_Item;
    public Spawner spawner;
    [Space]
    private readonly SpawnOpt[] spawnPatron = {SpawnOpt.NEAR,SpawnOpt.RANDOM, SpawnOpt.RANDOM, SpawnOpt.RANDOM, SpawnOpt.RANDOM};
    private int spawnOrder = 0;
    [Space]
    //Temporizador
    public float timerSpawn = 5f;
    private float count;
    [Space]
    //si sobrepasa o alcanza el numero deja de producir
    public int itemsLimit = 20;

    #endregion
    #region
    private void Update()
    {
        //Revisa si ha pasado el tiempo, cuando llega a 0 entonces ejecuta
        if (
            GameManager.IsOnGame()
            && Timer(ref count, timerSpawn)
            && spawner.GetActualQty() < itemsLimit // si supera el limite
            ){
            SpawnItem();
        }
        
    }
    #endregion
    #region Methods



    private void SpawnItem()
    {
        int selected = XavHelpTo.Get.ZeroMax(prefs_Item.Length);
        spawnOrder = XavHelpTo.Know.NextIndex(true, spawnPatron.Length, spawnOrder);

        spawner.Generate(prefs_Item[selected], spawnPatron[spawnOrder]);
    }
    #endregion
}
