using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Translate;

/// <summary>
/// Poseedor de las traducciones
/// puede que para mas idiomas
/// </summary>
public class Translator
{
    [HideInInspector]
    public static Translator _ = new Translator();


    /// <summary>
    /// Idioma actual, por defecto es español
    /// </summary>
    public static Idiom idiom = Idiom.es;

    /// <summary>
    /// Valores en español con los textos
    /// No usar directamente.
    /// </summary>
    private readonly string[] value = {};

    /// <summary>
    /// Busca en un segmento especificado de llaves
    /// y te trae alguna de estas en caso de llegar a un limite.
    /// </summary>
    /// <returns>La traducción de un valor del segmento establecido</returns>
    public static string ClampKey(TKey valueKey, TKey[] segmenKey) => _.value[ Mathf.Clamp((int)valueKey,(int)segmenKey[0],(int)segmenKey[segmenKey.Length - 1])];

    /// <summary>
    /// Te devuelve el resultado de los datos para
    /// la traducción especificadaa
    /// </summary>
    /// <returns>La traducción de un valor</returns>
    public static string Trns(TKey enumKey) => _.value[(int)enumKey];


}
