using Ball.Shared.GameOver;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ball.LevelManagement
{
    public class LevelCompletedScreen : MonoBehaviour
    {
        [Header("Menu Elements")]
        [SerializeField] GameObject visualRoot;
        [SerializeField] Button nextLevelButton;
        [SerializeField] Button toMenuButton;
        [Header("Settings")]
        [SerializeField] float delay;

        void Awake()
        {
            GameOverController.OnVictory += HandleVictory;

            nextLevelButton.onClick.AddListener(NextLevel);
            toMenuButton.onClick.AddListener(ToMenu);
        }

        void OnDestroy()
        {
            GameOverController.OnVictory -= HandleVictory;
        }

        void NextLevel()
        {
            LevelManager.LoadNextLevel();
        }

        void ToMenu()
        {
            LevelManager.ToMenu();
        }

        void HandleVictory()
        {
            if (LevelManager.IsCurrentLevelLastInSequence)
                nextLevelButton.gameObject.SetActive(false);

            this.DelayAction(() => visualRoot.SetActive(true), delay);
        }
    }
}
