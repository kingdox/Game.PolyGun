#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Options;
#endregion
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioListener))]
public class AudioSystem : MonoInit
{
    #region Variables
    public static AudioSystem _;
    [Header("AudioSystem Settings")]
    public AudioSource src_music;
    [Space]
    public AudioMixer mixer;
    [Space]
    private const string SFX_VOLUME_NAME= "SoundVolume";
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

        //Cargamos las configuraciónes de musica
        SetMusicVolume();
        SetSounds();

    }
    #endregion
    #region Methods

    /// <summary>
    /// Set a music to sound
    /// </summary>
    public static void SetThisMusic(AudioClip clip)
    {
        if (_.src_music.clip != null  && clip.name == _.src_music.clip.name) return;

        //usar una coroutina ?
        _.src_music.clip = clip;

        _.src_music.Play();
    }

    /// <summary>
    /// Sets the volume of the music
    /// </summary>
    public static void SetMusicVolume(float val = -1)
    {
        if (val.Equals(-1))
        {
            //por defecto cargamos las de SavedData
            _.src_music.volume = OptionData.musicVolume[DataPass.GetSavedData().musicVolume];
        }
        else
        {
            //_.src_music.outputAudioMixerGroup.audioMixer.
            _.src_music.volume = val;
        }
    }

    /// <summary>
    /// Sets the sounds or not
    /// </summary>
    public static void SetSounds()
    {
        _.mixer.SetFloat(SFX_VOLUME_NAME, OptionData.sfxVolume[DataPass.GetSavedData().sfxVolume]);
        //PrintX("Cambios");
    }
    #endregion
}

