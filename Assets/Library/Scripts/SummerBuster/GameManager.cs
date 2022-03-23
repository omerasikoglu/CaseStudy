using UnityEngine;
using System;

namespace SummerBuster
{
    public enum GameState
    {
        None = 0,
        Start =1,
        Win = 2,
    }
    public class GameManager : Singleton<GameManager>
    {
        public static event Action<GameState> OnStateChanged;
        public GameState State { get; private set; }

        private void Start()
        {
            ChangeState(GameState.Start);
        }

        public void ChangeState(GameState newState)
        {
            if (State == newState) return;

            State = newState;

            switch (newState)
            {
                case GameState.None:
                case GameState.Start: HandleStart(); break;
                case GameState.Win: HandleWin(); break;
                default: break;
            }

            OnStateChanged?.Invoke(newState);
        }

        private void HandleStart()
        {
            UIManager.Instance.CloseWinGameUI();
        }

        private void HandleWin()
        {
            UIManager.Instance.OpenWinGameUI();
        }
    }
}
