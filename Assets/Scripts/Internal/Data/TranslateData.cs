using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Translate
{

    public class TranslateData {

        //START
        private delegate Language LanguageRecipe(Idiom idiom, params string[] values);
        private readonly static LanguageRecipe lang = (Idiom idiom, string[] values) => new Language(idiom, values);
        //END

        private static readonly string[] es =
        {
            "Español",
                "Jugar","Introducción","Logros","Opciones","Salir","Retroceder"
        };
        private static readonly string[] en =
        {
            "English",
                "Play","Introduction","Achievements","Options","Exit","Back",
        };

        private static readonly Language[] languages;

        //Armamos los idiomas disponibles en un arreglo
        static TranslateData(){
            languages = new Language[]{
                lang(Idiom.es,es),
                lang(Idiom.en,en),
            };
        }

        /// <summary>
        /// Buscamos El <see cref="Language"/> del <see cref="Idiom"/>
        /// </summary>
        public Language Get(Idiom i) => Get((int)i);
        public Language Get(int i) => languages[i];
    }
    /// <summary>
    /// Contenedor del idioma correspondiente y sus valores traducidos a ese idioma
    /// <para>usa <seealso cref="Idiom"/> para definir el idioma </para>
    /// </summary>
    public struct Language{
        private readonly Idiom idiom;
        private readonly string[] values;
        public Language(Idiom idiom, string[] values){
            this.idiom = idiom;
            this.values = values;
        }
        public string Value(TKey k) => values[(int)k];
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
        No = -1, // en caso de que tengamos que declarar que no hay
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