#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavLib;
#endregion
public class Navigator : MonoBehaviour
{
    #region Var
    //Script especializado en los cambios entre sus "Paginas"
    //Dependiendo de lo existente, puedes manejar los botones "de navegación"
    //para que te dirija a la especificada
    private int indexActual = 0;
    [Header("Navigator Buttons")]
    public Button btn_L;
    public Button btn_R;
    [Space]
    [Header("Settings")]
    //-> objetos que se interpretan como "paginas"
    public GameObject[] pages;
    //-> revisamos si puede pasar los limites o no(osea volver al inicio o no)
    public bool haveBounds = false;
    #endregion
    #region Events
    private void Awake()
    {
        //se coloca en la pagina 0 antes que nada
        indexActual = 0;
        XavHelpTo.Change.ActiveObjectsExcept(pages);
        NavButtonsChecker();
    }
    #endregion
    #region Methods
    /// <summary>
    /// Revisamos si vamos hacia adelante o no
    /// </summary>
    public void _NavigateTo(bool goForward) {
        indexActual = XavHelpTo.Know.NextIndex(goForward, pages.Length, indexActual);
        XavHelpTo.Change.ActiveObjectsExcept(pages, indexActual);
        //Detectamos los limites
        NavButtonsChecker();
    }
    /// <summary>
    /// Revisamos si el indice ha llegado a uno de los limites,
    /// de ser así, dependiendo del boton se activará o desactivará
    /// </summary>
    private void NavButtonsChecker()
    {
        if (!haveBounds) return;
        btn_L.interactable = indexActual != 0;
        btn_R.interactable = indexActual != pages.Length - 1;
    }
    #endregion
}
