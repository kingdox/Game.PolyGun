#region 
using UnityEngine;
using XavLib;
#endregion
//[System.Serializable]
[RequireComponent(typeof(AudioSource))]
public class SfxItem : MonoX
{
    #region Var
    private AudioSource source;

    public AudioClip[] clips;
    public int lastClipIndex;
    #endregion
    #region Event
    private void Awake()
    {
        Get(out source);
        SetClip();

    }
    #endregion
    #region Method

    /// <summary>
    /// Sets a random clip from te array of clips
    /// </summary>
    public void SetClip()
    {
        lastClipIndex = XavHelpTo.Know.DifferentIndex(clips.Length, lastClipIndex);
        source.clip = clips[lastClipIndex];
    }

    /// <summary>
    /// Check if enable or disable the sound
    /// </summary>
    public void PlayStop(bool c)
    {
        XavHelpTo.Change.ActiveAudioSource(source, c);
    }


    /// <summary>
    /// Play a random sound of the item
    /// </summary>
    public void PlaySound(){

        //Si no está tocando la rola
        if (!source.isPlaying)
        {
            SetClip();
            source.Play();
        }
    }
    /// <summary>
    /// Returns the audio src like a ref to manage it
    /// </summary>
    public AudioSource GetAudioSrc() => source;

    #endregion
}