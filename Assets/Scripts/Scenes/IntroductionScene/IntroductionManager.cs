#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;
using XavLib;
#endregion
public class IntroductionManager : MonoManager
{
    #region Variables

    //ultima posición de cada opción
    [Header("Input Controller")]
    public MenuInputController menuInputC;
    public Navigator navigator;
    [Space]
    [Header("Pages")]
    public IntroductionPages[] introPages;
    //public GameObject[] tutorialPages;
    //public GameObject[] manualPages;
    //public GameObject[] creditsPages;
    public GameObject[][] metaPages;
    private int[] indexPages = {};
    private int lastIndex = 0;
    // usar un navigator para cambiar de paginas
    // usar el introduction Manager para setear las paginas
    //y reestablecerlo a 0 (o guardar las pos de cada pagina aquí)
    #endregion
    #region Events
    private void Start(){
        lastIndex = 0;
        metaPages = new GameObject[][]{
            introPages[0].pagesG,
            introPages[1].pagesG,
            introPages[2].pagesG
            //TODO
        };
        indexPages = new int[metaPages.Length];
        foreach (GameObject[] pags in metaPages) XavHelpTo.Change.ActiveObjectsExcept(pags, -1);

        navigator.haveBounds = true;
        navigator.SetPages(metaPages[0]);
        //aquí se coloca al navigator e page initial
    }
    public override void Init()
    {
        print("Hola");
    }
    private void Update()
    {
        ControlCheck();
    }
    #endregion
    #region Methods

    private void ControlCheck()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) return;

        KeyPlayer keyPress = ControlSystem.KnowKey(KeyPlayer.LEFT, KeyPlayer.RIGHT);

        //si presionan derecha o izq cambiamos la pagina
        if (ControlSystem.KeyExist(keyPress)){
            navigator._NavigateTo(keyPress.Equals(KeyPlayer.RIGHT));
        }
    }

    /// <summary>
    /// Cambiamos de entre las paginas
    /// Solo si este cambio es distinto
    /// </summary>
    public void ChangePagesTo(int i){
        if (!lastIndex.Equals(i)){

            //Actualizamos el ultimo indice
            indexPages[lastIndex] = navigator.GetIndexActual();

            menuInputC.lastIndex = i;

            lastIndex = i;

            //Colocamos als nuevas paginas
            navigator.SetPages(metaPages[i], indexPages[i]);
        }
    }
    #endregion
}