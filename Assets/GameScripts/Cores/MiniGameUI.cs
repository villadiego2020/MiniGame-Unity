using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace minigame.cores
{
    public class MiniGameUI : MonoBehaviour
    {
        [SerializeField] private MiniGameManager m_Manager;
        [SerializeField] private Button m_StartBtnPrefab;
        [SerializeField] private Transform m_GameListContent;
        [SerializeField] private Transform m_GameContent;

        [SerializeField] private TextMeshProUGUI m_TitleLabel;
        [SerializeField] private TextMeshProUGUI m_GameFinishStateLabel;
        
        public Transform GameListContent => m_GameListContent;
        public Transform GameContent => m_GameContent;

        void Start()
        {
            foreach (Transform t in m_GameListContent) Destroy(t.gameObject);
            foreach (var list in m_Manager.ListMiniGames())
            {
                var game = Instantiate(m_StartBtnPrefab, m_GameListContent);
                game.gameObject.SetActive(true);
                game.GetComponentInChildren<TextMeshProUGUI>()?.SetText(list.DisplayName);
                game.onClick.AddListener(() => m_Manager.Launch(list.ID));
            }
        }

        public void SetGameTitle(string title)
        {
            m_TitleLabel.text = title;
        }

        public void FinishGame(bool success)
        {
            m_GameFinishStateLabel.text = success ? "You Win!" : "You Lose!";
            m_GameFinishStateLabel.gameObject.SetActive(true);
        }

        public void OpenGameList(bool open)
        {
            m_GameListContent.gameObject.SetActive(open);

            m_GameFinishStateLabel.text = string.Empty;
            m_GameFinishStateLabel.gameObject.SetActive(open);
        }
    }
}