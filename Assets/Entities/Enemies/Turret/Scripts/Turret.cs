using Ball.Entities.Player;
using Ball.Shared;
using Ball.Shared.GameOver;
using System;
using UnityEngine;

namespace Ball.Enemies
{
    class Turret : MonoBehaviour
    {
        [SerializeField] FieldOfView fieldOfView;
        [SerializeField] ChargingShooter shooter;
        [Header("Transforms")]
        [SerializeField] Transform targetInitialPosition;
        [SerializeField] Transform headTrackerBone;
        [SerializeField] Transform headBone;
        [SerializeField] Transform bodyBone;
        [Space]
        [SerializeField] GameObject muzzleFlash;
        [Header("Stats")]
        [SerializeField] float turnSpeed;

        public ChargingShooter Shooter => shooter;

        public Vector3 Target { get; private set; }

        IdleState idleState;
        PlayerSpottedState playerSpottedState;

        TurretState currentState;

        new Transform transform;
        Transform playerTransform;

        Health playerHealth;

        Vector3 prevPlayerPosition;
        Vector3 playerVelocity =>
           (playerTransform.position - prevPlayerPosition) / Time.deltaTime;

        bool PlayerInView => fieldOfView.InView(playerTransform.position);
        bool PlayerInRange => fieldOfView.InRange(playerTransform.position);

        void Awake()
        {
            transform = base.transform;
            playerTransform = FindObjectOfType<Player>().transform;
            playerHealth = playerTransform.GetComponent<Health>();

            idleState = new IdleState(this);
            playerSpottedState = new PlayerSpottedState(this);
            currentState = idleState;

            Player.PlayerDespawned += HandlePlayerDespawned;
        }

        void Update()
        {
            currentState.CheckPlayer();
            currentState.Update();
            LookAtTarget();
            prevPlayerPosition = playerTransform.position;
        }

        void OnDestroy()
        {
            Player.PlayerDespawned -= HandlePlayerDespawned;
        }

        void SetState(TurretState newState)
        {
            currentState = newState;
            newState.OnEnter();
        }

        void LookAtTarget()
        {
            Quaternion targetRotation = Quaternion.LookRotation(Target - headTrackerBone.position, transform.up);
            Quaternion trackerRotation = Quaternion.RotateTowards(headTrackerBone.rotation, targetRotation, turnSpeed * Time.deltaTime);
            headTrackerBone.rotation = trackerRotation;

            headBone.localRotation = Quaternion.Euler(headTrackerBone.localEulerAngles.x, 0, 0);
            bodyBone.localRotation = Quaternion.Euler(0, headTrackerBone.localEulerAngles.y, 0);
        }

        public void ResetTarget()
        {
            Target = targetInitialPosition.position;
        }

        void HandlePlayerDespawned(Player player)
        {
            ResetTarget();
        }

        void OnDrawGizmosSelected()
        {
            fieldOfView.OnDrawGizmosSelected();
        }

        abstract class TurretState
        {
            protected Turret turret;

            public TurretState(Turret turret)
            {
                this.turret = turret;
            }

            public abstract void OnEnter();
            public abstract void CheckPlayer();
            public abstract void Update();
        }

        class IdleState : TurretState
        {
            public IdleState(Turret turret) : base(turret)
            {
                this.turret = turret;
            }

            public override void OnEnter()
            {
                turret.shooter.ResetCharge();
            }

            public override void CheckPlayer()
            {
                if (GameOverController.GameEnded)
                    return;

                if (turret.PlayerInRange &&
                    turret.PlayerInView)
                    turret.SetState(turret.playerSpottedState);
            }

            public override void Update()
            {
                turret.ResetTarget();
            }
        }

        class PlayerSpottedState : TurretState
        {
            public PlayerSpottedState(Turret turret) : base(turret)
            {
                this.turret = turret;
            }

            public override void OnEnter() { }

            public override void CheckPlayer()
            {
                if (GameOverController.GameEnded)
                    turret.SetState(turret.idleState);

                if (!turret.PlayerInRange)
                    turret.SetState(turret.idleState);
            }

            public override void Update()
            {
                if (turret.shooter.TryShoot())
                    Instantiate(turret.muzzleFlash, turret.shooter.BulletSpawnPosition, turret.shooter.BulletSpawnRotation);
                
                Vector3? targeterResult = Targeting.TryGetTarget(
                    turret.shooter.BulletSpawnPosition,
                    turret.shooter.MuzzleVelocity,
                    turret.playerTransform.position,
                    turret.playerVelocity);

                if (targeterResult.HasValue)
                    turret.Target = targeterResult.Value;
            }
        }
    }
}