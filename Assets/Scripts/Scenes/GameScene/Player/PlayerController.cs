#region
using System;
using System.Security;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;
#endregion
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Equipment))]
[RequireComponent(typeof(Shot))]
///<summary>
/// Manejo de los controles de Player
/// <para>
/// Dependiendo de la tecla presionada buscaremos
/// hacer una acción o otra
/// </para>
///</summary>
public class PlayerController : MonoX
{

    #region Variables
    //private static Transform player;

    [Header("PlayerSettings")]
    [SerializeField]
    public Stats stats;

    [Space]
    private Movement movement;
    private Equipment equipment;
    private Shot shot;

    #endregion

    #region Events
    private void Awake()
    {
        Get(out movement);
        Get(out equipment);
        Get(out shot);
        //Get(out player);

    }
    private void Update()
    {
        CheckOnGame();
        Pause();
    }
    #endregion
    #region Methods

    /// <summary>
    /// Detectas los  controles para en juego
    /// </summary>
    private void CheckOnGame(){

        //se maneja internamente
        Movement();

        //retornamos si no esta en juego
        if (!GameManager.IsOnGame()) return;
        Equipment();
        Attack();
    }
    /// <summary>
    /// Movemos la player en la dirección en la que ha tocado las teclas
    /// Se revisa cada tecla presionada por separado para asignar los
    /// valores correspondientes
    /// </summary>
    private void Movement(){
        //movement.SetAxis(ControlSystem.GetAxis());
        movement.SetAxis(ControlSystem.GetAxisOf(ControlSystem.keysHorizontal), 0, ControlSystem.GetAxisOf(ControlSystem.keysForward));
        movement.Move();
    }
    /// <summary>
    /// Detecta si has tocado alguna tecla de equipación, de ser así
    /// ejecuta una acción en <see cref="Equipment"/>
    /// </summary>
    private void Equipment(){
        //Buscamos la primera accion de objeto selecta
        equipment.Action(ControlSystem.KnowIndexKeyFrame(ControlSystem.keysObjects));
    }

    /// <summary>
    /// Ejecuta el ataque del player si la tecla fue presionada
    /// </summary>
    private void Attack(){
        if (ControlSystem.IsKeyFrame(KeyPlayer.OK_FIRE)){
            shot.ShotBullet( new BulletShot(stats.atkSpeed + stats.speed,stats.range,stats.damage));
        }
    }

    /// <summary>
    /// Te permitirá entrar y salir de la pantalla de pausa
    /// </summary>
    private void Pause(){
        if (!ControlSystem.IsKeyFrame(KeyPlayer.BACK)) return;

        if (!OptionSystem.isOpened){   
            GameStatus actualStatus = GameManager.IsOnGame() ? GameStatus.ON_PAUSE : GameStatus.ON_GAME;
            GameManager.SetGameStatus(actualStatus);
        }
      
    }
    
    #endregion
}

/// <summary>
/// Modelo que conforma la estructura los seres (aliados y enemigos, incluyendo al player) 
/// </summary>
[Serializable]
public struct Stats{

    /// <summary>
    /// Nos indica cuanto tiempo de vida posee, este decrese a medida que pasa el tiempo
    /// </summary>
    public float timeLife;
    /// <summary>
    /// indica la velocidad de desplazamiento
    /// </summary>
    public float speed;
    /// <summary>
    /// Indica la cantidad que reduce de tiempo de vida, este frecuenta poseer algo de variabilidad al uso
    /// </summary>
    public float damage;
    /// <summary>
    /// Rango minimo requerido para poder hacer que el personaje pueda atacar
    /// </summary>
    public float range;
    /// <summary>
    /// Velocidad en interactuar con el area para infligir daño
    /// </summary>
    public float atkSpeed;

}