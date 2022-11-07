using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ball
{
    [CreateAssetMenu(fileName = "MusicSet", menuName = "Scriptable Objects/Music Set")]
    public class MusicSet : ScriptableObject
    {
        [SerializeField] AudioClip[] music;

        public AudioClip[] Music => music;
    }
}
