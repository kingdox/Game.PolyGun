#region Imports
using UnityEngine;
using XavHelpTo.Build;
using XavHelpTo.Know;
#endregion
namespace Crafts
{
    public class CraftData
    {
        #region Imports
        //START
        private delegate Craft CraftRecipe(CraftType type, params ItemContent[] values);
        private readonly static CraftRecipe craft = (CraftType type, ItemContent[] values) => new Craft(type, values);
        //END
        public static readonly ItemContent[] itemsShape = { ItemContent.SQUARE, ItemContent.CIRCLE, ItemContent.TRIANGLE };
        public static readonly ItemContent[] itemsBuff = { ItemContent.ATK_SPEED, ItemContent.TARGET_SHOT, ItemContent.FROST, ItemContent.STREGHT, ItemContent.SPEED };
        private static readonly Craft[] crafts;

        static CraftData()
        {
            crafts = new Craft[]{
                //full 3
                craft(CraftType.AAA,    ItemContent.SQUARE,ItemContent.SQUARE,ItemContent.SQUARE),
                craft(CraftType.BBB,    ItemContent.CIRCLE,ItemContent.CIRCLE,ItemContent.CIRCLE),
                craft(CraftType.CCC,    ItemContent.TRIANGLE,ItemContent.TRIANGLE,ItemContent.TRIANGLE),

                // 2 y 1
                // A
                craft(CraftType.AAB,    ItemContent.SQUARE,ItemContent.SQUARE,ItemContent.CIRCLE),
                craft(CraftType.AAC,    ItemContent.TRIANGLE,ItemContent.SQUARE,ItemContent.SQUARE),
                // B
                craft(CraftType.BBA,    ItemContent.SQUARE, ItemContent.CIRCLE,ItemContent.CIRCLE),
                craft(CraftType.BBC,    ItemContent.TRIANGLE,ItemContent.CIRCLE,ItemContent.CIRCLE),
                // C        
                craft(CraftType.CCA,    ItemContent.SQUARE, ItemContent.TRIANGLE,ItemContent.TRIANGLE),
                craft(CraftType.CCB,    ItemContent.CIRCLE, ItemContent.TRIANGLE,ItemContent.TRIANGLE),

                // 1,1,1
                craft(CraftType.ABC,    ItemContent.SQUARE,ItemContent.CIRCLE, ItemContent.TRIANGLE),

                //Especial
                craft(CraftType.EXTRA,  ItemContent.ATK_SPEED, ItemContent.TARGET_SHOT, ItemContent.FROST, ItemContent.STREGHT, ItemContent.SPEED)

            };
        }
        #endregion
        #region Methods

        /// <summary>
        /// try to match with a craft
        /// </summary>
        public CraftType MatchType(ItemContent[] slots)
        {
            CraftType result = CraftType.NO;


            //preguntar si no posee items, entonces es buff


            foreach (Craft c in crafts)
            {
                if (result.Equals(CraftType.NO) && c^slots)
                {
                    Debug.Log($"{c.type} hizo match");
                    result = c.type;
                }
            }
            return result;
        }

        #endregion
    }
    /// <summary>
    /// contains the info of how to craft with the itemcontents
    /// </summary>
    public struct Craft
    {
        public readonly CraftType type;
        public readonly ItemContent[] values;//los requeridos

        public Craft(CraftType type, ItemContent[] values)
        {
            this.type = type;
            this.values = values;
        }

        /// <summary>
        /// Revisa si ambos arreglos son iguales
        /// </summary>
        public static bool operator ^(Craft a, ItemContent[] b) {
            bool isSame = true;
            for (int x = 0; x < b.Length; x++)
            {


                if (isSame)
                {

                    //valor que estamos identificando la cantidad de cada sitio
                    ItemContent value = b[x];

                    //cantidad de veces repetidas del valor en los slots
                    int _craft_repeats = value.RepeatsIn(a.values);
                    //cantidad de veces repetidas del valor en el craft
                    int _slot_repeats = value.RepeatsIn(b);


                    ///si tanto en slots como en craft tienen la misma cantidad entonces continuen
                    if (!_slot_repeats.Equals(_craft_repeats))
                    {
                        isSame = false;
                    }
                }


            }


            return isSame;
        }
        
    }

    /// <summary>
    /// Combinations to craft
    /// </summary>
    public enum CraftType
    {
        NO = -1,

        AAA, BBB, CCC,
        AAB, AAC, BBA, BBC, CCA, CCB,
        ABC,

        EXTRA//when you get full with buffs
    }

}