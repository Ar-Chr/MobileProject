using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ball
{
    public class HittableSettings : ScriptableObject
    {
        [SerializeField] GameObject hitDecal;
        [SerializeField] GameObject hitParticles;
        [SerializeField] AudioClip[] hitSounds;

        public GameObject HitDecal => hitDecal;
        public GameObject HitParticles => hitParticles;
        public AudioClip HitSound => hitSounds[Random.Range(0, hitSounds.Length)];
    }
}
