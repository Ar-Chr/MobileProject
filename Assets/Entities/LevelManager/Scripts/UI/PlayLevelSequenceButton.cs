using UnityEngine;
using UnityEngine.UI;

namespace Ball.LevelManagement
{
    public class PlayLevelSequenceButton : MonoBehaviour
    {
        [SerializeField] Button button;
        [SerializeField] LevelSequence levelSequence;

        void Reset()
        {
            if (button == null)
                button = GetComponent<Button>();
        }

        void Awake()
        {
            button.onClick.AddListener(HandleButtonClicked);
        }

        void HandleButtonClicked()
        {
            LevelManager.SetDesiredSequence(levelSequence);
            LevelManager.StartSequence();
        }
    }
}
