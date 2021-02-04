#region 
using UnityEngine;
using XavHelpTo.Know;
using xGet = XavHelpTo.Get.Get;
using XavHelpTo.Change;
#endregion
//[System.Serializable]
[RequireComponent(typeof(AudioSource))]
public class SfxItem : MonoX
{
    #region Var
    private AudioSource source;

    public AudioClip[] clips;
    public int lastClipIndex;

    /// <summary>
    /// Esta si aumenta podrá ser variacion entre positivo y negativo
    /// </summary>
    public float pitchVariation = 0;

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
        lastClipIndex = Know.DifferentIndex(clips.Length, lastClipIndex);
        source.clip = clips[lastClipIndex];
        pitchVariation = 1 + xGet.MinusMax(pitchVariation);

    }

    /// <summary>
    /// Check if enable or disable the sound
    /// </summary>
    public void PlayStop(bool c)
    {
        Change.ActiveAudioSource(source, c);
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


    public void EndSoundIn(Transform tr)
    {
        transform.parent = tr;

        Destroy(gameObject, source.clip.length);
    }

    /// <summary>
    /// Returns the audio src like a ref to manage it
    /// </summary>
    public AudioSource GetAudioSrc() => source;

    #endregion
}