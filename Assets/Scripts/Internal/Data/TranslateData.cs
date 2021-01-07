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
            // IDIOM
            "Español",
                // BUTTONS
                "Jugar","Introducción","Logros","Opciones","Salir","Retroceder",
                "Idioma","Velocidad","Musica","Sonido","Controles",


                //Achievements
                //P1
                "Robots eliminados","Jefes eliminados","Oleada de enemigos","Objetos recogidos","Curaciónes en una partida",
                //P2
                "Tiempo al borde de morir","Metros recorridos en partida","Creaciones en una partida","Robots aliados con vida","*Lector*",

                //Messages TODO
                /*1*/"Donde estoy?,\n Como siempre.......... \nOscuro...... \nMuy Oscuro..........",
                /*2*/"DESCONOCIDOOOO000000",

                //Messages Opt TODO
                /*1*/"Idioma que estará el juego",
                /*2*/"Velocidad de los textos mostrandose",
                /*3*/"Volumen que sonará la musica",
                /*4*/"Habrá sonidos o no?",
                /*5*/"Controles con los cuales el jugador usará",
                /*6*/"Botón para retroceder",
        };
        private static readonly string[] en =
        {
            // IDIOM TODO
            "English",
                // BUTTONS
                "Play","Introduction","Achievements","Options","Exit","Back",
                "Idiom","Text Speed","Music","Sounds","Controls",

                //Achievements //TODO falta traducir
                //P1 TODO
                "Robots eliminados","Jefes eliminados","Oleada de enemigos","Objetos recogidos","Curaciónes en una partida",
                //P2 TODO
                "Tiempo al borde de morir","Metros recorridos en partida","Creaciones en una partida","Robots aliados con vida","*Lector*",

                //Messages TODO
                /*1*/"Donde estoy?,\n Como siempre.......... \nOscuro...... \nMuy Oscuro..........",
                /*2*/"DESCONOCIDOOOO000000",

                //Messages Opt TODO
                /*1*/"Idioma que estará el juego",
                /*2*/"Velocidad de los textos mostrandose",
                /*3*/"Volumen que sonará la musica",
                /*4*/"Habrá sonidos o no?",
                /*5*/"Controles con los cuales el jugador usará",
                /*6*/"Botón para retroceder",

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
        public int GetLangLength() => languages.Length;
    }
    /// <summary>
    /// Contenedor del idioma correspondiente y sus valores traducidos a ese idioma
    /// <para>usa <seealso cref="Idiom"/> para definir el idioma </para>
    /// </summary>
    public struct Language{
        private readonly Idiom idiom;// por si requiere uso posterior...
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
        _6_LANGUAGE,
        _7_TEXTSPEED,
        _8_MUSIC,
        _9_SOUND,
        _10_CONTROLS,

        //Achievements
        // P1 TODO
        ACHIEVE_KILLS_ROBOT,
        ACHIEVE_KILLS_BOSS,
        ACHIEVE_WAVES_ENEMIES,
        ACHIEVE_OBJECTS_COLLECTED,
        ACHIEVE_HEALS_GAME,
        // P2
        ACHIEVE_TIME_DEATHLIMIT,
        ACHIEVE_METTERS_GAME,
        ACHIEVE_CREATIONS_GAME,
        ACHIEVE_ROBOTS_ALIVE,
        ACHIEVE_ESPECIAL_READ,


        //Messages (Mensajes del cyborg...)
        MSG_CYBORG_WHERE,
        MSG_CYBORG_UNKNOW,

        //Messages Opt (Mensajes de las opciones
        MSG_OPT_LANGUAGE,
        MSG_OPT_TEXTSPEED,
        MSG_OPT_MUSIC,
        MSG_OPT_SOUND,
        MSG_OPT_CONTROLS,
        MSG_OPT_BACK,
    }
}