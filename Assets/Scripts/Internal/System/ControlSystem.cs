#region imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Key;
using Environment;
using XavLib;
#endregion
public class ControlSystem : MonoX
{
    #region var
    private static ControlSystem _;

    [Space]
    [Header("Settings")]
    public static bool canInput = true;
    private Key.Key[] keys = Data.data.GetKeys();
    private KeyCode[] codes;

    public readonly static KeyPlayer[] keysMovement = { KeyPlayer.UP, KeyPlayer.DOWN, KeyPlayer.RIGHT, KeyPlayer.LEFT, };

    public readonly static KeyPlayer[] keysHorizontal = { KeyPlayer.RIGHT, KeyPlayer.LEFT, };
    public readonly static KeyPlayer[] keysVertical = { };
    public readonly static KeyPlayer[] keysForward = { KeyPlayer.UP, KeyPlayer.DOWN, };

    //public
    #endregion
    #region Events
    private void Awake(){
        //Singleton corroboration
        if (_ == null){ DontDestroyOnLoad(gameObject); _ = this;}
        else if (_ != this) Destroy(gameObject);
        canInput = true;
    }
    private void Start() => LoadCodes();
    #endregion
    #region Methods


    /// <summary>
    /// Comprobamos si la tecla SIGUE siendo presionada
    /// </summary>
    public static bool IsKeyHold(KeyPlayer kp) => IsKeyHold((int)kp);
    public static bool IsKeyHold(int i) => Input.GetKey(_.codes[i]);
    public static bool IsKeyHold(params KeyPlayer[] kps){ foreach (KeyPlayer kp in kps) if (IsKeyHold(kp)) return true; return false;}
    public static bool IsKeyHold(params int[] ks) { foreach (int k in ks) if (IsKeyHold(k)) return true; return false; }

    /// <summary>
    /// Comprueba si la tecla FUE presionada en el frame
    /// </summary>
    public static bool IsKeyFrame(KeyPlayer kp) => IsKeyFrame((int)kp);
    public static bool IsKeyFrame(int i) => Input.GetKeyDown(_.codes[i]);
    public static bool IsKeyFrame(params KeyPlayer[] kps) { foreach (KeyPlayer kp in kps) if (IsKeyFrame(kp)) return true; return false; }
    public static bool IsKeyFrame(params int[] ks){foreach (int k in ks) if (IsKeyFrame(k)) return true;return false;}


    /// <summary>
    /// Devuelve la tecla que SIGUE siendo presionada
    /// </summary>
    public static KeyPlayer KnowKeyHold(params KeyPlayer[] kps) { foreach (KeyPlayer kp in kps) if (IsKeyHold(kp)) return kp; return KeyPlayer.NO; }
    /// <summary>
    /// Devuelve la tecla que FUE presionada en el frame
    /// </summary>
    public static KeyPlayer KnowKeyFrame(params KeyPlayer[] kps){foreach (KeyPlayer kp in kps) if (IsKeyFrame(kp)) return kp;return KeyPlayer.NO;}

    /// <summary>
    /// Devuelve el indice que FUE presionada en el frame
    /// </summary>
    public static int KnowIndexKeyHold(params KeyPlayer[] kps) { for (int i = 0; i < kps.Length; i++) { if (IsKeyHold(kps[i])) return i; } return -1; }
    /// <summary>
    /// Devuelve el indice que FUE presionada en el frame
    /// </summary>
    public static int KnowIndexKeyFrame(params KeyPlayer[] kps){for (int i = 0; i < kps.Length; i++){if (IsKeyFrame(kps[i])) return i;}return -1;}

    


    //------

    /// <summary>
    ///  Comprueba si poseemos una tecla 
    /// </summary>
    public static bool IsKeyExist(KeyPlayer k) => !k.Equals(KeyPlayer.NO);




    /// <summary>
    /// Presionas el botón, accionando su proceso
    /// </summary>
    public static void CastButton(Button b) { b.Select(); b.onClick.Invoke(); }
    /// <summary>
    /// Comprueba si el botón FUE presionado en el frame y si es interactuable
    /// </summary>
    public static bool IsButtonFrame(Button b, KeyPlayer[] k) => IsKeyFrame(k) && b.interactable;
    /// <summary>
    /// Presiona el boton si botón FUE presionado en el frame y si es interactuable
    /// </summary>
    public static void CastButtonFrame(Button b, params KeyPlayer[] k){if (IsButtonFrame(b, k)) CastButton(b);}
    /// <summary>
    /// Selecciona el boton si existe interacción
    /// </summary>
    public static void SelectButtonFrame(Button b, params KeyPlayer[] k) { if (IsButtonFrame(b, k)) b.Select(); }


    
    /// <summary>
    /// Cargamos los Codes basado en los datos que poseemos del keys
    /// </summary>
    private void LoadCodes()
    {
        //Conocemos la dimención de los codigos basado en la longitud de los
        //codes y las teclas
        New(out codes, KeyData.codeLenght * keys.Length);
        int c = 0;

        //por cada key... vamos a recorrer sus codes y asignarlos a un arreglo
        for (int x = 0; x < keys.Length; x++){
            foreach (KeyCode code in keys[x].keyCodes)codes[c++] = code; 
        }
    }
    ///// <summary>
    ///// Revisamos si la tecla fue presionada y, dependiendo devolveremos:
    ///// <para>-1, 0, 1</para>
    ///// <para>el keys[0] es el que suma, y keys[1] resta, solo tomará los 2 primeros del arreglo</para>
    ///// </summary>
    public static int GetAxisOf(KeyPlayer[] keys, int _val = 0)
    {

        for (int x = 0; x < 2; x++)
        {
            if (ControlSystem.IsKeyHold(keys[x]))
            {
                _val += XavHelpTo.Change.BoolToInt(x.Equals(0));
            }
        }
        //sino...
        return _val;
    }


    ////TODO revisar
    //public static Vector3 GetAxis(Vector3 axis = default)
    //{
    //    KeyPlayer[][] axisKeys ={
    //        keysHorizontal,
    //        keysVertical,
    //        keysForward
    //    };
    //    PrintX(axis);
    //    //keysMovement
    //    for (int x = 0; x < 3; x++){

    //        bool c = IsKeyHold(axisKeys[x]);
    //        //si toco una tecla
    //        if (c) {
    //            foreach (KeyPlayer kp in axisKeys[x]){   
    //                axis[x] += XavHelpTo.Change.BoolToInt(KnowIndexKeyHold(axisKeys[x]).Equals(0) );
    //            }
    //        }
    //    }

    //    return axis;
    //}
    #endregion
}
