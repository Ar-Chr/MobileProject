using Ball.Entities.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Ball
{
    public class PlayerSounds : MonoBehaviour
    {
        [SerializeField] Health health;
        [SerializeField] BumpDetector bumpDetector;
        [SerializeField] AudioSource source;
        [Space]
        [SerializeField] List<RandomClip> pops;

        private void Reset()
        {
            source = GetComponent<AudioSource>();
        }

        void Awake()
        {
            source = gameObject.GetComponent<AudioSource>();
            transform.parent = null;

            health.Died += HandleDied;
            bumpDetector.OnBump += HandleBump;
        }

        void HandleDied()
        {
            pops[Random.Range(0, pops.Count)].Play(source);
        }

        private void HandleBump(BumpDetector.ImpactLevel impactLevel)
        {
            source.PlayOneShot(impactLevel.sound, 1);
        }

        private void OnDestroy()
        {
            health.Died -= HandleDied;
            bumpDetector.OnBump -= HandleBump;
        }
    }
}
