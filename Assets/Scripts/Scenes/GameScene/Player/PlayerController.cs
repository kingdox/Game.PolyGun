﻿#region
using System;
using System.Security;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;
using XavLib;
#endregion
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Rotation))]
[RequireComponent(typeof(Equipment))]
[RequireComponent(typeof(Shot))]
[RequireComponent(typeof(SaveVelocity))]
[RequireComponent(typeof(Rigidbody))]
///<summary>
/// Manejo de los controles de Player
/// <para>
/// Dependiendo de la tecla presionada buscaremos
/// hacer una acción o otra
/// </para>da
///</summary>
public class PlayerController : MonoX
{

    #region Variables
    //private static Transform player;

    [Header("PlayerSettings")]
    public Character character;
   
    [Space]
    private Destructure destructure;
    private bool isDead = false;

    [Header("Buffs")]
    public Transform buffList;
    public ItemBuff[] buffs;

    [Header("Requirement")]
    private Movement movement;
    private Rotation rotation;
    private Equipment equipment;
    private Shot shot;
    private SaveVelocity saveVelocity;
    [HideInInspector]
    public Rigidbody body;
    [Header("Debug")]
    private static float entryLife = -1;

    #endregion
    #region Events
    private void Awake()
    {
        Get(out movement);
        Get(out rotation);
        Get(out equipment);
        Get(out shot);
        Get(out destructure);
        GetAdd(ref saveVelocity);
        Get(out body);

        GetChilds(out buffs, buffList);
        character.type = CharacterType.PLAYER;
    }
    private void Update()
    {
        CheckOnGame();

        //hay cambios
        if (!entryLife.Equals(-1)){
            character.timeLife = entryLife;
            entryLife = -1;
        }

    }
    private void FixedUpdate()
    {
        if (!isDead && GameManager.IsOnGame())
        {
            Movement(); // movement se maneja internamente
        }
        else
        {
            movement.StopMovement();
        }
    }
    #endregion
    #region Methods

    /// <summary>
    /// Detectas las caracteristicas de cuando estas en juego
    /// </summary>
    private void CheckOnGame(){
        if (!isDead && character.IsAlive()){
            Pause();

            if (GameManager.IsOnGame()){
                BuffsUpdate();
                Equipment();
                Attack();


                if (character.timeLifeMax < character.timeLife)
                {
                    character.timeLife = character.timeLifeMax;
                }
                character.LessLife();

                DebugChecker();
            }
           

        }else{
            //Eliminamos a poly
            SetDead();
        }
    }

    /// <summary>
    /// Set the dead of player
    /// </summary>
    public void SetDead()
    {
        if (isDead) return;
        isDead = true;
        destructure.DestructureThis();
        GameManager.GameOver();
    }

    /// <summary>
    /// Recorre la lista de buffs existentes y aplica sus propiedades en caso
    /// de que exista
    /// </summary>
    private void BuffsUpdate()
   {
        for (int x = 0; x < buffs.Length; x++)  
        {
            //revisamos si NO ha podido aplicar el buff
            buffs[x].CanApplyBuff(ref character);
        }
    }

    /// <summary>
    /// Movemos la player en la dirección en la que ha tocado las teclas
    /// Se revisa cada tecla presionada por separado para asignar los
    /// valores correspondientes
    /// </summary>
    private void Movement(){
        Vector3 axis = ControlSystem.GetAxis();
        movement.Move(axis, character.speed);
        rotation.RotateByAxis(axis);
    }
    /// <summary>
    /// Detecta si has tocado alguna tecla de equipación, de ser así
    /// ejecuta una acción en <see cref="Equipment"/>
    /// </summary>
    private void Equipment(){

        //si hay algo por fabricar
        equipment.WaitedCraft(ref character, ref buffs);

        //Buscamos la primera accion de objeto selecta
        int indexK = ControlSystem.KnowIndexKeyFrame(ControlSystem.keysObjects);
        //si tocamos una tecla
        if (!indexK.Equals(-1)){
            ActionType action = equipment.Action(indexK);
            //si se usó
            if (action.used){

                //PrintX($"{action.item}");

                //si es un item, entonces curamos
                if (action.IsItem())
                {
                    //PrintX("Curando");
                    character.timeLife += Data.data.healShape[(int)action.item];
                }
                else
                {
                    BuffType buffType = action.ToBuffType();
                    //Esta recorriendo para buscar el buff y asignarle
                    foreach (ItemBuff buff in buffs)
                    {
                        if (buff.buff.Equals(buffType))  
                        {
                            buff.StartBuff();
                        }
                    }
                }

            }
        }
    }

    /// <summary>
    /// Ejecuta el ataque del player si la tecla fue presionada
    /// </summary>
    private void Attack(){
        if (ControlSystem.IsKeyFrame(KeyPlayer.OK_FIRE)){

            //Dispara y ve si ha completado
            if (shot.ShotBullet(character))
            {
                body.AddForce(-transform.forward * 20, ForceMode.Impulse);
            }
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

    /// <summary>
    /// Checks if it was pressed the debug mode
    /// </summary>
    private void DebugChecker()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Q))
        {
            //Activates or disactivates the debug mode
            GameManager._onDebug = !GameManager._onDebug;
        }
    }
    /// <summary>
    /// Clear all the efects
    /// </summary>
    public void __Debug_ClearBuffEffects()
    {
        foreach (ItemBuff buff in buffs)
        {
            buff.Reset();
        }
    }
    //Set a new qty of life
    public static void __Debug_SetLife(float newLife)
    {
        entryLife = newLife;

    }
    #endregion
}

/// <summary>
/// Modelo que conforma la estructura los seres (aliados y enemigos, incluyendo al player) 
/// </summary>
[Serializable]
public struct Character{

    public CharacterType type;
    /// <summary>
    /// Nos indica cuanto tiempo de vida posee, este decrese a medida que pasa el tiempo
    /// </summary>
    public float timeLife;
    /// <summary>
    /// Nos muestra la vida maxima que posee
    /// </summary>
    public float timeLifeMax;
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

    //****BUFF THINGS
    public bool isInmortal;
    public bool canExtraShots;

    /// <summary>
    /// Revisa si sigue con vida
    /// </summary>
    public bool IsAlive() => timeLife > 0;

    /// <summary>
    /// Hace que pierda vida el player a medida que pasa el tiempo
    /// </summary>
    public void LessLife()
    {
        if (!isInmortal)
        {
            timeLife = Mathf.Clamp(timeLife - Time.deltaTime, 0, timeLifeMax);
        }
    }

    /// <summary>
    /// Manejador que comprueba la vida y resta lo correspondiente
    /// </summary>
    public void LostLife(float val)
    {
        if (!isInmortal)
        {
            this.timeLife -= val;
        }
    }

}

/// <summary>
/// Los tipos de personajes en pantalla
/// </summary>
public enum CharacterType
{
    MINIONS = -1,

    PLAYER,
    ENEMY,
    ALLY,
    BOSS
}