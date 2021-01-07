using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Translate;
using Environment;
namespace Options
{
    /// <summary>
    /// Información que apoya con el manejo de datos
    /// <para>Dependencia con <see cref="TranslateData"/> y <see cref="Data"/></para>
    /// </summary>
    public class OptionData {
        #region VARIABLES
        /*
         * Datos principales
         */
        //[OPTIONS]
        public static readonly float[] textSpeed = { 0, .005f, .015f, .025f };
        public static readonly float[] musicVolume = { 0, 30, 50, 70 };
        public static readonly float[] sfxVolume = { 0, 70 };
        public static readonly int controls = 1;// longitud de cantidad de controlesps4?
                                                //
        private static readonly TKey[] msgOpt = {
            TKey.MSG_OPT_LANGUAGE,
            TKey.MSG_OPT_TEXTSPEED,
            TKey.MSG_OPT_MUSIC,
            TKey.MSG_OPT_SOUND,
            TKey.MSG_OPT_CONTROLS,
            TKey.MSG_OPT_BACK,
        };

        //TODO revisar como hacer el manejo de distintos valores
        // usar keys? o mostrar
        //private static readonly TKey[] optVal = {
        //    TKey._IDIOM // valor de 
        //}
        #endregion
            #region METHODS





            /// <summary>
            /// Mediante la opcion, consigues la llave para sacar la traducción
            /// del mensaje
            /// </summary>
        public static string GetMsgOfOpt(Option opt) => Data.Translated().Value(msgOpt[(int)opt]);
        #endregion
    }


    //Opciones que hay en el menú de opciones
    public enum Option
    {
        LANGUAGE,
        TEXTSPEED,
        MUSIC,
        SOUND,
        CONTROLS,
        BACK
    }
}