using minigame.cores;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace minigame.games
{
    public class TapTheButtonMiniGame : MiniGames
    {
        [Header("Config")]
        [SerializeField] private int m_TargetTaps = 10;
        [SerializeField] private float m_LimeLimit = 5f;

        [Header("UI")]
        [SerializeField] private Button m_TabBtn;
        [SerializeField] private TextMeshProUGUI m_TabLabel;

        private int m_Taps;
        private float m_Time;

        #region IMiniGame

        public override void StartGame()
        {
            m_Taps = 0;
            m_Time = m_LimeLimit;
            m_TabBtn.onClick.AddListener(() =>
            {
                if (CurrentState != State.Running) return;
                m_Taps++;
                UpdateLabel();
            });

            UpdateLabel();

            base.StartGame();
        }
        #endregion

        #region  Game Logic
        void Update()
        {
            if(CurrentState != State.Running) return;
            if (m_Time <= 0f) return;
            m_Time -= Time.deltaTime;
            if (m_Time <= 0f) Finish(m_Taps >= m_TargetTaps);
            else UpdateLabel();
        }

        void UpdateLabel()
        {
            m_TabLabel.text = $"Taps: {m_Taps}/{m_TargetTaps}\nTime: {Mathf.Max(0, m_Time):0.0}s";
        }
        #endregion
    }
}