#region imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Key;
using Environment;
using UnityEngine.EventSystems;
#endregion
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
        if (_ == null){ DontDestroyOnLoad(gameObject); _ = this;}
        else if (_ != this) Destroy(gameObject);
        canInput = true;
    }
    private void Start() => LoadCodes();
    #endregion
    #region Methods
    /// <summary>
    /// Comprobamos si la tecla esta siendo presionada
    /// </summary>
    public static bool KeyPress(KeyPlayer kp) => Input.GetKey(_.codes[(int)kp]);
    public static bool KeyPress(int i) => Input.GetKey(_.codes[i]);
    /// <summary>
    /// Devolvemos si la tecla fue presionada o no
    /// Esta tecla detecta cuando en este frame ha sido presionado
    /// </summary>
    public static bool KeyDown(KeyPlayer kp) => Input.GetKeyDown(_.codes[(int)kp]);
    public static bool KeyDown(int i) => Input.GetKeyDown(_.codes[i]);
    public static bool KeyDown(params int[] ks){
        foreach (int k in ks) if (KeyDown(k)) return true;
        return false;
    }
    public static bool KeyDown(params KeyPlayer[] kps){
        foreach (KeyPlayer kp in kps) if (KeyDown(kp)) return true;
        return false;   
    }

    /// <summary>
    /// Sabemos si se toco la tecla 1 vez en el frame
    /// </summary>
    public static KeyPlayer KnowKey(params KeyPlayer[] kps)
    {
        foreach (KeyPlayer kp in kps) if (KeyDown(kp)) return kp;
        return KeyPlayer.NO;
    }

    /// <summary>
    /// Sabemos si una tecla mantiene presionada
    /// <para>Por defecto devuelve <see cref="KeyPlayer.NO"/></para>
    /// </summary>
    public static KeyPlayer KnowKeyHold(params KeyPlayer[] kps)
    {
        foreach (KeyPlayer kp in kps) if (KeyPress(kp)) return kp;
        return KeyPlayer.NO;
    }

    /// <summary>
    /// Presiona el boton si existe interacción
    /// </summary>
    public static void ButtonDown(Button b, params KeyPlayer[] k){if (IsDown(b, k)) Cast(b);}
    /// <summary>
    /// Selecciona el boton si existe interacción
    /// </summary>
    public static void ButtonSelect(Button b, params KeyPlayer[] k) { if (IsDown(b, k)) b.Select(); }
    /// <summary>
    /// Presionas el botón
    /// </summary>
    public static void Cast(Button b) { b.Select(); b.onClick.Invoke(); }
    /// <summary>
    /// Revisa si el boton fue tocado
    /// </summary>
    public static bool IsDown(Button b, KeyPlayer[] k) => KeyDown(k) && b.interactable;
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
