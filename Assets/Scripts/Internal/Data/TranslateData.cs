#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IdiomsES;
using IdiomsEN;
#endregion
namespace Translate
{

    public class TranslateData {
        #region Imports
        //START
        private delegate Language LanguageRecipe(Idiom idiom, params string[] values);
        private readonly static LanguageRecipe lang = (Idiom idiom, string[] values) => new Language(idiom, values);
        //END

        private static readonly Language[] languages;
        static TranslateData()
        {
            languages = new Language[]{
                lang(Idiom.es,Es.es),
                lang(Idiom.en,En.en),
            };
        }
        #endregion
        #region Methods
        /// <summary>
        /// Buscamos El <see cref="Language"/> del <see cref="Idiom"/>
        /// </summary>
        public Language Get(Idiom i) => Get((int)i);
        public Language Get(int i) => languages[i];
        public int GetLangLength() => languages.Length;
        #endregion
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
    public enum Idiom {es,en}
}