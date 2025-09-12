using minigame.scriptables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace minigame.cores
{
    public class MiniGameManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private MiniGameUI m_GameListUI;

        [Header("Auto-load descriptors from Resources/MiniGames")]
        [SerializeField] private bool m_AutoLoadDescriptors = true;
        [SerializeField] private List<MiniGameDescriptor> m_Descriptors = new List<MiniGameDescriptor>();

        private readonly Dictionary<string, MiniGameDescriptor> m_Map = new Dictionary<string, MiniGameDescriptor>();
        public IMiniGame Current;

        void Awake()
        {
            if (m_AutoLoadDescriptors == true)
                m_Descriptors = Resources.LoadAll<MiniGameDescriptor>("Games").Where(d => d != null && d.IsEnabled).ToList();

            m_Map.Clear();
            foreach (var descriptor in m_Descriptors)
            {
                if (descriptor != null && !m_Map.ContainsKey(descriptor.ID)) m_Map.Add(descriptor.ID, descriptor);
            }

            m_GameListUI.OpenGameList(true);
        }

        public IEnumerable<MiniGameDescriptor> ListMiniGames() => m_Descriptors;

        public void Launch(string id)
        {
            m_GameListUI.OpenGameList(false);
            StopCurrent();

            if (!m_Map.TryGetValue(id, out var desc) || desc.Prefab == null)
            {
                Debug.LogError($"MiniGame '{id}' not found.");
                return;
            }

            var go = Instantiate(desc.Prefab, m_GameListUI.GameContent);
            Current = go.GetComponent<IMiniGame>();
            if (Current == null)
            {
                Debug.LogError($"Prefab for '{id}' doesn't have IMiniGame component.");
                Destroy(go);
                return;
            }

            m_GameListUI.SetGameTitle(desc.DisplayName);
            var ctx = new MiniGameContext(desc.ID, desc.DisplayName, m_GameListUI.GameContent, (m) => Debug.Log($"[{id}] {m}"));
            Current.OnFinished += HandleFinished;
            Current.Initialize(ctx);
            Current.StartGame();
        }

        public void StopCurrent()
        {
            m_GameListUI.SetGameTitle("Game");
            if (Current == null) return;
            Current.StopGame();
            Current.OnFinished -= HandleFinished;
            if (Current is Component c) Destroy(c.gameObject);
            Current = null;
            foreach (Transform t in m_GameListUI.GameContent) Destroy(t.gameObject);
        }

        private void HandleFinished(IMiniGame g, bool success)
        {
            Debug.Log($"MiniGame '{g.DisplayName}' finished. Success={success}");

            m_GameListUI.FinishGame(success);
            StartCoroutine(WaitAndFinish());
            IEnumerator WaitAndFinish()
            {
                yield return new WaitForSeconds(5f);
                StopCurrent();
                m_GameListUI.OpenGameList(true);
            }
        }
    }
}