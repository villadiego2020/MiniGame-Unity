using System;
using UnityEngine;

namespace minigame.scriptables
{
    [CreateAssetMenu(menuName = "MiniGame/Descriptor", fileName = "MiniGame_Descriptor")]
    public class MiniGameDescriptor : ScriptableObject
    {
        [SerializeField] private bool m_IsEnabled = true;
        [SerializeField] private string m_ID;
        [SerializeField] private string m_DisplayName;
        [SerializeField] private GameObject m_MiniGamePrefab;

        public bool IsEnabled => m_IsEnabled;
        public string ID => m_ID;
        public string DisplayName => m_DisplayName;
        public GameObject Prefab => m_MiniGamePrefab;
    }
}