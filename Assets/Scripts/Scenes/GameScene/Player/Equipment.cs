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
    /// Dependiendo del Slot Seleccionado podremos Tomar un objetoo y guardarlo
    /// o podemos usar el objeto
    /// </summary>
    public void Action(int i){
        if (i.Equals(-1)) return;

        //si no hay coloca
        if (slots[i].Equals(ItemContent.NO)){
            //asignas el objeto encontrado
            detector.SetNearestItemType(ref slots[i]);
            equipedQty++;

        }
        else{
            equipedQty--;

            //si hay lo usa
            //usandolo.....

            //TODO
            PrintX($"Se usó {slots[i]}");
            slots[i] = ItemContent.NO;

        }

    }
    /// <summary>
    /// Limpiamos los slots y los dejamos con <see cref="ItemContent.NO"/>
    /// </summary>
    private void ClearSlots() => slots = XavHelpTo.Set.FillWith(ItemContent.NO, slots);
    #endregion
}

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
/// <summary>
/// Tipos de enemigos en el juego
/// </summary>
public enum EnemyType{
    PLUR,
    MOND
}