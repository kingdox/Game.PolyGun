#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
public class Equipment : MonoX, IEquipment
{
    #region Variables
    /// <summary>
    /// Contenedor del tipo, TODO en vez de un string contendrá el
    /// tipo de fragmento o modulo....
    /// </summary>
    public string[] slots = new string[3];

    /*
     * TODO
     * Aqui vamos ha hacer el manejo de los Equipments encontrados
     * Este script:
     * - Almacena hasta 3 slots los items
     * - Al conseguir los 3, dependiendo del tipo genera una cosa o otra
     *  enviando una acción...
     * 
     *  * _ Recoger objeto
     * _ Consumir objeto
     * 
     */
    #endregion
    #region Events
    private void Awake(){
        
        New(out slots, 3);
        
    }
    #endregion
    #region Methods

    /// <summary>
    /// Dependiendo del Slot Seleccionado podremos Tomar un objetoo y guardarlo
    /// o podemos usar el objeto
    /// </summary>
    public void Action(int i){
        //TODO falta un struct para el manejo de los items
        //(tanto obj como Buff, de ser buff mostrar su tipo)


    }

    /// <summary>
    /// Colocamos el Item encontrado del suelo, independiente de si es
    /// un tipo <see cref="ShapesType"/> o si es un <see cref="BuffType"/>
    /// </summary>
    public void SetItem(int i)
    {

    }

    public void UseSlot(int i)
    {

    }

    #endregion
}

public interface IEquipment{

    //Revisa el i y dependiendo buscará en el slot
    void Action(int i);

    //coloca el nuevo obj encontrado del suelo
    void SetItem(int i);

    void UseSlot(int i);
}




/// <summary>
/// Objeto que tenemos en la escena de juego que nos dará la información
/// del item, su tipo, y su numero (1,3 si es shape) (1,5 si es Buff)
/// </summary>
public struct Item{

    public ItemType type;
    public ItemContent content;

    public Item( ItemType type, ItemContent content)
    {
        this.type = type;
        this.content = content;
    }

}






public enum ItemType{
    SHAPES,
    BUFFS
}

/// <summary>
/// Contenido de el Item
/// </summary>
public enum ItemContent{
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

/*
 * TODO resolver de objetos
 Los objetos cercanos se detectarán:
    Todos verán la posición del player, pero si hay uno más cerca
    Entonces se añade al "Mas cercano actual" si este se aleja del rango ó
    otro de estos obj encuentra más cercania que el anterior lo setea
 
 */

