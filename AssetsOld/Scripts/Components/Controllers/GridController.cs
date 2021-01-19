#region imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavLib;
#endregion
namespace Grid
{
    /// <summary>
    /// Maneja un grupo de objetos en el UI, permitiendo
    /// Organizarlos de manera responsiva a el contenedor
    /// <para>___1/ene/2021__</para>
    /// <para>Este Componente sigue en desarrollo</para>
    /// </summary>
    public class GridController : MonoBehaviour
    {
        #region Var

        //Donde adminsitraremos nuestros items con los tamaños esperados
        private GridLayoutGroup grid;
        // nuestro rect para ajustarnos a él
        private RectTransform rect;

        [Header("Settings")]
        public RectTransform[] rect_containers;
        [Space]
        public bool initLoaded = true;


        //Conocimiento para saber el tamaño esperado
        private int countOfItems = 0;
        private Vector2 spacing = new Vector2(0,0);
        private Vector2 childSize = new Vector2(0, 0);


        //START AnchorSizeRecipe
        private delegate Vector2 AnchorSizeRecipe(RectAnchor anch);
        private readonly static AnchorSizeRecipe anchorSizeOf = (RectAnchor anch) => (anch.max - anch.min) * 100;
        //END 
        //START RectAnchorRecipe
        private delegate RectAnchor RectAnchorRecipe(RectTransform r);
        private readonly static RectAnchorRecipe rectAnchorOf = (RectTransform r) => new RectAnchor(r.anchorMin, r.anchorMax);
        //END

        #endregion
        #region Events
        private void Awake(){

            //Mejora el rendimiento si los asignas con el inspector
            grid = GetComponent<GridLayoutGroup>();
            rect = GetComponent<RectTransform>();

            //Si se quisiera iniciar cargando al grid
            if (initLoaded) LoadGrid();
        }
        private void Start(){
            if (initLoaded) Refresh();
        }
        #endregion
        #region Methods

        /// <summary>
        /// Cargas el los datos al grid con la información actual
        /// </summary>
        public void LoadGrid(){


            countOfItems = transform.childCount;
            spacing = Vector2.one * grid.spacing * (countOfItems - 1) / countOfItems;


            Vector2 _objSize = GetChildSize();

            //Debug.Log($"Contenedores : {rect_containers.Length}, Tamaño : {_objSize} Sobre {_screenSize}");
            childSize = new Vector2(_objSize.x , _objSize.y / countOfItems) - spacing;
        }


        /// <summary>
        /// Bajo el tamaño
        /// </summary>
        /// <returns>El tamaño de los hijos basado en los tamaños de los contenedores</returns>
        public Vector2 GetChildSize(){
            Vector2 _screenSize = new Vector2(Screen.width, Screen.height);

            ///Recorremos los rect padres de este
            for (int x = 0; x < rect_containers.Length; x++)
            {
                //Donde el primero inicia con los valores de _screenSize
                _screenSize = XavHelpTo.Get.QtyOf(_screenSize, anchorSizeOf(rectAnchorOf(rect_containers[x])));
            }

            return XavHelpTo.Get.QtyOf(_screenSize, anchorSizeOf(rectAnchorOf(rect)));
        }

        /// <summary>
        /// Actualiza el Grid para ajustarlo con los valores que posee
        /// </summary>
        public void Refresh(){
            grid.cellSize = childSize;

            //TODO poner un bool publico para ver si refresca los colocados o no...?
            //grid.spacing = spacing;

            //grid.padding = padding;
            //grid.startAxis = axis;
            //grid.constraintCount = countOfItems;
        }


        #endregion

    }

    public struct RectAnchor
    {
        public Vector2 min;
        public Vector2 max;
        public RectAnchor(Vector2 min, Vector2 max)
        {
            this.min = min;
            this.max = max;
        }
    }
}
