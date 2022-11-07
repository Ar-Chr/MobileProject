using UnityEngine;

namespace Ball.Enemies
{
    public class Spin : MonoBehaviour
    {
        [SerializeField] float degreesPerSecond;

        new Transform transform;

        private void Start()
        {
            transform = base.transform;
        }

        private void FixedUpdate()
        {
            transform.Rotate(0, degreesPerSecond * Time.deltaTime, 0);
        }
    }
}
