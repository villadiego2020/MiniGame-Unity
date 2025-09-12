using System;

namespace minigame.cores
{
    public interface IMiniGame
    {
        event Action<IMiniGame, bool> OnFinished;
        string Id { get; set; } 
        string DisplayName { get; set; }
        public State CurrentState { get; }
        void Initialize(IMiniGameContext context);
        void StartGame();
        void StopGame();
        void Finish(bool success);
    }

    public enum State
    {
        NotStarted,
        Running,
        Finished
    }
}
