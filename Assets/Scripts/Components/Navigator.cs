#region Imports
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo.Know;
using XavHelpTo.Change;
#endregion
public class Navigator : MonoBehaviour
{
    #region Var
    //Script especializado en los cambios entre sus "Paginas"
    //Dependiendo de lo existente, puedes manejar los botones "de navegación"
    //para que te dirija a la especificada
    public int indexActual = 0;
    [Space]
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
    private void Start()
    {
        indexActual = 0;

        if (!pages.Length.Equals(0))
        {
            RefreshPages();
        }
        else
        {
            pages = new GameObject[0];
            SetBtnAct(btn_L);
            SetBtnAct(btn_R);
        }
    }
    #endregion
    #region Methods
    /// <summary>
    /// Actualizamos las paginas y mostramos una de estas
    /// </summary>
    public void RefreshPages(int i = 0)
    {
        Change.ActiveObjectsExcept(pages,i);
        NavButtonsChecker();
    }
    /// <summary>
    /// Agregamos las nuevas paginas como referencias
    /// </summary>
    public void SetPages(GameObject[] newPages, int i = 0)
    {
        //si vemos que hay uno entrante
        if (!newPages.Length.Equals(0))
        {
            Change.ActiveObjectsExcept(pages, -1);
            //pages[indexActual].SetActive(false);
            pages = newPages;
            indexActual = i;
        }

        RefreshPages(i);
    }
    /// <summary>
    /// Revisamos si vamos hacia adelante o no
    /// </summary>
    public void _NavigateTo(bool goForward) {

        if (goForward && !btn_R.interactable) return;
        if (!goForward && !btn_L.interactable) return;


        indexActual = Know.NextIndex(goForward, pages.Length, indexActual);
        Change.ActiveObjectsExcept(pages, indexActual);
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
        SetBtnAct(btn_L, indexActual != 0);
        SetBtnAct(btn_R, indexActual != pages.Length - 1);
    }
    /// <summary>
    /// Ingresa la actividad del botón
    /// </summary>
    private void SetBtnAct(Button btn, bool condition = false) => btn.interactable = condition;
    /// <summary>
    /// Tomamos el indice actual
    /// </summary>
    public int GetIndexActual() => indexActual;
    #endregion
}
