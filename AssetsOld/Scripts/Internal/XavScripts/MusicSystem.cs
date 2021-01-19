#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;
using XavLib;
#endregion

public class MusicSystem : MonoBehaviour
{
    #region Var
    private static MusicSystem _;

    private bool isSoundOn = true;

    //-> los path de las canciones
    public enum MusicPath {
        No,
    };

    //-> los path de los sfx, su orden determina los hijos del sfx
    public enum SfxType{
        No
    }



    //private string musicPath = "Sound/Music/";


    [Header("Music")]
    public AudioSource audio_music;
    public AudioClip clip_music;
    [Space]
    public AudioClip[] musicRef;

    [Header("Sfx")]
    //SfxItem
    public SfxItem[] sfxItems;


    #endregion
    #region Events
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
    
    #endregion
    #region Methods


    /// <summary>
    /// Activamos o desactivamos el sonido
    /// </summary>
    /// <param name="condition"></param>
    public static void SetSound(bool condition){

        _.isSoundOn = condition;

        if (condition)
        {
            CheckMusic(true);
        }
        else
        {
            _.StopMusic();
        }
    }


    /// <summary>
    /// Revisamos si la musica puede sonar y qué musica poner,
    /// si está sonando la misma que corresponde no hace nada
    /// </summary>
    public static void CheckMusic(bool bypass = false){
        if (CanSound())
        {
            Scenes _activeScene = XavHelpTo.Know.ActiveScene();
            MusicPath key = MusicPath.No;


            //TODO debe haber escenas que puedan contarse la misma canción

            //key = (int)_activeScene

            //switch (_activeScene)
            //{
            //    case Scenes.MenuScene:
            //    case Scenes.InstructionScene:
            //        key = (MusicPath)1;

            //        break;
            //    case Data.Scenes.PreparationScene:
            //        key = (MusicPath)3;

            //        break;
            //    case Data.Scenes.GameScene:
            //        key = (MusicPath)2;
            //        break;
            //    default:
            //        //nada supongo
            //        break;
            //}

           PlayThisMusic(key, bypass);
        }
        else
        {
            _.StopMusic();
        }

    }


    /// <summary>
    /// Reproduce la musica que ha sido referenciada en el arreglo
    /// //TODO revisar esto
    /// </summary>
    public static void PlayThisMusic(MusicPath path = MusicPath.No, bool byPass = false){
        if (path != MusicPath.No && CanSound())
        {
            //esto te lleva a picos en el rendimiento, mejor referenciar
            //_.clip_music = Resources.Load<AudioClip>(_.musicPath + path);

            _.clip_music = _.musicRef[(int)path];

            if (!_.clip_music.Equals(_.audio_music.clip) || byPass)
            {

                Debug.Log($"Reproduciendo : {path}");
                _.audio_music.clip = _.clip_music;
                _.audio_music.Play();
            }
        }
        else
        {
            _.StopMusic();
        }
    }

    /// <summary>
    /// Detenemos la musica actual
    /// </summary>
    private void StopMusic()
    {
        audio_music.Stop();
        clip_music = null;

    }

    /// <summary>
    /// Establecemos el volumen de la musica
    /// </summary>
    /// <param name="v"></param>
    public static void SetVolume(float v)
    {
        _.audio_music.volume = v;
    }

    /// <summary>
    /// Preguntamos si la musica esta permittida o no
    /// </summary>
    /// <returns></returns>
    public static bool CanSound() => _.isSoundOn;
    #endregion


    /// <summary>
    /// Inicia el sonido correspondiente
    /// </summary>
    public static void ReproduceSound(SfxType type){
        if (CanSound())
        {
            _.sfxItems[(int)type].PlaySound();

        }
    }
}

/*
 
  float percent = DataFunc.KnowPercentOfMax(index, fars.Length) / 100;
            MusicSystem.SetVolume(percent);
 
 */
