using System.Collections.Generic;
using UnityEngine;

namespace Ball.Enemies
{
    public class TurretSounds : MonoBehaviour
    {
        [SerializeField] Turret turret;
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioSource chargeSource;
        [Space]
        [SerializeField] List<RandomClip> shotSounds;
        [SerializeField] AudioClip chargeSound;
        [SerializeField] float chargeSoundOffset;

        private void Reset()
        {
            turret = GetComponent<Turret>();
        }

        private void Awake()
        {
            turret.Shooter.OnShot += HandleShot;
            turret.Shooter.ChargeStarted += HandleChargeStarted;
            turret.Shooter.ChargeChanged += HandleChargeChanged;
            turret.Shooter.ChargeCancelled += HandleChargeCancelled;
        }

        void HandleShot()
        {
            shotSounds[Random.Range(0, shotSounds.Count)].Play(audioSource);
        }

        void HandleChargeStarted()
        {
            chargeSource.PlayDelayed(turret.Shooter.ChargeTime - chargeSound.length + chargeSoundOffset);
        }

        void HandleChargeChanged(float chargePercentage)
        {

        }

        void HandleChargeCancelled()
        {
            chargeSource.Stop();
        }
    }
}