using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace minigame.cores
{
    public abstract class MiniGames : MonoBehaviour, IMiniGame
    {
        public event Action<IMiniGame, bool> OnFinished;
        public string Id { get; set; } = "default_mini_game";
        public string DisplayName { get; set; } = "Default MiniGame";
        public State CurrentState { get; set; } = State.NotStarted;
        protected IMiniGameContext m_Ctx { get; set; }

        #region IMiniGame
        public virtual void Initialize(IMiniGameContext context)
        {
            Id = context.Id;
            DisplayName = context.DisplayName;
            m_Ctx = context;
        }

        public virtual void StartGame()
        {
            CurrentState = State.Running;
        }

        public virtual void StopGame()
        {
            OnFinished = null;
            Id = "default_mini_game";
            DisplayName = "Default MiniGame";
            CurrentState = State.NotStarted;
            m_Ctx = null;
        }

        public virtual void Finish(bool success)
        {
            CurrentState = State.Finished;
            OnFinished?.Invoke(this, success);
        }
        #endregion
    }
}