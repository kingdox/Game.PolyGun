#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavLib;
using Environment;
#endregion
public class MenuManager : MonoBehaviour , IManager
{
    #region Variables
    private bool init = false;


    //public bool I() { return false; }
    //bool IManager.I { get => false;  }

    //[Header("Settings")]
    [Header("Navigator Buttons")]
    public Button[] buttons;

    //poseerán este ordenamiento
    enum ButtonMenu{
        Play,
        Intro,
        Achieve,
        Opt,
        Exit
    }

    #endregion
    #region Events
    private void Awake(){
        init = false;

    }
    private void Update()
    {
        //TODO esto cambiarlo en el "ManagerSystem"
        if (!init && DataPass.IsReady()) Init();
    }

    public void Init(){
        init = true;
        ButtonAdjust(!DataPass.GetSavedData().isIntroCompleted);
    }
    public void GoToScene(string name) => XavHelpTo.ChangeSceneTo(name);
    public void GoToScene(Scenes scene) => XavHelpTo.ChangeSceneTo(scene.ToString());
    #endregion
    #region Methods



    /// <summary>
    /// Ajustará qué botones podrán ser interactuables y cuales no,
    /// dependiendo del estado de si el tutorial fue terminado o no.
    /// </summary>
    private void ButtonAdjust(bool adjust = false){
        ButtonMenu[] buttonsMenu = { ButtonMenu.Play, ButtonMenu.Achieve, ButtonMenu.Opt};

        for (int x = 0; x < buttons.Length; x++){

            buttons[x].interactable = init;

            if (adjust){
                //si encuentra que forma parte de los adjust
                foreach (ButtonMenu btn in buttonsMenu)
                {
                    if ((ButtonMenu)x == btn) buttons[x].interactable = false;
                }
            }
        }
    }
    

    #endregion
}


