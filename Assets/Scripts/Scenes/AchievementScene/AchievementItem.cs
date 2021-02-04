#region Imports
using UnityEngine;
using UnityEngine.UI;
using Achievements;
using XavHelpTo.Build;
using XavHelpTo.Set;
#endregion
public class AchievementItem : MonoBehaviour
{
    #region Variables
    //Los colores para cada etapa en orden
   
    private TextValBarItem item;

    //Veremos el progreso de la barra
    private float toMax = 0;


    [Header("Settings")]
    public MsgController msg_title;
    public MsgController msg_description;
    public MsgController msg_value;


    [Space]
    public Image img_bar_last;
    [Space]
    public Image img_bar_actual;
    public RectTransform rect_bar_actual;
    [Space]
    [Header("Unlocker")]
    public bool isUnlockItem = false;
    [Space]
    public GameObject lockObj;

    #endregion
    #region Events
    private void Awake(){
        //Iniciamos en 0% el ancho de la barra
        rect_bar_actual.anchorMax = new Vector2(0, rect_bar_actual.anchorMax.y);
        img_bar_last.color = Color.white;
        toMax = 0;

       
    }
    private void Start()
    {
        //Comprobamos si el item es de tipo unlock
        if (isUnlockItem)
        {
            AchieveSystem.SetUnlockItem(this);
        }
    }
    private void Update(){
        DrawBar();
    }
    #endregion
    #region Methods

    /// <summary>
    /// Se asigna los datos del modelo de los valores del item
    /// </summary>
    public void SetItem(TextValBarItem newItem) {
        item = newItem;
        DrawItem();
    }

    /// <summary>
    /// Se encarga de pintar el Item con los valores que posee de <see cref="TextValBarItem"/>
    /// </summary>
    private void DrawItem()
    {
        float[] _limits = item.limit.ToArray();
        int _limitIndex = item.LimitReached;
        img_bar_last.color = Color.black;

        //Debug.Log($"{item.key}: {item.TextValue}");
        //Cargamos la llave
        msg_title.LoadKey(item.key);
        msg_description.LoadKey(item.keyDesc);
        //Cargamos el valor
        msg_value.LoadText(item.TextValue);

        //ajustamos el progreso de la barra
        if (_limitIndex != -1){

            toMax = item.value.PercentOf(_limits[_limitIndex]) / 100;

            if (item.value == 0){
                img_bar_last.color = Color.white;
                img_bar_actual.color = Color.black;
                lockObj.SetActive(true);
            }
            else{
                img_bar_last.color = Color.black;
                img_bar_actual.color = AchievementData.colorSteps[_limitIndex];
                lockObj.SetActive(false);

            }
        }
        else
        {
            //Si supera los limites
            img_bar_last.color = Color.black; 
            img_bar_actual.color = Color.red; 
            toMax = 1;
            lockObj.SetActive(false);
        }

    }




    /// <summary>
    /// Pintaremos la barra de manera de que se vea como está cargando,
    /// se cargará basandose de una variable "Max" con la que podrá ver
    /// </summary>
    private void DrawBar(){

        float _rectX = rect_bar_actual.anchorMax.x;
        //evitamos renderizar
        if (toMax == _rectX) return;

        float _scale = 2;//TODO HARDCODED

        rect_bar_actual.anchorMax = new Vector2(
            Set.UnitInTime(_rectX, toMax,_scale)
            , rect_bar_actual.anchorMax.y);

    }

    #endregion
}