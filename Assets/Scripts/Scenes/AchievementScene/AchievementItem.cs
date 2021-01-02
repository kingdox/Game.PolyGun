#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Achievements;
using XavLib;
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
    public MsgController msg_value;

    [Space]
    public Image img_bar_last;
    [Space]
    public Image img_bar_actual;
    public RectTransform rect_bar_actual;


    #endregion
    #region Events
    private void Awake(){
        //Iniciamos en 0% el ancho de la barra
        rect_bar_actual.anchorMax = new Vector2(0, rect_bar_actual.anchorMax.y);
        toMax = 0;
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

        Debug.Log($"{item.key}: {item.TextValue}");
        //Cargamos la llave
        msg_title.LoadKey(item.key);
        //Cargamos el valor
        msg_value.LoadText(item.TextValue);

        //ajustamos el progreso de la barra
        if (_limitIndex != -1){

            toMax = XavHelpTo.KnowPercentOfMax(item.value, _limits[_limitIndex]) / 100;

            if (item.value == 0){
                img_bar_last.color = Color.white;
                img_bar_actual.color = Color.black;
            }
            else{
                img_bar_last.color = Color.black;
                img_bar_actual.color = AchievementData.colorSteps[_limitIndex];
            }
        }
        else
        {
            //Si supera los limites
            img_bar_last.color = Color.black; //AchievementData.colorSteps[2];//XavHelpTo.SetColorParam(img_bar_last.color, (int)ColorType.RGB, AchievementData.colorSteps[2]);
            img_bar_actual.color = Color.red; //AchievementData.colorSteps[2];
            toMax = 1;
        }

    }




    /// <summary>
    /// Pintaremos la barra de manera de que se vea como está cargando,
    /// se cargará basandose de una variable "Max" con la que podrá ver
    ///
    /// Al llamarlo la primera vez este debe de estar en  el tamaño 0 % y se irá estirando con un IEnumerator...
    /// En casode que se cambie y este siga cargando, solo se dirigirá a el sitio donde le corresponde
    /// </summary>
    ///
    private void DrawBar(){

        float _rectX = rect_bar_actual.anchorMax.x;
        //evitamos renderizar
        if (toMax == _rectX) return;

        //la barra debería moverse más rapido si está más lejos... sacarle un porcentaje de la diferencia?
        float _difference = XavHelpTo.GetPositive(toMax - _rectX);

        float scale = 2; //TODO HARDCODED

        float _stepToMax = Mathf.MoveTowards(_rectX, toMax, Time.deltaTime * _difference * scale);

        _stepToMax =   Mathf.Clamp(_stepToMax, 0, 1);

        rect_bar_actual.anchorMax = new Vector2( _stepToMax ,rect_bar_actual.anchorMax.y);

    }

    #endregion
}