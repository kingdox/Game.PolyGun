#region Imports
using System;
using UnityEngine;
using XavLib;
#endregion

public class Item : MonoX
{
    #region Variables
    [Header("Item Settings")]
    public ItemContent type;
    public ParticleSystem part_selected;
    public bool Isselected = false;

    public static event Action Selection;
    #endregion
    #region Events
    private void Update()
    {
        XavHelpTo.Change.ActiveParticle(part_selected, Isselected);
    }
    #endregion
    #region Methods
    #endregion
}
