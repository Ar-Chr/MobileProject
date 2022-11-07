using Ball.Shared.GameOver;
using System;
using UnityEngine;

namespace Ball.Entities.Player
{
    public class Player : MonoBehaviour
    {
        public static Action<Player> PlayerSpawned;
        public static Action<Player> PlayerDespawned;

        [SerializeField] Health health;

        void Awake()
        {
            GameOverController.OnGameEnded += DisableSelf;
        }

        void Start()
        {
            health.Died += HandlePlayerDied;
            PlayerSpawned?.Invoke(this);
        }

        void OnDestroy()
        {
            GameOverController.OnGameEnded -= DisableSelf;
        }

        void HandlePlayerDied()
        {
            GameOverController.InvokeLoss(new PlayerDied());
        }

        void DisableSelf()
        {
            gameObject.SetActive(false);
        }
    }
}
