﻿#region imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion
//[RequireComponent<VerticalLayoutGroup>]
namespace Grid
{
    public class GridController : MonoBehaviour
    {
        #region Var

        //Donde adminsitraremos nuestros items con los tamaños esperados
        private GridLayoutGroup grid;
        // nuestro rect para ajustarnos a él
        private RectTransform rect;

        [Header("Container")]
        //el contenedor del rect que modifique a este, luego podría ser un array TODO...
        //actualmente solo soporta 1 nivel de escala de contenedor
        public RectTransform rect_container;


        [Header("Settings")]
        //Conocimiento para saber el tamaño esperado
        public int countOfItems = 0;
        public Vector2 spacing = new Vector2(0,0);
        //public RectOffset padding = new RectOffset(0,0,0,0);
        //public GridLayoutGroup.Axis axis;

        //Extra
        //con estos sabremos cuanto miden
        [Space]
        [Header("Tamano de el objeto")]
        
        public Vector2 objSize = new Vector2(0, 0);

        [Space]
        [Header("Tamaño de hijos")]
        public Vector2 childSize = new Vector2(0, 0);


        //CURRY AnchorSizeRecipe
        private delegate Vector2 AnchorSizeRecipe(RectAnchor anch);
        private readonly static AnchorSizeRecipe anchorSizeOf = (RectAnchor anch) => (anch.max - anch.min) * 100;
        //END CURRY

        //CURRY RectAnchorRecipe
        private delegate RectAnchor RectAnchorRecipe(RectTransform r);
        private readonly static RectAnchorRecipe rectAnchorOf = (RectTransform r) => new RectAnchor(r.anchorMin, r.anchorMax);
        //END CURRY

        #endregion
        #region Events
        private void Awake(){

            //Mejora el rendimiento si los asignas con el inspector
            grid = GetComponent<GridLayoutGroup>();
            rect = GetComponent<RectTransform>();

            LoadGrid();
        }
        private void Start()
        {
            Refresh();
        }
        #endregion
        #region Methods

        /// <summary>
        /// Cargas el los datos al grid con la información actual
        /// </summary>
        public void LoadGrid(){

            //TODO esto en un array nos permitiría tener esto para multiple nivel

            countOfItems = transform.childCount;

            //Tenemos el valor maximo
            Vector2 _screen = new Vector2(Screen.width, Screen.height);



            // TODO tomamos el tamaño del objeto en porcentaje
            Vector2 _containerSize = anchorSizeOf(rectAnchorOf(rect_container));
            //Sacamos el valor del contenedor con respecto a la pantalla
            Vector2 _cont_val = XavHelpTo.KnowQtyOfPercent(_containerSize, _screen);


            //----
            //TODO tomamos el tamaño del container en porcentaje
            Vector2 _objSize = anchorSizeOf(rectAnchorOf(rect));
            //HACK este es el verdadero tamaño maximo basado en el del contenedor
            Vector2 _obj_val = XavHelpTo.KnowQtyOfPercent(_objSize, _cont_val);

            //---

            //HACK podrías hacer un for y al terminar guardar los ultimos valores...

            Debug.Log($"Container : {_containerSize}, Obj : {_objSize}, TEST RESULT {_obj_val}");

            //objSize = XavHelpTo.GetSizeOf(_objSize);

            childSize = new Vector2( _obj_val.x ,_obj_val.y / countOfItems);
            //axis = grid.startAxis;
        }

        /// <summary>
        /// Actualiza el Grid para ajustarlo con los valores que posee
        /// </summary>
        public void Refresh(){
            grid.cellSize = childSize;
            grid.spacing = spacing;
            //grid.padding = padding;
            //grid.startAxis = axis;
            //grid.constraintCount = countOfItems;
        }


        #endregion

        /* 
         * Dependiendo de lo bien que vaya el script puede que se generalice
         * Aquí manejaremos el componente de verticalLayoutGroup
         * para que a nivel externo podamos ajustarlo por porcentajes 
         * en vez de manualmente en el inspecctor
         * 
         * Este script tiene como ideal de que si alguien externo lo llama
         * para modificarlo puede cambiar sus valores y así ajustará el item
         * lo mejor posible
         * 
         * el vertical es mas un contenedor.... que apila verticalmente
        */

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
