#region Imports
using UnityEngine;
using Environment;
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
    [Space]
    //Ultimos indices de las paginas 
    public int[] indexPages = {};
    public int lastIndex = 0;
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
        if (Input.anyKey)
        {
            ControlCheck();
        }
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
            NavButt(keyPress.Equals(KeyPlayer.RIGHT));
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

            //con esto podemos guardar los indices de las paginas
            indexPages[lastIndex] = navigator.GetIndexActual();

            //actualizamos el ultimo indice, mostrando la pagina a la que estamos interactuando
            //este dejara marcado el ultimo boton del menú
            menuInputC.lastIndex = i;

            //actualizamos el ultimo indice aquí...
            lastIndex = i;


            //Colocamos als nuevas paginas y colocamos el indice guardado de esa pagina
            navigator.SetPages(introPages[i].GetObjectsRef(), indexPages[i]);

            // actualizamos la pagina selecta para que posea sus textos cargados
            introPages[i].ReloadPage(indexPages[i]);
        }
    }



    /// <summary>
    /// Navegador hacia alguno de los lados de la pagina actual
    /// </summary>
    public void NavButt(bool goForward)
    {
        //1 - paramos el que esté cargando actualmente (así evito los problemas de mr coroutina)
        introPages[lastIndex].InstantPage(indexPages[lastIndex]);

        //2 - cambiamos a la pagina correspondiente
        navigator._NavigateTo(goForward);

        //3 - actualizamos a el nuevo indice
        int i = navigator.indexActual;
        indexPages[lastIndex] = i;

        //4 - cargo el nuevo indice
        introPages[lastIndex].ReloadPage(indexPages[lastIndex]);
    }

    #endregion
}