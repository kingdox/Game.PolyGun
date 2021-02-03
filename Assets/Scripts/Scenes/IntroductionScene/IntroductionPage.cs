#region Imports
using UnityEngine;
using XavLib;
#endregion
public class IntroductionPage : MonoBehaviour
{
    #region Variables
    [Header("IntroductionPage Setup")]
    [Space]
    private MsgController[] messagesC = new MsgController[0];
    [Space]
    private ImageController[] imagesC = new ImageController[0];
    [Header("parents")]
    public Transform parent_msgC;
    public Transform parent_imgC;
    #endregion
    #region Events

    private void Start()
    {
        messagesC = new MsgController[0];
        imagesC = new ImageController[0];

        if (parent_msgC != null) SetMessageControllers();
        if (parent_imgC != null) SetImageControllers();
    }
    #endregion
    #region Methods

    /// <summary>
    /// Coloca el arreglo con los controles 
    /// </summary>
    private void SetMessageControllers()
    {
        GameObject[] childs = XavHelpTo.Get.Childs(parent_msgC);
        messagesC = new MsgController[childs.Length];
        for (int x = 0; x < childs.Length; x++)
        {
            messagesC[x] = childs[x].GetComponent<MsgController>();
        }
    }
    /// <summary>
    /// Coloca el arreglo con los controles 
    /// </summary>
    private void SetImageControllers()
    {
        GameObject[] childs = XavHelpTo.Get.Childs(parent_imgC);
        imagesC = new ImageController[childs.Length];
        for (int x = 0; x < childs.Length; x++)
        {
            imagesC[x] = childs[x].GetComponent<ImageController>();
        }
    }

    /// <summary>
    /// Refresca la información de los controles de imagen y mensaje
    /// </summary>
    public void ReloadPage()
    {
        //print($"OBJ {name}");
        for (int j = 0; j < imagesC.Length; j++){
            imagesC[j].Refresh();
        }
        for (int j = 0; j < messagesC.Length; j++){
            messagesC[j].Refresh();
        }
    }

    /// <summary>
    /// Cargamos al instante los datos
    /// </summary>
    public void InstantLoadPage()
    {
        for (int x = 0; x < messagesC.Length; x++)
        {
            messagesC[x].LoadText(messagesC[x]._GetSavedData(),0);
        }
    }
    #endregion
}