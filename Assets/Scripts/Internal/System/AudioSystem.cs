#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioListener))]
public class AudioSystem : MonoInit
{
    #region Variables
    public static AudioSystem _;
    [Header("AudioSystem Settings")]
    public AudioSource src_music;

    #endregion
    #region events
    private new void Awake()
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

        Begin();
    }
    public override void Init()
    {
        throw new System.NotImplementedException();
        //Cargamos las configuraciónes
    }
    #endregion
    #region Methods

    /// <summary>
    /// Set a music to sound
    /// </summary>
    public static void SetThisMusic(AudioClip clip)
    {
        _.src_music.clip = clip;

    }

    #endregion
}

