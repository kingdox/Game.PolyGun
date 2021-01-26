#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
                craft(CraftType.AAC,    ItemContent.SQUARE,ItemContent.SQUARE,ItemContent.TRIANGLE),
                // B
                craft(CraftType.BBA,    ItemContent.CIRCLE,ItemContent.CIRCLE,ItemContent.SQUARE),
                craft(CraftType.BBC,    ItemContent.CIRCLE,ItemContent.CIRCLE,ItemContent.TRIANGLE),
                // C        
                craft(CraftType.CCA,    ItemContent.TRIANGLE,ItemContent.TRIANGLE,ItemContent.SQUARE),
                craft(CraftType.CCB,    ItemContent.TRIANGLE,ItemContent.TRIANGLE,ItemContent.CIRCLE),

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

            foreach (Craft c in crafts)
            {
                if (result.Equals(CraftType.NO))
                {   
                    result = c.KnowSlotsCraft(slots);
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
        private readonly CraftType type;
        private readonly ItemContent[] values;//los requeridos

        public Craft(CraftType type, ItemContent[] values)
        {
            this.type = type;
            this.values = values;
        }

        /// <summary>
        /// Based on the slots, look if is this craft match with the slots
        /// </summary>
        public CraftType KnowSlotsCraft(ItemContent[] slots)
        {

            CraftType craftType = type;

            for (int i = 0; i < slots.Length; i++)
            {
                //if find a repeated item then. no match
                if (RepeatContains(slots[i]).Equals(0) )
                {
                    craftType = CraftType.NO;
                }
            }
            return craftType;
        }

        /// <summary>
        /// Looka item and return the qty repeated here
        /// </summary>
        private int RepeatContains(ItemContent item)
        {
            int repeated = 0;
            foreach (ItemContent i in values){
                if (item.Equals(i))
                {
                    repeated++;
                }
            }
            return repeated;
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

    //public enum Shapes
    //{
    //    SQUARE,
    //    CIRCLE,
    //    TRIANGLE,
    //}

}