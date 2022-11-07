using Ball.Entities.Player;
using UnityEngine;

namespace Ball
{
    public class KillPlayerOnFall : MonoBehaviour
    {
        [SerializeField] Transform playerTransform;
        [SerializeField] Health playerHealth;

        private void Reset()
        {
            if (!playerHealth)
                playerHealth =
                    FindObjectOfType<Player>()
                    ?.GetComponent<Health>();

            if (!playerTransform)
                playerTransform =
                    FindObjectOfType<Player>()
                    ?.transform;
        }

        private void FixedUpdate()
        {
            if (playerTransform && playerTransform.position.y < transform.position.y)
                playerHealth.TakeDamage(playerHealth.CurrentHealth);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector3(0, transform.position.y, 0), new Vector3(20, 0, 20));
        }
    }
}
