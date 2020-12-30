using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{

    private KeyController _ = null;

    private void Awake(){
        //Singleton corroboration
        if (_ == null) _ = this;
        else if (_ != this) Destroy(gameObject);
        
    }


    /*
        Tendremos nuestros CONTROLS
        que nos podrán decir qué deberemos estar pendientes
        y, estas se volverán true

    con estas comprobamos cuando => son presionadas
    cuando son pusheadas y mantenidas

     */









}

public class KeyData
{
    private static readonly Key[] keys;

    //START KeyRecipe
    private delegate Key KeyRecipe(KeyPlayer kP, KeyCode[] kC);
    private readonly static KeyRecipe key = (KeyPlayer kP, KeyCode[] kC) => new Key(kP,kC) ;
    //END

    ////START
    private delegate KeyCode KeyCodeRecipe(int[] codes);
    //private readonly static KeyCodeRecipe keyCodes = (int[] codes) => new Key(codes);


    static KeyData(){

        keys = new Key[] {

            //key(KeyPlayer.BACK,ke)


        };
    }
}

/// <summary>
/// Modelo de una tecla, contiene el nombre de la llave y sus llaves Keycode
/// </summary>
public struct Key
{
    // llave de este tipo
    public KeyPlayer keyPlayer;

    //Llaves que detectará
    public KeyCode[] keyCodes;

    public Key(KeyPlayer keyPlayer, KeyCode[] keyCodes)
    {
        this.keyPlayer = keyPlayer;
        this.keyCodes = keyCodes;
    }
}

/// <summary>
/// Son las llaves que posee el jugador
/// </summary>
public enum KeyPlayer
{
    BACK,
    OK_FIRE,

    LEFT,
    RIGHT,
    UP,
    DOWN,

    C,V,B
}