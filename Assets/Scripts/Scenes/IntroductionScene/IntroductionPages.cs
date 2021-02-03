#region Imports
using UnityEngine;
using XavLib;
#endregion
public class IntroductionPages : MonoBehaviour
{
    #region Variables
    //[Header("IntroductionPages Setup")]
    private IntroductionPage[] pages;
    private GameObject[] pagesG;
    #endregion
    #region Events
    private void Start()
    {
        PageLoad();
    }
    #endregion
    #region Methods

    /// <summary>
    /// Revisa si esta correcto las paginas
    /// </summary>
    public void PageLoad(){
        //conocemos el largo de paginas
        int length = transform.childCount;
        //si hay diferencia de longitudes de pagina
        pages = new IntroductionPage[length];
        pagesG = XavHelpTo.Get.Childs(transform);

        for (int x = 0; x < length; x++)
        {
            if (pagesG != null) LoadPage(x);
        }
    }

    /// <summary>
    /// Carga la pagina en el arreglo
    /// </summary>
    public void LoadPage(int x){
        pages[x] = pagesG[x].GetComponent<IntroductionPage>();
        pagesG[x].SetActive(false);
    }

    /// <summary>
    /// Actualiza la pagina 
    /// </summary>
    public void ReloadPage(int i)
    {
        pages[i].ReloadPage();
    }

    /// <summary>
    /// Actualizo por completo
    /// </summary>
    public void InstantPage(int i)
    {
        pages[i].InstantLoadPage();
    }

    /// <summary>
    /// Devuelve el arreglo de los objetos
    /// </summary>
    public GameObject[] GetObjectsRef() => pagesG;
    #endregion
}