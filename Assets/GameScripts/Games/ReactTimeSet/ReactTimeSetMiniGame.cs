using minigame.cores;
using TMPro;
using UnityEngine;

namespace minigame.games
{
    public class ReactTimeSetMiniGame : MiniGames
    {
        [Header("Config")]
        [SerializeField] private float m_ReactTime = 5f;
        [SerializeField] private float m_TimeTabTarget = 0.2f;

        [Header("UI")]
        [SerializeField] private TextMeshProUGUI m_ReactTimeLabel;
        [SerializeField] private TextMeshProUGUI m_ReactTimeTabLabel;
        [SerializeField] private GameObject m_TabOnScreenLabel;

        private float m_Time;
        private float m_TimeTab;

        #region IMiniGame

        public override void StartGame()
        {
            m_Time = m_ReactTime;
            m_ReactTimeLabel.text = m_ReactTime.ToString("F2");

            base.StartGame();
        }
        #endregion

        #region  Game Logic
        void Update()
        {
            if (CurrentState != State.Running) return;
            UpdateReactTimeLabel();
            UpdateReactTimeTabLabel();
        }

        void UpdateReactTimeLabel()
        {
            m_Time -= Time.deltaTime;
            m_ReactTimeLabel.text = $"Taps Start in: {Mathf.Max(0, m_Time):0.0}s";
        }

        void UpdateReactTimeTabLabel()
        {
            if(m_Time <= 0f)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Finish(m_TimeTab <= m_TimeTabTarget);
                }

                m_TimeTab += Time.deltaTime;
                m_ReactTimeTabLabel.text = $"Start: {m_TimeTab:0.0}s";
                m_ReactTimeTabLabel.gameObject.SetActive(true);
                m_ReactTimeLabel.gameObject.SetActive(false);
                m_TabOnScreenLabel.SetActive(true);
            }
        }

        #endregion
    }
}