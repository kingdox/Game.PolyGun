using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Poseedor de las traducciones
/// puede que para mas idiomas
/// </summary>
public class Translator
{
    [HideInInspector]
    public static Translator _ = new Translator();
    private enum Idioms { es, en}


    /// <summary>
    /// Valores en español con los textos
    /// No usar directamente.
    /// </summary>
    private readonly string[] value =
    {
        //General
        "Metros: ",
        "Monstruos: ",
        "Monedas: ",

        // Simbolos
        "M",
        "$",

        // Characters
        "Monje",
        "Paladín",
        "Cazador",
        "Barbaro",

        // PowDescription
        "Aumenta la velocidad gradualmente.",
        "Reestablece la energía poco a poco.",
        "Permite dar saltos dobles.",
        "Atrae más monstruos.",

        // Buff ???
       "Energía",
       "Agilidad",
       "Escudo"
    };

    /// <summary>
    /// Busca en un segmento especificado de llaves
    /// y te trae alguna de estas en caso de llegar a un limite.
    /// </summary>
    /// <param name="valueKey"></param>
    /// <param name="segmenKey"></param>
    /// <returns>La traducción de un valor del segmento establecido</returns>
    public static string ClampKey(TKey valueKey, TKey[] segmenKey) => _.value[ Mathf.Clamp((int)valueKey,(int)segmenKey[0],(int)segmenKey[segmenKey.Length - 1])];

    /// <summary>
    /// Te devuelve el resultado de los datos para
    /// la traducción especificadaa
    /// </summary>
    /// <param name="enumKey"></param>
    /// <returns>La traducción de un valor</returns>
    public static string Trns(TKey enumKey) => _.value[(int)enumKey];


}

/// <summary>
/// Contenedor de las llaves de los translate comunes
/// </summary>
public enum TKey
{
    //General
    Metters,
    Monsters,
    Money,

    // Simbolos
    SIGN_Metters,
    SIGN_Money,

    // Characters
    CHAR_Monk,
    CHAR_Paladin,
    CHAR_Hunter,
    CHAR_Brutus,

    // PowDescription
    POW_Monk,
    POW_Paladin,
    POW_Hunter,
    POW_Brutus,

    // Buffs
    BUFF_Energy,
    BUFF_Speed,
    BUFF_Shield
}