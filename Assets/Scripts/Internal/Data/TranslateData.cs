using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Translate
{

    public class TranslateData : MonoBehaviour
    {

        private readonly string[] es =
        {
            "Español",
                "Jugar","Introducción","Logros","Opciones","Salir","Retroceder"
        };
        private readonly string[] en =
        {
            "English",
                "Play","Introduction","Achievements","Options","Exit","Back",
        };
    }
    /// <summary>
    /// Idiomas que contendrá el juego
    /// </summary>
    public enum Idiom {
        es,
        en
    }
    /// <summary>
    /// Contenedor de las llaves para traducir dependiendo del <see cref="Idiom"/>
    /// </summary>
    public enum TKey {

        // IDIOM
        _IDIOM,
        // BUTTONS
        _0_PLAY,
        _1_INTRODUCTION,
        _2_ACHIEVEMENTS,
        _3_OPTIONS,
        _4_EXIT,
        _5_BACK,

    }
}