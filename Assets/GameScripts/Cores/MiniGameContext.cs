using System;
using UnityEngine;

namespace minigame.cores
{
    public class MiniGameContext : IMiniGameContext
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public Transform GameContent { get; private set; }
        public Action<string> Logger { get; private set; }

        public MiniGameContext(string id, string displayName, Transform content, Action<string> logger)
        { 
            Id = id;
            DisplayName = displayName;
            GameContent = content;
            Logger = logger; 
        }

        public void Log(string msg) => Logger?.Invoke(msg);
    }
}