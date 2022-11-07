using System.Collections;
using Ball.LevelManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Ball
{
    public class PauseScreen : MonoBehaviour
    {
        [SerializeField] GameObject root;
        [SerializeField] Button resumeButton;
        [SerializeField] Button toMenuButton;

        private void Start()
        {
            resumeButton.onClick.AddListener(TogglePause);
            toMenuButton.onClick.AddListener(ToMenu);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                TogglePause();
        }

        void TogglePause()
        {
            root.SetActive(!root.activeSelf);
            Time.timeScale = root.activeSelf ? 0 : 1;
        }

        void ToMenu()
        {
            Time.timeScale = 1;
            LevelManager.ToMenu();
        }
    }
}
