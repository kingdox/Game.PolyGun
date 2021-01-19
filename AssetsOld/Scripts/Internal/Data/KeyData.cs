#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;
#endregion
namespace Key
{
    #region  KeyData
    public class KeyData
    {
        private static readonly Key[] keys;
        public static readonly int codeLenght = 1;
        //STARt
        private delegate Key KeyPlayerRecipe(KeyPlayer kP);
        private readonly static KeyPlayerRecipe key = (KeyPlayer kP) => new Key(kP, DefaultKeyCode(kP));

        private delegate Key[] KeysRecipe(int qty);
        private readonly static KeysRecipe keysQty = (int qty) => KeyTransformer(qty);


        /// <summary>
        /// Asignamos los datos basado en la cantidad de llaves que posee el jugador
        /// Esto solo debe iniciarse una vez
        /// </summary>
        static KeyData()
        {
            keys = keysQty(9);
        }


        /// <summary>
        /// Asignamos en ordern la cantidad de llaves que hay
        /// </summary>
        public static Key[] KeyTransformer(int qty)
        {
            Key[] ks = new Key[qty];
            for (int x = 0; x < qty; x++) ks[x] = key((KeyPlayer)x);
            return ks;
        }

        /// <summary>
        /// Dependiendo de la tecla tendrá
        /// la cantidad de opciones disponibles...
        /// </summary>
        public static KeyCode[] DefaultKeyCode(KeyPlayer keyPlayer)
        {

            //de momento solo posee un solo nivel
            KeyCode[] codes = new KeyCode[codeLenght];

            switch (keyPlayer)
            {
                //Accion opciones
                case KeyPlayer.BACK:
                    codes[0] = KeyCode.Escape;
                    break;
                case KeyPlayer.OK_FIRE:
                    codes[0] = KeyCode.Space;
                    break;

                // Movimiento / Navegación
                case KeyPlayer.LEFT:
                    codes[0] = KeyCode.LeftArrow;
                    break;
                case KeyPlayer.RIGHT:
                    codes[0] = KeyCode.RightArrow;
                    break;
                case KeyPlayer.UP:
                    codes[0] = KeyCode.UpArrow;
                    break;
                case KeyPlayer.DOWN:
                    codes[0] = KeyCode.DownArrow;
                    break;
                // Inventario / almacenamiento
                case KeyPlayer.C:
                    codes[0] = KeyCode.C;
                    break;
                case KeyPlayer.V:
                    codes[0] = KeyCode.V;
                    break;
                case KeyPlayer.B:
                    codes[0] = KeyCode.B;
                    break;
            }


            return codes;
        }

        public Key[] _GetAllKeys() => keys;
    }
    #endregion
    #region  Key
    [System.Serializable]
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


        public bool Contains(KeyCode k)
        {
            foreach (KeyCode c in keyCodes) if (c == k) return true;
            return false;
        }

        //public bool isPressed()
    }
    #endregion
}




/*
 Controles alternativos
    - Los de PS4
    - moverse => WASD, atack => SPACE, almacenamiento => flechas izq, abajo, derecha
*/