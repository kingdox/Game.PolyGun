#region ####################### IMPLEMENTATION
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
#endregion
#region ### CLASS
public class DataPass : MonoBehaviour
{
    #region ####### VARIABLES

    [HideInInspector]
    public static DataPass _;//Singleton....

    [Header("Saved Data")]
    [SerializeField]
    private SavedData savedData = new SavedData();

    [Header("DataPass info")]
    public bool isReady = false;

    #endregion
    #region ###### EVENTS
    private void Awake()
    {
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

    /// <summary>
    /// Revisamos si existen datos guardados, de no existir los crea
    /// </summary>
    private void DataInit()
    {
        isReady = false;
        string path = Application.persistentDataPath + Data.data.savedPath;
        Debug.Log($"El archivo Existe?? {File.Exists(path)}, Ruta: {path}");

        SaveLoadFile(!File.Exists(path));
        isReady = true;
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

            Debug.Log($"Archivo {Data.data.savedPath} Guardado {GetSavedData().debug_savedTimes} veces !");
        }
        else
        {
            _dataStorage = _formatter.Deserialize(_stream) as DataStorage;
            _stream.Close();
            SetData(_dataStorage.savedData);
            //_.savedData = _dataStorage.savedData;
        }
    }

    /// <returns>Los datos guardados</returns>
    public static SavedData GetSavedData() => _.savedData;

    /// <summary>
    ///  Inserta los nuevos datos que poseerá dataPass en su "SavedData"
    /// </summary>
    /// <param name="newSavedData"></param>
    public static void SetData(SavedData newSavedData) => _.savedData = newSavedData;



    //Esto es solo para DEBUGs
    private void OnDisable()
    {
        Debug.Log("Guardado");
        SaveLoadFile(true);
    }

    #endregion
}
#endregion

#region DataStorage y SavedData
[System.Serializable]
public class DataStorage
{
    //aquí se vuelve a colocar los datos puestos debajo...
    public SavedData savedData = new SavedData();
    //Con esto podremos guardar los datos de datapass a DataStorage
    public DataStorage(SavedData saved)
    {
        savedData = saved;
    }
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
    public int actualmoney;
    public float recordMetersReached;
    public int recordMonstersKilled;
    public int lastMoneySpent;
    public float lastMetersReached;
    public int lastMonstersKilled;

    //Extra
    [Space(10)]
    [Header("Debug Area")]
    public int debug_savedTimes;
};
#endregion
