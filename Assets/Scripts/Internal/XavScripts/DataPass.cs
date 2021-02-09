#region ####################### IMPLEMENTATION
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Environment;
using System;
#endregion
#region ### DataPass
/// <summary>
/// Encargado de ser la conexión de los datos guardados con las escenas
/// Este podrá cargar el ultimo archivo o guardar un archivo con sus datos
/// <para>Dependencias: <seealso cref="Data"/>, <seealso cref="SavedData"/>, <seealso cref="DataStorage"/></para>
/// </summary>
public class DataPass : MonoBehaviour
{
    #region ####### VARIABLES

    [HideInInspector]
    private static DataPass _;//Singleton....

    [HideInInspector]
    public bool isReady = false;

    [Header("Saved Data")]
    [SerializeField]
    private SavedData savedData = new SavedData();
    //public event Action Ready;

    #endregion
    #region ###### EVENTS
    private void Awake()
    {
       //XavHelpTo.WordCount
       //"".WordCount
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

    }
    private void Start() => DataInit();
    #endregion
    #region ####### METHODS
    //public int value;
    //public static void Singleton(this bool s)//{//}

    /// <summary>
    /// Revisamos si existen datos guardados, de no existir los crea
    /// </summary>
    private void DataInit()
    {
        isReady = false;
        string path = Application.persistentDataPath + Data.data.savedPath;
        //Debug.Log($"El archivo Existe?? {File.Exists(path)}, Ruta: {path}");

        SaveLoadFile(!File.Exists(path));
        isReady = true;
        //Ready.Invoke();
    }

    /// <summary>
    /// Guardamos ó cargamos el archivo que poseeremos para contener los datos
    /// que se guardan en un archivo
    /// </summary>
    /// <param name="wantSave"></param>
    public static void  SaveLoadFile(bool wantSave = false)
    {
        string _path = Application.persistentDataPath + Data.data.savedPath;
        BinaryFormatter _formatter = new BinaryFormatter();
        FileStream _stream = new FileStream(_path, wantSave ? FileMode.Create : FileMode.Open);
        DataStorage _dataStorage;
       
        //Dependiendo de si va a cargar o guardar hará algo o no
        if (wantSave)
        {
            _.savedData.debug_savedTimes++;
            _dataStorage = new DataStorage(GetSavedData());
            _formatter.Serialize(_stream, _dataStorage);
            _stream.Close();

           // Debug.Log($"Archivo {Data.data.savedPath} Guardado {GetSavedData().debug_savedTimes} veces !");
        }
        else
        {
            _dataStorage = _formatter.Deserialize(_stream) as DataStorage;
            _stream.Close();
            SetData(_dataStorage.savedData);
            //_.savedData = _dataStorage.savedData;
        }
    }

    /// <summary>
    /// Tomamos los datos que ya estén cargados
    /// </summary>
    public static SavedData GetSavedData() => _.savedData;

    /// <summary>
    ///  Inserta los nuevos datos que poseerá dataPass en su "SavedData"
    /// </summary>
    /// <param name="newSavedData"></param>
    public static void SetData(SavedData newSavedData) => _.savedData = newSavedData;

    /// <summary>
    /// Preguntamos si <seealso cref="DataPass"/> está listo
    /// </summary>
    public static bool IsReady() => _.isReady;

#endregion
}
#endregion
#region DataStorage y SavedData
/// <summary>
/// Encargado de hacer que, con un constructor se agreguen los nuevos valores
/// <para>Dependencias => <seealso cref="SavedData"/></para>
/// </summary>
[System.Serializable]
public class DataStorage
{
    //aquí se vuelve a colocar los datos puestos debajo...
    public SavedData savedData = new SavedData();
    //Con esto podremos guardar los datos de datapass a DataStorage
    public DataStorage(SavedData savedData) => this.savedData = savedData;
}

/// <summary>
/// Este es el modelo de datos que vamos a guardar y manejar
/// para los archivos que se crean... Estos datos internos pueden cambiar para los proyectos...
/// <para>
///     Aquí almacenamos los datos internos del juego
/// </para>
/// </summary>
[System.Serializable]
public struct SavedData
{
    //tutorial completado?
    public bool isIntroCompleted;


    // index del enum de Idiom
    public int idiom;

    // (index) velocidad de los textos
    public int textSpeed;

    // (index)porcentaje de volumen de la musica
    public int musicVolume;

    // (index) cantida de sonido que tendrá
    public int sfxVolume;

    // (index)tipo de control que usa el personaje
    public int control; 

    // Arreglo ordenado de los valores de cada logro correspondiente
    public float[] achievements;
    [Space]
    public int record_waves;


    /// <summary>
    /// EXTRA, Leftover QTY
    /// </summary>
    public int leftoverQty;



    //usado para obtener una instancia nueva con los valores sin ser referenciados...
    public SavedData(SavedData s)
    {
        this = s;

        //hacemos por separado los valores que se quiere guardar por separado
        this.achievements = new float[s.achievements.Length];
        this.record_waves = s.record_waves;

        for (int x = 0; x < s.achievements.Length; x++)
        {
            this.achievements[x] = s.achievements[x];
        }


    }
    //Extra Debug ?
    [Space(10)]
    [Header("Debug Area")]
    public int debug_savedTimes;
};
#endregion
