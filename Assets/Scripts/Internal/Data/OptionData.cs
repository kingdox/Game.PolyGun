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
        public static readonly float[] musicVolume = { 0, .3f, .5f, .7f }; // el audiosource lo maneja en unit
        public static readonly float[] sfxVolume = { 0, 70 };
        public static readonly int controls = Key.KeyData.CODE_LENGTH;// longitud de cantidad de controlesps4?
        //public static readonly int[] LEFTOVER_QTY = {0,50,250, 500 };
                                                //
        private static readonly TKey[] msgOpt = {
            TKey.MSG_OPT_LANGUAGE,
            TKey.MSG_OPT_TEXTSPEED,
            TKey.MSG_OPT_MUSIC,
            TKey.MSG_OPT_SOUND,
            TKey.MSG_OPT_CONTROLS,
            TKey.MSG_OPT_BACK,
        };
        private static readonly TKey[] msg_Idiom = {
            TKey._IDIOM,
            TKey._IDIOM,
        };
        private static readonly TKey[] msg_textSpeed = {
            TKey.MSG_OPT_TEXTSPEED_INSTANT,
            TKey.MSG_OPT_TEXTSPEED_FAST,
            TKey.MSG_OPT_TEXTSPEED_NORMAL,
            TKey.MSG_OPT_TEXTSPEED_SLOW,
        };
        private static readonly TKey[] msg_musicVolume = {
            TKey.MSG_OPT_MUSIC_NO,
            TKey.MSG_OPT_MUSIC_LOW,
            TKey.MSG_OPT_MUSIC_NORMAL,
            TKey.MSG_OPT_MUSIC_HIGH
        };
        private static readonly TKey[] msg_sfxVolume = {
            TKey.MSG_OPT_SOUND_NO,
            TKey.MSG_OPT_SOUND_YES,
        };
        private static readonly TKey[] msg_controls = {
            TKey.MSG_OPT_CONTROLS_NORMAL,
            TKey.MSG_OPT_CONTROLS_INVERT,
            //TKey.MSG_OPT_CONTROLS_NORMAL
        };
        #endregion
            #region METHODS
            /// <summary>
            /// Dependiendo de la opcion, manejaremos busqueda en una o otra
            /// </summary>
        public static string GetValueMsg(Option opt){

            SavedData saved = DataPass.GetSavedData();

            TKey[][] indexMatrix = {
                msg_Idiom,
                msg_textSpeed,
                msg_musicVolume,
                msg_sfxVolume,
                msg_controls
            };
            int[] indexValue = {
                saved.idiom,
                saved.textSpeed,
                saved.musicVolume,
                saved.sfxVolume,
                saved.control
            };

            return opt.Equals(Option.BACK) ? ""
                : Data.Translated().Value(
                    indexMatrix[(int)opt][indexValue[(int)opt]]
            );
        }
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