using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ball
{
    public class Hittable : MonoBehaviour
    {
        [SerializeField] HittableSettings settings;
        [SerializeField] AudioSource source;

        public void Hit()
        {
            source.PlayOneShot(settings.HitSound);
        }

        public void HitBullet(Vector3 hitPosition, Vector3 hitDirection)
        {
            source.PlayOneShot(settings.HitSound);
            Instantiate(settings.HitParticles, hitPosition, Quaternion.LookRotation(hitDirection));
            Instantiate(settings.HitDecal, hitPosition, Quaternion.identity);
        }
    }
}
