#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#endregion
public class ItemBuff : MonoX
{
    #region Variables
    [Header("ItemBuff settings")]
    public BuffType buff = BuffType.NO;

    //valor que vamos a guardar, este valor será el que está siendo alterado...
    public float savedValue = -1;
    //si es -1 entonces otorga
    public float buffValue = -1;
    //Cuenta la cantidad de Buffs aplicados

    public int stacks = 0;
    [Space]
    public bool isRunning = false;
    //de momento 10 s
    public float timer = 10f;
    public float count = 0;

    private bool inited = false;


    #endregion
    #region Events
    #endregion
    #region Methods

    /// <summary>
    /// Usar en un Update:
    /// <para>
    /// Asigna 
    /// </para>
    /// </summary>
    private void CheckBuffUpdates(ref float val, float addVal = default)
    {
        if (addVal.Equals(default)) addVal = buffValue;

        //Si estamos corriendo y cuando pasen .... segundos
        if (BuffUpdate())
        {
            val = savedValue + addVal;
        }
        else
        {
            if (!inited)
            {
                savedValue = val;
                inited = true;
            }
            else
            {
                val = savedValue;
            }
        }
    }


    /// <summary>
    /// Revisa y actualiza el buff por el tiempo definido
    /// devuelve el valor de <see cref="isRunning"/>
    /// </summary>
    public bool BuffUpdate()
    {
        if (isRunning && Timer(ref count, timer))
        {
            stacks--;
            if (stacks.Equals(0))   
            {
                isRunning = false;

            }
            //savedValue = -1;

        }
        return isRunning;
    }


    /// <summary>
    /// Aplica el Buff correspondiente para el character
    /// el resto no interactua...
    /// Devuelve true si es un tipo de excepción
    /// </summary>
    public bool CanApplyBuff(ref Character character)
    {
        bool canApply = true;

        switch (buff)
        {
            case BuffType.ATK_SPEED:
                //reducimo la cantidad para aumentar la cadencia
                CheckBuffUpdates(ref character.atkSpeed, -buffValue);
                break;
            case BuffType.TARGET_SHOT:
                character.canExtraShots = BuffUpdate();
                //canApply = false;
                break;
            case BuffType.FROST:
                character.isInmortal = BuffUpdate();
                //canApply = false;
                break;
            case BuffType.STREGHT:
                CheckBuffUpdates(ref character.damage);
                break;
            case BuffType.SPEED:
                CheckBuffUpdates(ref character.speed);
                break;
            default:
                //No buff
                PrintX("ItemBuff -> ERROR, no asignaste el buff al itemBuff");
                break;
        }

        //devolvemos informando si hay escepciones o no
        return canApply;
    }

    public void StartBuff()
    {
        //si hay otro buff lo checkeamos y añadimos como pila
        //de momento lo resetea
        //count = 0;
        stacks++;
        isRunning = true;
    }

    #endregion
}

/// <summary>
/// Muestra el tipo de buff existentes en el juego
/// </summary>
public enum BuffType
{
    NO = -1,

    ATK_SPEED,
    TARGET_SHOT,
    FROST,
    STREGHT,
    SPEED
}





/*
LOS buff tienen un corto periodo de tiempo pero
cuando se consumen individualmente estos aumentan en x4 ns

extra crafting 5s 
cada uno separado 15 / 20

 
 */
