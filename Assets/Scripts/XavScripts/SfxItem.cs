using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            int _newClipIndex = XavHelpTo.GetDifferentInt(clips.Length, lastClipIndex);
            //XavHelpTo.ZeroMax(clips.Length); // Random.Range(0, clips.Length);
            //Siempre que no se repita  y que haya mas de 1 clip se hará reroll
            //while (lastClipIndex == _newClipIndex && clips.Length > 1 )
            //{
            //    _newClipIndex = Random.Range(0, clips.Length);
            //}

            //Reproduce uno de los sonidos
            source.clip = clips[_newClipIndex];
            source.Play();
        }
    }

}
//MusicSystem.ReproduceSound(MusicSystem.SfxType.Coin);
