using Ball.Shared.GameOver;
using Ball.Shared.Utility;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ball.LevelManagement
{
    public static class LevelManager
    {
        static Scene activeScene => SceneManager.GetActiveScene();

        static LevelSequence desiredSequence;
        static List<SceneByAsset> loadedSequence;

        static int currentLevelNumber;
        static bool init;

        public static bool IsCurrentLevelLastInSequence => currentLevelNumber == loadedSequence.Count - 1;

        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            SceneManager.sceneLoaded += (scene, mode) => GameOverController.StartNewGame();
        }

        #region Sequence Controls

        public static void SetDesiredSequence(LevelSequence sequence)
        {
            desiredSequence = sequence;
        }

        public static void StartSequence()
        {
            LoadSequence();
            LoadFirstLevel();
        }

        static void LoadSequence()
        {
            loadedSequence = desiredSequence.GetCopy();
        }

        #endregion

        #region Level Changers

        public static void LoadFirstLevel()
        {
            currentLevelNumber = -1;
            LoadNextLevel();
        }

        public static void LoadNextLevel()
        {
            currentLevelNumber = (currentLevelNumber + 1) % loadedSequence.Count;
            LoadScene(loadedSequence[currentLevelNumber]);
        }

        public static void Restart()
        {
            LoadScene(activeScene.buildIndex);
        }

        public static void ToMenu()
        {
            LoadScene("MainMenu");
        }

        #region LoadScene

        static void LoadScene(SceneByAsset scene)
        {
            LoadScene(scene.SceneName);
        }

        static void LoadScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }

        static void LoadScene(int sceneIndex)
        {
            SceneManager.LoadSceneAsync(sceneIndex);
        }

        #endregion

        #endregion
    }
}