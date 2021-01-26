#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;
using XavLib;
using Crafts;

#endregion
public class Equipment : MonoX
{
    #region Variables

    public bool canCraft = false;

    [SerializeField]
    public ItemContent[] slots;
    private PlayerDetector detector;
    //cuenta la cantidad de objetos que posee actualmente
    public int equipedQty = 0;

    private float timeCount_craft = 0;
    private float timer_craft = 5f;

    //TODO test
    public CraftType craftWaiting = CraftType.NO;

    #endregion
    #region Events
    private void Start()
    {
        New(out slots, 3);
        ClearSlots();
        Get(out detector);
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
            case CraftType.BBB:
            case CraftType.CCC:

            // 2
            // A
            case CraftType.AAB:
            case CraftType.AAC:

            //B
            case CraftType.BBA:
            case CraftType.BBC:

            //C
            case CraftType.CCA:
            case CraftType.CCB:

            // 1
            case CraftType.ABC:
                break;

            case CraftType.EXTRA:
                //activates all the buffs
                foreach (ItemBuff buff in buffs)
                {
                    buff.StartBuff();
                }

                break;
            default:
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

//TODO aquí estan los ALLY
/// <summary>
/// Aliados posibles en el juego
/// </summary>
public enum AllyType{
    BOXBOX,
    TRI_SHOT,
    HEARTH,
    ROMB,
    POL
}

