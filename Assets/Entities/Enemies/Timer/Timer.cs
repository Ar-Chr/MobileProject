using Ball.Shared.GameOver;
using UnityEngine;

namespace Ball.Enemies
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] float time;
        [SerializeField] bool active = true;

        public float TimeLeft => time;

        void Start()
        {
            GameOverController.OnGameEnded += HandleGameEnded;
        }

        void Update()
        {
            if (!active)
                return;

            if (time <= 0)
            {
                time = 0;
                GameOverController.InvokeLoss(new OutOfTime());
                active = false;
            }

            time -= Time.deltaTime;
        }

        void HandleGameEnded()
        {
            active = false;
        }
    }
}