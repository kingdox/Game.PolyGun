#region import
using UnityEngine;
using Environment;
#endregion
namespace Key
{
    #region  KeyData
    public class KeyData
    {
        private static readonly Key[] keys;
        public const int CODE_LENGTH = 2;
        private const int KEYS_QTY = 9;
        //STARt
        private delegate Key KeyPlayerRecipe(int kP);
        private readonly static KeyPlayerRecipe key = (int kP) => new Key((KeyPlayer)kP, DefaultKeyCode(kP));
        // END
    


        /// <summary>
        /// Asignamos los datos basado en la cantidad de llaves que posee el jugador
        /// Esto solo debe iniciarse una vez
        /// </summary>
        static KeyData()
        {
            //añade todas las llaves con sus variaciones para cada diferente tipo de grupo de controles
            keys = KeyTransformer();
        }


        /// <summary>
        /// Asignamos en ordern la cantidad de llaves que hay
        /// </summary>
        public static Key[] KeyTransformer()
        {
            Key[] ks = new Key[KEYS_QTY];

            for (int x = 0; x < KEYS_QTY; x++) ks[x] = key(x);
            return ks;
        }

        /// <summary>
        /// Dependiendo de la tecla tendrá
        /// la cantidad de opciones disponibles...
        /// </summary>
        public static KeyCode[] DefaultKeyCode(int keyPlayer)
        {

            //de momento solo posee un solo nivel
            KeyCode[] codes = new KeyCode[CODE_LENGTH];


            KeyCode[] defaultKeyCodes =
            {
                KeyCode.Escape,
                KeyCode.Space,
                KeyCode.LeftArrow,
                KeyCode.RightArrow,
                KeyCode.UpArrow,
                KeyCode.DownArrow,
                KeyCode.C,
                KeyCode.V,
                KeyCode.B
            };
            //esto podría estar mejor...
            KeyCode[] alternativeKeyCodes =
           {
                KeyCode.Escape,
                KeyCode.Space,
                KeyCode.A,
                KeyCode.D,
                KeyCode.W,
                KeyCode.S,
                KeyCode.LeftArrow,
                KeyCode.DownArrow,
                KeyCode.RightArrow
            };

            codes[0] = defaultKeyCodes[keyPlayer];
            codes[1] = alternativeKeyCodes[keyPlayer];

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