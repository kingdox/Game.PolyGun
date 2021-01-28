#region Imports
using UnityEngine;
using XavLib;
#endregion

public class Item : MonoBehaviour
{
    #region Variables
    [Header("Item Settings")]
    public ItemContent type;
    public ParticleSystem part_selected;
    public bool Isselected = false;

    #endregion
    #region Events
     private void Update()
    {
        XavHelpTo.Change.ActiveParticle(part_selected, Isselected);
        if (!Isselected)
        {
            part_selected.Clear();
        }
    }
    #endregion
    #region Methods
    #endregion
}
