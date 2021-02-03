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
    private int[] indexPages = {};
    private int lastIndex = 0;
    #endregion
    #region Events
    private void Start(){
        lastIndex = 0;
        indexPages = new int[introPages.Length];
        navigator.haveBounds = true;
    }
    public override void Init()
    {
        navigator.SetPages(introPages[0].GetObjectsRef());

        SavedData saved = DataPass.GetSavedData();
        saved.isIntroCompleted = true;
        DataPass.SetData(saved);
        AchieveSystem.UpdateAchieve(Achieves.ESPECIAL_READ);
        DataPass.SaveLoadFile(true);

    }
    private void Update()
    {
        ControlCheck();
    }
    #endregion
    #region Methods

    /// <summary>
    /// Checkea si ha tocado las teclas en concretas
    /// <para>ignora el mouse</para>
    /// </summary>
    private void ControlCheck()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) return;

        KeyPlayer keyPress = ControlSystem.KnowKeyFrame(KeyPlayer.LEFT, KeyPlayer.RIGHT);

        //si presionan derecha o izq cambiamos la pagina
        if (ControlSystem.IsKeyExist(keyPress)){
            //Cargamos la pagina al movernos//TODO
            introPages[lastIndex].InstantPage(indexPages[lastIndex]);
            navigator._NavigateTo(keyPress.Equals(KeyPlayer.RIGHT));
        }

        if (ControlSystem.IsKeyFrame(KeyPlayer.BACK)){
            GoToScene(Scenes.MenuScene);
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
            navigator.SetPages(introPages[i].GetObjectsRef(), indexPages[i]);

            introPages[i].ReloadPage(indexPages[i]);
        }
    }
    #endregion
}