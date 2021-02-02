#region 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Environment;
using XavLib;
#endregion

public class InitialManager : MonoManager, IInitManager
{
    #region Variables

    private bool canPressAnyKey = false;

    [Header("Initial Manager Settings")]
    public ImageController imgC_splash;
    public MsgController msg_history;
    public MsgController msg_pressAny;

    private SavedData saved;

    #endregion
    #region  Events
    public override void Init()
    {
        StartCoroutine(LoadSplash());

        //asignamos las cosas por defecto en caso de ser la priera vez
        saved = DataPass.GetSavedData();
        if (!saved.isIntroCompleted)
        {
            saved.textSpeed = 2;
            saved.musicVolume = 2;
            saved.sfxVolume = 1;
            New(out saved.achievements, AchieveSystem.achievementLenght);
            DataPass.SetData(saved);
        }

    }
    private void Update()
    {   
        if (canPressAnyKey && Input.anyKey) CheckInit();
    }
    #endregion
    #region Methods

    /*
        TODO para el futuro
        puede que toque colocar la imagen de español y ingles antes de
    cargar la historia para que no se la pierda, antes de seguir cargando..?
     */

    /// <summary>
    /// Aquí se manejará el progreso
    /// <para>donde step representa el paso en el
    /// que nos encontramos para el manejo de la pantalla</para>
    /// </summary>
    private IEnumerator LoadSplash(int step = 0){
        float[] stepsSpeed = { 2, 4, 2.5f};
        bool condition = step < stepsSpeed.Length;
        float seconds = condition ? stepsSpeed[step] : 0;

        yield return new WaitForSeconds(seconds);
        //Si falta por cargar lo hace...
        if (condition)
        {
            switch (step){
                    //Mostramos el splash
                case 0:
                    imgC_splash.color_want = Color.white;
                    imgC_splash.keepUpdate = true;
                    break;
                    //Oscurecemos
                case 1:
                    imgC_splash.color_want = Color.black;
                    imgC_splash.scaleSpeed = 1.5f;
                break;
                case 2:
                    //Usamos el splash para desaparecer la pantalla
                    imgC_splash.color_want = XavHelpTo.Set.ColorParam(imgC_splash.color_want, (int)ColorType.a, 0);
                    msg_history.LoadKey(TKey.MSG_INIT_HISTORY);
                    StartCoroutine(ActivePressAny());
                    break;
            }

            StartCoroutine(LoadSplash(++step));
        }
    }

    /// <summary>
    /// Espera a que termine de cargar la historia para volver clicable
    /// </summary>
    private IEnumerator ActivePressAny(){
        yield return new WaitForSeconds(1);
        if (msg_history.IsFinished()){
            canPressAnyKey = true;
            msg_pressAny.LoadKey(TKey.MSG_INIT_PRESS_ANY);
        }
        else StartCoroutine(ActivePressAny());
    }

    public void CheckInit(){

        if (saved.isIntroCompleted)
        {
            XavHelpTo.Look.Print("Cargando menu");
            GoToScene(Scenes.MenuScene);
        }
        else
        {
            XavHelpTo.Look.Print("Cargando Introducción");
            GoToScene(Scenes.IntroductionScene);
        }
    }

    #endregion
}

interface IInitManager
{
    /// <summary>
    /// Revisamos si ha hecho tutorial o no, dependiendo de la respuesta
    /// se lleva la jugador al menu o a introducción
    /// </summary>
    void CheckInit();
}