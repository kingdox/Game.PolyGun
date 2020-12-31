#region imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Key;
using Environment;

public class ControlSystem : MonoBehaviour
{
    #region var
    private static ControlSystem _;

    [Header("Settings")]
    private Key.Key[] keys = Data.data.GetKeys();
    private KeyCode[] codes;
    [Space]
    public static bool canInput = true;
    //public
    #endregion
    #region Events
    private void Awake(){
        //Singleton corroboration
        if (_ == null)
        {
            DontDestroyOnLoad(gameObject);
            _ = this;
        }
        else if (_ != this)
        {
            Destroy(gameObject);
        }
        canInput = true;
    }
    private void Start()
    {
        LoadCodes();
    }

    #endregion
    #region Methods

    /// <summary>
    /// Devolvemos si la tecla fue presionada o no
    /// Esta tecla detecta cuando en este frame ha sido presionado
    /// </summary>
    public static bool KeyDown(int i) => Input.GetKeyDown(_.codes[i]);
    /// <summary>
    /// Devolvemos si la tecla fue presionada o no
    /// Esta tecla detecta cuando en este frame ha sido presionado
    /// </summary>
    public static bool KeyDown(KeyPlayer kp) => Input.GetKeyDown(_.codes[(int)kp]);


    /// <summary>
    /// Comprobamos si la tecla esta siendo presionada
    /// </summary>
    public static bool KeyPress(int i) => Input.GetKey(_.codes[i]);
    /// <summary>
    /// Comprobamos si la tecla esta siendo presionada
    /// </summary>
    public static bool KeyPress(KeyPlayer kp) => Input.GetKey(_.codes[(int)kp]);

    /// <summary>
    /// Busca la tecla basado en lo presionado
    /// </summary>
    //private Key.Key SearchKey(KeyCode code){

    //    int index = -1;

    //    foreach (var k in keys)
    //    {
    //        if (k.Contains(code))
    //        {
    //            index = (int)k.keyPlayer;
    //        }
    //    }

    //    return keys[index];
    //}

    //poseeremos un arreglo con los codes de cada key y veremos si alguno
    //fue tocado, de ser así los enviamos para investigar quien fue y como comportarlo

    /// <summary>
    /// Cargamos los Codes basado en los datos que poseemos del keys
    /// </summary>
    private void LoadCodes()
    {
        //Conocemos la dimención de los codigos basado en la longitud de los
        //codes y las teclas
        codes = new KeyCode[KeyData.codeLenght * keys.Length];
        int c = 0;

        //por cada key... vamos a recorrer sus codes y asignarlos a un arreglo
        for (int x = 0; x < keys.Length; x++){
            foreach (KeyCode code in keys[x].keyCodes)codes[c++] = code; 
        }
    }

    #endregion
}
#endregion
