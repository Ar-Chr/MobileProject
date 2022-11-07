using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ball
{
    public class QuitButton : MonoBehaviour
    {
        [SerializeField] Button quitButton;

        private void Start()
        {
            quitButton.onClick.AddListener(Quit);
        }
        
        void Quit()
        {
            Application.Quit();
        }
    }
}
