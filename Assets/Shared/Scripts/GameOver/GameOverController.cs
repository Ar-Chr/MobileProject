using System;
using UnityEngine;

namespace Ball.Shared.GameOver
{
    public static class GameOverController
    {
        public static event Action OnVictory;
        public static event Action<LossCondition> OnLoss;
        public static event Action OnGameStarted;
        public static event Action OnGameEnded;

        public static bool GameEnded { get; private set; }

        public static void StartNewGame()
        {
            GameEnded = false;
            OnGameStarted?.Invoke();
        }

        public static void InvokeVictory()
        {
            if (GameEnded)
                return;

            GameEnded = true;
            OnVictory?.Invoke();
            OnGameEnded?.Invoke();
        }

        public static void InvokeLoss(LossCondition lossCondition)
        {
            if (GameEnded)
                return;

            GameEnded = true;
            OnLoss?.Invoke(lossCondition);
            OnGameEnded?.Invoke();
        }
    }

    public abstract class LossCondition
    {
        public abstract string GameOverMessage { get; }
    }

    public class OutOfTime : LossCondition
    {
        string gameoverMessage = "Время вышло";
        public override string GameOverMessage => gameoverMessage;
    }

    public class PlayerDied : LossCondition
    {
        string gameoverMessage = "Вы погибли";
        public override string GameOverMessage => gameoverMessage;
    }
}