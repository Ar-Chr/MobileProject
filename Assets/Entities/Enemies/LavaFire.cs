using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ball
{
    public class LavaFire : MonoBehaviour
    {
        [SerializeField] ParticleSystem particles;
        [SerializeField] new Collider collider;
        [SerializeField] float activeTime;
        [SerializeField] float inactiveTime;
        [SerializeField] float colliderSwitchDelay;

        [SerializeField] float timer;
        bool active;

        private void Update()
        {
            if (active)
            {
                timer += Time.deltaTime;
                if (timer > activeTime)
                {
                    SetActive(false);
                    timer = 0;
                }
            }
            else
            {
                timer += Time.deltaTime;
                if (timer > inactiveTime)
                {
                    SetActive(true);
                    timer = 0;
                }
            }
        }

        void SetActive(bool active)
        {
            this.DelayAction(() => collider.enabled = active, colliderSwitchDelay);
            var emission = particles.emission;
            emission.enabled = active;
            this.active = active;
        }
    }
}
