using Ball.Triggers;
using UnityEngine;

namespace Ball.Enemies
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] LayerMask layerMask;
        [Header("On Hit")]
        [SerializeField] TriggerActionComposite genericActions;
        [SerializeField] TriggerActionComposite<GameObject> gameObjectActions;

        float muzzleVelocity;

        Vector3 velocity;
        Vector3 prevPosition;

        public void Initialize(float muzzleVelocity)
        {
            this.muzzleVelocity = muzzleVelocity;
        }

        void Start()
        {
            prevPosition = transform.position;
            velocity = transform.forward * muzzleVelocity;
        }

        void FixedUpdate()
        {
            Move();
            //ApplyGravity();

            CheckCollision();

            prevPosition = transform.position;
        }

        void Move()
        {
            float time = Time.deltaTime;

            Vector3 movement =
                velocity * time +
                Physics.gravity * time * time / 2;

            transform.position += movement;
        }

        void ApplyGravity()
        {
            velocity += Physics.gravity * Time.deltaTime;
        }

        void CheckCollision()
        {
            Vector3 direction = transform.position - prevPosition;
            Ray ray = new Ray(prevPosition, direction);
            if (Physics.Raycast(ray, out RaycastHit hit, direction.magnitude, layerMask))
                Collided(hit);
        }

        void Collided(RaycastHit hit)
        {
            genericActions.Execute();
            gameObjectActions.Execute(hit.collider.gameObject);
        }
    }
}
