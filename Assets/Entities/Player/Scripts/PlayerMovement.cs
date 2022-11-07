using System.Collections;
using UnityEngine;

namespace Ball.Entities.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] new Rigidbody rigidbody;
        [SerializeField] new Camera camera;
        [Header("Settings")]
        [SerializeField] float forceModifier;
        [SerializeField] float maxSpeed;
        [SerializeField] float stoppingSpeed;

        float sqrMaxSpeed;

        void Reset()
        {
            if (rigidbody == null)
                rigidbody = GetComponent<Rigidbody>();

            if (camera == null)
                camera = Camera.main;
        }

        void Awake()
        {
            TouchDetector.PositionChanged += HandleTouchPositionChanged;
            TouchDetector.PositionChanged += InterruptStopping;
            TouchDetector.TapDetected += Stop;
        }

        private void Start()
        {
            sqrMaxSpeed = maxSpeed * maxSpeed;
        }

        void OnDestroy()
        {
            TouchDetector.PositionChanged -= HandleTouchPositionChanged;
            TouchDetector.PositionChanged -= InterruptStopping;
            TouchDetector.TapDetected -= Stop;
        }

        void HandleTouchPositionChanged(Vector2 deltaPosition)
        {
            float camYRot = Mathf.Deg2Rad * camera.transform.eulerAngles.y;
            Vector3 forwardMovement = deltaPosition.y * new Vector3(Mathf.Sin(camYRot), 0, Mathf.Cos(camYRot));
            Vector3 sideMovement = deltaPosition.x * new Vector3(Mathf.Cos(camYRot), 0, -Mathf.Sin(camYRot));
            Vector3 movement = forwardMovement + sideMovement;
            Move(movement);
        }

        void Move(Vector3 movement)
        {
            if (rigidbody.velocity.WithY(0).sqrMagnitude > sqrMaxSpeed)
                return;

            movement *= forceModifier;
            rigidbody.AddForce(movement);

            Vector3 torque = new Vector3(movement.z, 0, -movement.x) * 2;
            rigidbody.AddTorque(torque);
        }

        #region Stopping

        Coroutine stoppingCoroutine;

        void Stop()
        {
            stoppingCoroutine = StartCoroutine(StoppingCoroutine());
        }

        void InterruptStopping(Vector2 deltaPosition)
        {
            if (stoppingCoroutine == null)
                return;

            StopCoroutine(stoppingCoroutine);
        }

        IEnumerator StoppingCoroutine()
        {
            while (rigidbody.velocity.sqrMagnitude > stoppingSpeed * stoppingSpeed)
            {
                Vector3 horizontalVelocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
                Vector3 stoppingVector = -horizontalVelocity.normalized * stoppingSpeed;
                rigidbody.AddForce(stoppingVector);
                yield return null;
            }
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
        }

        #endregion
    }
}