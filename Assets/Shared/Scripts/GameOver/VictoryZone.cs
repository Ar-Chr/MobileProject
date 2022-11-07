using Ball.Entities.Player;
using UnityEngine;

namespace Ball.Shared.GameOver
{
    public class VictoryZone : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
                GameOverController.InvokeVictory();
        }
    }
}