using UnityEngine;

namespace minigame.cores
{
    public interface IMiniGameContext
    {
        string Id { get; set; }
        string DisplayName { get; set; }
        Transform GameContent { get; }
        void Log(string msg);
    }
}