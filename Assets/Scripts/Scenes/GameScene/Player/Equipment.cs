#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavLib;
#endregion
public class Equipment : MonoX
{
    #region Variables

    public bool canCraft = false;

    private ItemContent[] slots;
    private PlayerDetector detector;
    //cuenta la cantidad de objetos que posee actualmente
    private int equipedQty = 0;

    private float timeCount_craft = 0;
    private float timer_craft = 5f;

    #endregion
    #region Events
    private void Start()
    {
        New(out slots, 3);
        ClearSlots();
        Get(out detector);
        equipedQty = 0;
    }
    private void Update(){

        //cuidaremos que se actualice el flag para poder crear tras el tiempo...
        CanPassedTime(ref canCraft, ref timeCount_craft, timer_craft);

        //si tenemos equipado la misma cantidad del slot limite
        //checkearemos
        if (canCraft && equipedQty.Equals(slots.Length) ){
            //...
        }

    }
    #endregion
    #region Methods

    

    /// <summary>
    /// Dependiendo del Slot Seleccionado podremos Tomar un objeto y guardarlo
    /// o podemos usar el objeto
    /// </summary>
    public ActionType Action(int i){

        ActionType actionType = new ActionType(
            slots[i]
            , !slots[i].Equals(ItemContent.NO)
        ); 

        //si no Esta siendo USADO
        if (!actionType.used){
            //asignas el objeto encontrado
            detector.SetNearestItemType(ref slots[i]);
            equipedQty++;
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
    private void ClearSlots() => slots = XavHelpTo.Set.FillWith(ItemContent.NO, slots);
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
                    ItemContent.CIRCLE,
                    ItemContent.TRIANGLE,
                    ItemContent.SQUARE
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