using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavLib;
//[System.Serializable]
public class SfxItem : MonoBehaviour
{
    //esta es solo para identificador, pero no lo uso
    public MusicSystem.SfxType type;

    private AudioSource source;

    public AudioClip[] clips;
    public int lastClipIndex;


    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }


    public void PlaySound(){

        //Si no está tocando la rola
        if (!source.isPlaying)
        {
            int _newClipIndex = XavHelpTo.Know.DifferentIndex(clips.Length, lastClipIndex);
           
            //Reproduce uno de los sonidos
            source.clip = clips[_newClipIndex];
            source.Play();
        }
    }

}
//MusicSystem.ReproduceSound(MusicSystem.SfxType.Coin);
