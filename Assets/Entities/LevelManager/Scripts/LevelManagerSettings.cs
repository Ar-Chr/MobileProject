using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Ball.LevelManagement
{
    [CreateAssetMenu(fileName = "LevelManagerSettings", menuName = "Scriptable Objects/Level Manager Settings")]
    public class LevelManagerSettings : ScriptableObject
    {
        public SceneByAsset MainMenuScene;
        public LevelSequence DesiredSequence;

        static LevelManagerSettings instance;
        public static LevelManagerSettings Instance
        {
            get
            {
                if (instance == null)
                    instance = FindInstance();

                return instance;
            }
        }

        static LevelManagerSettings FindInstance()
        {
            Debug.Log("Finding instance");
            return Resources.FindObjectsOfTypeAll<LevelManagerSettings>()[0];
        }
    }
}