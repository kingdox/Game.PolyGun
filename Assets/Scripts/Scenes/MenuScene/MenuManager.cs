#region Imports
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using XavLib;
#endregion
public class MenuManager : MonoManager
{
    #region Variables
    private enum Menu {
        Play,
        Intro,
        Achieve,
        Opt,
        Exit
    }

    private readonly TKey[] keys_Msg ={
        TKey.MSG_CYBORG_WHERE,
        TKey.MSG_CYBORG_UNKNOW
    };
    [Header("MenuManager")]
    public MsgController msg_Message;
    //[Space]
    public bool waitToLoad = false;
    //
    public bool wantRefresh;
    //
    [Space]
    public ImageController transitorScreen;
    public MenuInputController menuInputs;
    [Space]
    public AudioClip clip;

    #endregion
    #region Events
    private void Start(){
        Time.timeScale = 1f;
        transitorScreen.gameObject.SetActive(true);
    }
    private void Update() {


        if (!OptionSystem.isOpened)
        {
            if (!waitToLoad && msg_Message.IsFinished())
            {
                StartCoroutine(MessageWait());
            }
            if (wantRefresh)
            {
                wantRefresh = false;
                RefreshAll();
            }
        }


    }
    public override void Init(){
        SavedData saved = DataPass.GetSavedData();
        ButtonAdjust(!saved.isIntroCompleted);
        AudioSystem.SetThisMusic(clip);

    }
    #endregion
    #region Methods


    /// <summary>
    ///  Revisamos si en las opciones estan cerradas y si el mensaje ya ha terminado
    /// </summary>
    private bool MessageReady(MsgController msg) => !OptionSystem.isOpened && msg.IsFinished();

    /// <summary>
    /// Limpia el mensaje actual y abre las opciones
    /// </summary>
    public void OpenOptions(){
        msg_Message.LoadKey(TKey.No, 0);
        OptionSystem.OpenClose(true);
        wantRefresh = true;
    }

    /// <summary>
    /// Ajustará qué botones podrán ser interactuables y cuales no,
    /// dependiendo del estado de si el tutorial fue terminado o no.
    /// </summary>
    private void ButtonAdjust(bool adjust = false){
        Menu[] buttonsMenu = { Menu.Play, Menu.Achieve, Menu.Opt};

        for (int x = 0; x < menuInputs.buttons.Length; x++){

            menuInputs.buttons[x].interactable = true;

            if (adjust){
                //si encuentra que forma parte de los adjust
                foreach (Menu btn in buttonsMenu)
                {
                    if ((Menu)x == btn) menuInputs.buttons[x].interactable = false;
                }
            }
        }
    }


    /// <summary>
    /// Aquí cargaremos cada cierto tiempo un mensaje
    /// </summary>
    IEnumerator MessageWait(float waitTime = 4){
        waitToLoad = true;

        yield return new WaitForSeconds(waitTime);

        //si estas en menu, no se ha cargado mensaje y el que estaba ya ha terminado
        if (MessageReady(msg_Message)) LoadMessage();

        waitToLoad = false;

    }

    /// <summary>
    ///Carga algún mensaje del indice
    ///<para>si index es -1 entonces tomará aleatoriamente alguno</para>
    /// </summary>
    private void LoadMessage(int index=-1){
        index = index != -1 ? index : XavHelpTo.Know.DifferentIndex(keys_Msg.Length, index);
        msg_Message.LoadKey(keys_Msg[index], .075f);
    }


    /// <summary>
    /// Refresca los textos en pantalla
    /// </summary>
    private void RefreshAll() {

        foreach (Button btn in menuInputs.buttons)
        {
            MsgController msg = btn.GetComponent<MsgController>();
            msg.LoadKey(msg.key);
        }

    }
    


    /// <summary>
    /// Te saca de la aplicación
    /// </summary>
    public void ExitApp() => Application.Quit();
    #endregion
}