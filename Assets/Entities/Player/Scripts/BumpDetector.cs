using System;
using UnityEngine;

namespace Ball.Entities.Player
{
    public class BumpDetector : MonoBehaviour
    {
        public event Action<ImpactLevel> OnBump;

        [SerializeField] ImpactLevel[] impactLevels;

        private void OnCollisionEnter(Collision collision)
        {
            for (int i = impactLevels.Length - 1; i >= 0; i--)
            {
                if (collision.impulse.sqrMagnitude > impactLevels[i].sqrThreshold)
                {
                    OnBump?.Invoke(impactLevels[i]);
                    return;
                }
            }
        }

        [Serializable]
        public class ImpactLevel
        {
            public float threshold;
            public AudioClip sound;

            public float sqrThreshold => threshold * threshold;
        }
    }
}