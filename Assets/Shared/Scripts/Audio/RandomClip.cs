using R60N.Utility;
using UnityEngine;

[System.Serializable]
public class RandomClip
{
    public AudioClip clip;
    public MinMaxFloat randomPitch;
    public MinMaxFloat randomVolume;

    public void Play(AudioSource source)
    {
        source.pitch = randomPitch.GetRandom();
        source.volume = randomVolume.GetRandom();
        source.PlayOneShot(clip);
    }
}