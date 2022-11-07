using Ball.Triggers;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ball.Enemies
{
    [Serializable]
    class ChargingShooter
    {
        public event Action OnShot;
        [SerializeField] TriggerActionComposite onShotActions;

        public event Action ChargeStarted;
        public event Action<float> ChargeChanged;
        public event Action ChargeCancelled;

        [SerializeField] Transform bulletSpawn;
        [Space]
        [SerializeField] Projectile bulletPrefab;
        [Space]
        [SerializeField] float muzzleVelocity;
        [SerializeField] float shotChargeTime;

        public Vector3 BulletSpawnPosition => bulletSpawn.position;
        public Quaternion BulletSpawnRotation => bulletSpawn.rotation;

        public float MuzzleVelocity => muzzleVelocity;
        public float ChargeTime => shotChargeTime;

        float shotCharge;
        float ShotCharge
        {
            get => shotCharge;
            set
            {
                shotCharge = value;
                ChargeChanged?.Invoke(shotCharge / shotChargeTime);
            }
        }

        public bool TryShoot()
        {
            if (ShotCharge == 0)
                ChargeStarted?.Invoke();

            ShotCharge += Time.deltaTime;
            if (ShotCharge >= shotChargeTime)
            {
                Shoot();
                return true;
            }
            return false;
        }

        void Shoot()
        {
            ShotCharge = 0;

            Projectile bullet = Object.Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.Initialize(muzzleVelocity);

            OnShot?.Invoke();
            onShotActions.Execute();
        }

        public void ResetCharge()
        {
            if (ShotCharge > 0)
                ChargeCancelled?.Invoke();
            ShotCharge = 0;
        }
    }
}
