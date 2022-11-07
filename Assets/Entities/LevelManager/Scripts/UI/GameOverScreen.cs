using Ball.Shared.GameOver;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ball.LevelManagement
{
    public class GameOverScreen : MonoBehaviour
    {
        [Header("Menu Elements")]
        [SerializeField] GameObject visualRoot;
        [SerializeField] TMP_Text lossReasonText;
        [SerializeField] Button restartButton;
        [SerializeField] Button toMenuButton;
        [Header("Settings")]
        [SerializeField] float delay;

        void Awake()
        {
            GameOverController.OnLoss += HandleLoss;

            restartButton.onClick.AddListener(Restart);
            toMenuButton.onClick.AddListener(ToMenu);
        }

        void OnDestroy()
        {
            GameOverController.OnLoss -= HandleLoss;
        }

        void Restart()
        {
            LevelManager.Restart();
        }

        void ToMenu()
        {
            LevelManager.ToMenu();
        }

        void HandleLoss(LossCondition condition)
        {
            lossReasonText.text = condition.GameOverMessage;
            this.DelayAction(() => visualRoot.SetActive(true), delay);
        }
    }
}
