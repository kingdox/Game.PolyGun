#region
using UnityEngine;
using XavLib;
using Environment;
#endregion
[RequireComponent(typeof(Spawner))]
public class ItemManager : MonoX
{
    #region
    private static ItemManager _;
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
    [Header("Debug")]
    private bool _Debug_CanNOTGenerate = false;

    #endregion
    #region
    private void Awake()
    {
        _ = this;
    }
    private void Update()
    {
        //Revisa si ha pasado el tiempo, cuando llega a 0 entonces ejecuta
        if (
            GameManager.IsOnGame()
            && Timer(ref count, timerSpawn)
            && TargetManager.GetItemsContainer().childCount < itemsLimit // si supera el limite
            ){
            SpawnItem();
        }
        
    }
    #endregion
    #region Methods


    /// <summary>
    /// Crea un Item y lo posiciona en alguno de los sitios correspondientes
    /// llamando al <see cref="Spawner"/>
    /// </summary>
    private void SpawnItem()
    {
        if (_Debug_CanNOTGenerate) return;

        int selected = -1;// XavHelpTo.Get.ZeroMax(prefs_Item.Length);

        //proabilidad de que salga item
        if (Random.Range(0,1f) < Data.data.itemShapeRate) {
            //busca solo items
            selected = XavHelpTo.Get.ZeroMax(3);
        }

        spawnOrder = XavHelpTo.Know.NextIndex(true, spawnPatron.Length, spawnOrder);
        GenerateItem(selected);
        //spawner.Generate(prefs_Item[selected], spawnPatron[spawnOrder], TargetManager.GetItemsContainer());
    }

    /// <summary>
    /// Generates the item selected
    /// </summary>
    public void GenerateItem(int selected)
    {

        if (selected.Equals((int)ItemContent.NO))    
        {
            //Random
            selected = XavHelpTo.Get.ZeroMax(prefs_Item.Length);
        }

        spawner.Generate(prefs_Item[selected], spawnPatron[spawnOrder], TargetManager.GetItemsContainer());

    }


    /// <summary>
    /// Devuelve uno de los items
    /// </summary>
    /// <returns></returns>
    public static GameObject GetRandomItemShape()
    {
        return _.prefs_Item[XavHelpTo.Get.ZeroMax(3)];
    }
    /// <summary>
    /// Set if you can generate or not
    /// </summary>
    public void __Debug_GeneratePlayStop()
    {
        _Debug_CanNOTGenerate = !_Debug_CanNOTGenerate;
    }

    /// <summary>
    /// Destroys all the items in scene
    /// </summary>
    public void __Debug_DestroyAll()
    {
        Transform container = TargetManager.GetItemsContainer();

        for (int x = 0; x < container.childCount; x++)
        {
            Destroy(container.GetChild(x).gameObject);
        }
    }
    #endregion
}


[SerializeField]
public enum ItemContent
{
    NO = -1,

    SQUARE,
    CIRCLE,
    TRIANGLE,

    /// Mejoras posibles en el juego
    ATK_SPEED,
    TARGET_SHOT,
    FROST,
    STREGHT,
    SPEED
}