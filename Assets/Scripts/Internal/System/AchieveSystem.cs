#region Imports
using UnityEngine;
using Achievements;
using XavLib;
using Environment;
#endregion
public class AchieveSystem : MonoX
{
    #region Variables
    private readonly Achievement[] achievements = Data.data.GetAchievements();
    private static AchieveSystem _;

    [Header("Achieve Unlock Settings")]
    
    private float achieveShowCount = 0;

    public static int achievementLenght;
    public static bool unlockShow = false;

    private AchievementItem achieveUnlockItem;
    private RectTransform rect_unlockItem;
    #endregion
    #region Events
    private void Awake()
    {
        //Singleton corroboration
        if (_ == null){
            DontDestroyOnLoad(gameObject);
            _ = this;
        }else if (_ != this){ Destroy(gameObject);}

        achievementLenght = achievements.Length;

    }
  
    private void Update()
    {
        if (_.achieveUnlockItem != null)
        {
            HideShowUnlock();


            //si pasa ese tiempo mientras se está mostrando en pantalla el  modal lo cierra
            if (unlockShow && Timer(ref achieveShowCount, Data.RECORD_ACHIEVE_TIMER))  
            {
                unlockShow = false;

            }

        }
    }
    #endregion
    #region Methods

    
   

    /// <summary>
    /// Colocamos la referencia del item unlocker de esta pantalla
    /// </summary>
    public static void SetUnlockItem(AchievementItem item){
        //unlockShow = false;
        _.achieveUnlockItem = item;
        _.rect_unlockItem = _.achieveUnlockItem.GetComponent<RectTransform>();

        //Colocamos las variables en un inicio sin mostrarse
        _.rect_unlockItem.anchorMin.Set(0, 1);
        _.rect_unlockItem.anchorMax = new Vector2(1, 2);

    }

    /// <summary>
    /// Esconde o muestra la pantalla
    /// </summary>
    private void HideShowUnlock(){

        int minY = unlockShow ? 0 : 1;
        //int maxY = unlockShow ? 1 : 2;

        Vector2 newMin = new Vector2(0, minY);
        Vector2 newMax = new Vector2(1, 1);

        //me las retorna en 0-1
        newMin.y = XavHelpTo.Set.UnitInTime(rect_unlockItem.anchorMin.y, minY);

        rect_unlockItem.anchorMin = newMin;
        rect_unlockItem.anchorMax = newMin + newMax;
    }
    /// <summary>
    /// Asigna al item los valores
    /// </summary>
    public static void Setitem(int index, AchievementItem item)
    {
        if (index != -1)
        {   
            item.SetItem(new TextValBarItem(
                _.achievements[index].key,
                _.achievements[index].keyDesc,
                _.achievements[index].limit,
                DataPass.GetSavedData().achievements[index]
            ));
        }
    }



    /// <summary>
    /// haremos el manejo de las actualizaciones con datapass y los achievements
    /// esto solo las implementa Y, detecta si hubo una batida de achievement para mostrar el modal
    /// </summary>
    public static void UpdateAchieve(Achieves index, float value = 1, bool overwrite=false) => UpdateAchieve((int)index, value, overwrite);
    public static void UpdateAchieve(int index, float value = 1, bool overwrite = false)
    {
        //se sumará la cantidad asignada
        //tomamos los datos
        SavedData saved = DataPass.GetSavedData();

        //guardamos el valor anterior
        float oldValue = saved.achievements[index];

        //sumamos las cantidades correspondientes
        if (overwrite)
        {
            saved.achievements[index] = value;

        }
        else
        {
            saved.achievements[index] += value;
        }


        //limites
            float[] limitsActual = _.achievements[index].limit.ToArray();

            //revisa ambos valores, el antiguo y el actual y revisa si poseen distinto limit, de ser así se batió un record...
        
            int old_LimitIndex = XavHelpTo.Know.FirstMajor(oldValue, limitsActual);
            int actual_LimitIndex = XavHelpTo.Know.FirstMajor(saved.achievements[index], limitsActual);

            if (old_LimitIndex != actual_LimitIndex )
            {
                //Se batió un record
                PrintX($"Record batido, de {old_LimitIndex} a {actual_LimitIndex}");
                unlockShow = true;
                _.achieveShowCount = 0;
                Setitem(index, _.achieveUnlockItem);

            }
    }

    

    /// <summary>
    /// Retorna los mejores achievements con diferencia de los datos otorgados,
    /// estos deberían ser datos guardados previos, siendo una comparativa entre lo viejo con lo nuevo
    /// esto organizará de los mejores a los peores
    /// </summary>
    public static int[] GetBestAchievements(float[] oldAchievements)
    {
        //int[] newAcheiveOrder = new int[oldAchievements.Length];
        int[] newAchieve = { -1, -1, -1 };

        float[] pcts = new float[achievementLenght];

        float[] savedAchieve = DataPass.GetSavedData().achievements;



        //calculo maximo
        int maxValIndex = 0;
        for (int x = 0; x < achievementLenght; x++)
        {
            float count = savedAchieve[x] - oldAchievements[x];
            float max = _.achievements[x].limit.gold;
            pcts[x] = XavHelpTo.Get.PercentOf(count, max);
            //PrintX($"count - {count}, max -{max}, pct{pcts[x]}");

            if (pcts[x] >  pcts[maxValIndex])
            {
                //actualizamos el indice
                maxValIndex = x;
            }

        }


        //bool isRepeated = false;

        //recorreremos
        for (int j = 0; j < newAchieve.Length; j++)
        {
            //asignamos el indice del mayor valor
            newAchieve[j] = maxValIndex;

            maxValIndex = KnowInferiorVal(maxValIndex, pcts);

        }


        //Cambiamos los valores que sean iguales con cualquier otro del arreglo
        newAchieve = XavHelpTo.Set.DifferentIndexInEquals(newAchieve, achievementLenght);




        return newAchieve;
    }


    /// <summary>
    /// Knows the inferior val of an value but greater than the value of the index
    /// </summary>
    private static int KnowInferiorVal(int index, float[] arr)
    {
        //es el ultimo inferior detectado
        int lastInfIndex = 0;

        for (int x = 0; x < arr.Length; x++)
        {   
            //recorro y actualizo el index y reviso si alguno es mayor que el indice PERO menor que el valor
            if (arr[x] > arr[lastInfIndex] &&  arr[x] < arr[index])
            {
                //actualizo el indice inferior
                lastInfIndex = x;
            }
        }

        return lastInfIndex;
    }




    /// <summary>
    /// SET AN achievement saved from datapass in Unlock
    /// </summary>
    /// <param name="i"></param>
    public static void __Debug_SetAchieve(float i)
    {

        Setitem((int)i, _.achieveUnlockItem);
    }
    /// <summary>
    /// Open or close the modal
    /// </summary>
    public static void __Debug_OpenCloseAchieveModal()
    {
        unlockShow = !unlockShow;
    }


    #endregion
}
public enum Achieves
{
    KILLS_ENEMY,
    KILLS_BOSS,
    WAVES_ENEMIES,
    OBJECTS_COLLECTED,
    HEALS_GAME,

    TIME_DEATHLIMIT,
    METTERS_GAME,
    CREATIONS_GAME,
    ESPECIAL_READ,
    ESPECIAL_CHEATS
}
