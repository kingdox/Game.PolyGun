using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Navigator : MonoBehaviour
{
    //Script especializado en los cambios entre sus "Paginas"
    //Dependiendo de lo existente, puedes manejar los botones "de navegación"
    //para que te dirija a la especificada

    [Header("Navigator Buttons")]
    public Button btn_L;
    public Button btn_R;


    [Header("Settings")]
    public GameObject[] pages;
    //-> revisamos si puede pasar los limites o no
    public bool haveBounds = false;
    private int indexActual = 0;

    private void Awake()
    {
        //se coloca en la pagina 0 antes que nada
        indexActual = 0;
        XavHelpTo.ActiveThisObject(pages);
        NavButtonsChecker();
    }



    /// <summary>
    /// Revisamos si vamos hacia adelante o no
    /// </summary>
    public void _NavigateTo(bool goForward) {
        indexActual = XavHelpTo.TravelArr(goForward, pages.Length, indexActual);
        XavHelpTo.ActiveThisObject(pages, indexActual);
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

}
