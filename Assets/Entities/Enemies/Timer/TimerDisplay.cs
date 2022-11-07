using Ball.Enemies;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Ball
{
    public class TimerDisplay : MonoBehaviour
    {
        [SerializeField] Timer timer;
        [SerializeField] TMP_Text text;

        private void Reset()
        {
            if (timer == null)
                timer = FindObjectOfType<Timer>();
        }

        private void Start()
        {
            if (timer == null)
                timer = FindObjectOfType<Timer>();
        }

        private void Update()
        {
            text.text = timer.TimeLeft.ToString("0.00");
        }
    }
}
