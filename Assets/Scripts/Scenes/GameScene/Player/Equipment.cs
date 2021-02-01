#region Imports
using UnityEngine;
using Environment;
using XavLib;
using Crafts;

#endregion
public class Equipment : MonoX
{
    #region Variables


    [Header("Equipment settings")]
    public PlayerDetector detector;
    [Space]
    private bool canCraft = false;
    public float timer_craft = Data.data.timeToCraft;

    private float timeCount_craft = 0;
    private ItemContent[] slots;
    //cuenta la cantidad de objetos que posee actualmente
    private int equipedQty = 0;

    public CraftType craftWaiting = CraftType.NO;

    #endregion
    #region Events
    private void Start()
    {
        New(out slots, 3);
        ClearSlots();
        //Get(out detector);
        craftWaiting = CraftType.NO;
    }
    private void Update(){

        //cuidaremos que se actualice el flag para poder crear tras el tiempo...
        CanPassedTime(ref canCraft, ref timeCount_craft, timer_craft);

        //si tenemos equipado la misma cantidad del slot limite
        //checkearemos
        if (canCraft && equipedQty.Equals(slots.Length) ){

            CraftType type = Data.data.SlotsMatch(slots);

            if (!type.Equals(CraftType.NO))
            {
                PrintX($"Match : {type}");
                canCraft = false;
                ClearSlots();
                craftWaiting = type;
            }



        }

    }
    #endregion
    #region Methods

    /// <summary>
    /// If exist a craft waiting to, calls with the parent and based on the properties
    /// Build a thing
    /// </summary>
    public void WaitedCraft(ref Character character, ref ItemBuff[] buffs)
    {
        if (craftWaiting.Equals(CraftType.NO)) return;


        switch (craftWaiting)
        {
            //3
            case CraftType.AAA:
                buffs[(int)BuffType.STREGHT].StartBuff(3);
                break;
            case CraftType.BBB:
                buffs[(int)BuffType.ATK_SPEED].StartBuff(3);
                break;
            case CraftType.CCC:
                buffs[(int)BuffType.SPEED].StartBuff(3);
                break;
            // 2
            // A
            case CraftType.AAB:
                buffs[(int)BuffType.TARGET_SHOT].StartBuff(3);
                break;
            case CraftType.AAC:
                AllyManager.GenerateAlly(transform, AllyType.TRI_SHOT);
                break;
            //B
            case CraftType.BBA:
                AllyManager.GenerateAlly(transform, AllyType.HEARTH);
                break;
            case CraftType.BBC:
                AllyManager.GenerateAlly(transform, AllyType.POL);
                break;
            //C
            case CraftType.CCA:
                buffs[(int)BuffType.FROST].StartBuff(3);
                break;
            case CraftType.CCB:
                AllyManager.GenerateAlly(transform, AllyType.ROMB);
                break;
            // 1
            case CraftType.ABC:
                AllyManager.GenerateAlly(transform, AllyType.BOXBOX);
                break;

            case CraftType.EXTRA:
                //activates all the buffs
                foreach (ItemBuff buff in buffs)
                {
                    buff.StartBuff();
                }
                break;
        }


        //y al final
        craftWaiting = CraftType.NO;
    }


    /// <summary>
    /// Dependiendo del Slot Seleccionado podremos Tomar un objeto y guardarlo
    /// o podemos usar el objeto
    /// </summary>
    public ActionType Action(int i){

        ActionType actionType = new ActionType(
            slots[i]
            , !slots[i].Equals(ItemContent.NO)
        ); 

        //si NO Esta siendo USADO
        if (!actionType.used){
            //asignas el objeto encontrado
            detector.SetNearestItemType(ref slots[i]);

            if (!slots[i].Equals(ItemContent.NO))
            {
                equipedQty++;
            }
        }else{
            //si esta siendo usado lo consumimos
            equipedQty--;
            slots[i] = ItemContent.NO;
        }

        return actionType;
    }

    /// <summary>
    /// Devuelve los slots actuales de equipamiento
    /// </summary>
    public ItemContent[] GetSlots() => slots;

    /// <summary>
    /// returns the first void slot
    /// </summary>
    public int GetVoidSlotIndex()
    {
        int index = -1;

        for (int i = 0; i < slots.Length; i++)
        {
            //if is the first and is void
            if (index.Equals(-1) && slots[i].Equals(ItemContent.NO))
            {
                index = i;
            }
        }

        return index;
    }
    /// <summary>
    /// Returns the first slot with a buff
    /// </summary>
    public int GetBuffSlotIndex()
    {
        int index = -1;




        return index;
    }

    /// <summary>
    /// Limpiamos los slots y los dejamos con <see cref="ItemContent.NO"/>
    /// </summary>
    private void ClearSlots()
    {
        slots = XavHelpTo.Set.FillWith(ItemContent.NO, slots);
        equipedQty = 0;
    }
    #endregion
}

public struct ActionType {
    public ItemContent item;
    public bool used;
    public ActionType(ItemContent item, bool used){
        this.item = item;
        this.used = used;
    }

    /// <summary>
    /// Pregunta si la acción es con un Item
    /// </summary>
    public bool IsItem()
    {
        return XavHelpTo.Know.IsEqualOf(item,
                    ItemContent.SQUARE,
                    ItemContent.CIRCLE,
                    ItemContent.TRIANGLE
                );
    }

    public BuffType ToBuffType()
    {
        BuffType type = BuffType.NO;

        //Boilerplate....
        switch (item)
        {
            case ItemContent.ATK_SPEED:
                type = BuffType.ATK_SPEED;
                break;
            case ItemContent.TARGET_SHOT:
                type = BuffType.TARGET_SHOT;
                break;
            case ItemContent.FROST:
                type = BuffType.FROST;
                break;
            case ItemContent.STREGHT:
                type = BuffType.STREGHT;
                break;
            case ItemContent.SPEED:
                type = BuffType.SPEED;
                break;
            default:
                break;
        }

        return type;
    }


}

