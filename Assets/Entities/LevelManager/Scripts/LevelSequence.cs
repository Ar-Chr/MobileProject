using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ball.LevelManagement
{
    [CreateAssetMenu(fileName = "LevelSequence", menuName = "Scriptable Objects/Level Sequence")]
    public class LevelSequence : ScriptableObject
    {
        [SerializeField] List<SceneByAsset> sequence;

        public List<SceneByAsset> GetCopy()
        {
            return sequence.ToList();
        }
    }
}
