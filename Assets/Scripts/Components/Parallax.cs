using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Este paralax solo usa el eje X
public class Parallax : MonoBehaviour
{
    #region Var
    //Atenuaciond el movimiento horizontal
    public float attenX = 0.01f;

    //Atenuacion del movimiento vertical
    public float attenY = 0;

    //Control de offset de la textura
    private Vector2 pos = Vector2.zero;


    private Vector2 camOldPos;

    //Referencia el renderer del BG
    private Renderer rend;
    #endregion
    #region Events

    private void Start() {
        //camOldPos = GameManager.GetCamera().transform.position;

        rend = GetComponent<Renderer>();

    }
    #endregion
    #region Methods

    private void Update() {

        Vector2 camPos = Vector2.one;// = Camera.main().pos;

        Vector2 camVar = new Vector2(camPos.x - camOldPos.x, camPos.y - camOldPos.y);

        //Asignamos la posición a la que se aplicará a la textura del offset atenuando la distancia entre la camara y la anterior pos de esta e X y Y
        pos.Set(pos.x + (camVar.x * attenX), pos.y + (camVar.y * attenY));


        //Cambia la pos del offset del material (puedes verlo yendo al material y ver el selectShader)
        rend.material.SetTextureOffset("_MainTex",pos);


        //Actualizamos al camara
        camOldPos = camPos;


    }
    #endregion



}
