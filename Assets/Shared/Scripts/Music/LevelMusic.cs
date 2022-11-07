using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ball.Shared
{
    public class LevelMusic : MonoBehaviour
    {
        [SerializeField] MusicSet musicSet;

        public MusicSet MusicSet => musicSet;
    }
}
