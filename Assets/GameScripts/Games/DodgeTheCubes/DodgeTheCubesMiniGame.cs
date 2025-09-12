using minigame.cores;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace minigame.games
{
    public class DodgeTheCubesMiniGame : MiniGames
    {
        [Header("Config")]
        [SerializeField] private float m_Duration = 15f;
        [SerializeField] private float m_SpawnEvery = 0.5f;
        [SerializeField] private float m_FallSpeed = 6f;
        [SerializeField] private int m_MaxHp = 10;

        [Header("UI")]
        [SerializeField] private TextMeshProUGUI m_DurationLabel;
        [SerializeField] private Image m_HpImg;

        private float m_DurationTimer;
        private float m_SpawnTime;
        private float m_HpValue;
        private Transform m_Player;

        #region IMiniGame
        public override void StartGame()
        {
            m_DurationTimer = m_Duration;
            m_SpawnTime = 0f;
            m_HpValue = m_MaxHp;

            m_Player = GameObject.CreatePrimitive(PrimitiveType.Capsule).transform;
            m_Player.SetParent(m_Ctx.GameContent, false);
            m_Player.position = new Vector3(0, -4f, 0);

            m_Player.gameObject.AddComponent<Player>().OnHit += () => 
            {
                m_HpImg.fillAmount = m_HpValue / m_MaxHp;
                m_HpValue -= 1;
                if(m_HpValue <= 0) Finish(false);
            };

            base.StartGame();
        }
        #endregion

        #region Game Logic
        void Update()
        {
            if(CurrentState != State.Running) return;

            float moveX = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(moveX, 0f, 0f);
            m_Player.Translate(movement * 6f * Time.deltaTime, Space.World);

            m_SpawnTime -= Time.deltaTime;
            if (m_SpawnTime <= 0f)
            {
                m_SpawnTime = UnityEngine.Random.Range(0.1f, m_SpawnEvery);
                var c = GameObject.CreatePrimitive(PrimitiveType.Cube);
                c.layer = LayerMask.NameToLayer("Enemy");
                c.transform.SetParent(m_Ctx.GameContent, false);
                c.transform.position = new Vector3(UnityEngine.Random.Range(-8f, 8f), 8f, 0f);
                c.AddComponent<BoxCollider>().isTrigger = true;
                c.AddComponent<Rigidbody>().useGravity = false;
                c.AddComponent<FallingCube>().speed = m_FallSpeed;
                c.AddComponent<FallingCube>().Agressive();
            }

            m_DurationTimer -= Time.deltaTime;
            m_DurationLabel.text = $"Time Left: {Mathf.Max(0, m_DurationTimer):0.0}s";
            if (m_DurationTimer <= 0f) Finish(true);
        }

        private class FallingCube : MonoBehaviour
        {
            public float speed = 6f;

            public void Agressive()
            {
                float value = UnityEngine.Random.value;
                if(value > 0.33f * 2)
                {
                    speed *= 2;
                }
            }

            void Update()
            {
                transform.position += Vector3.down * speed * Time.deltaTime;
                if (transform.position.y < -8f) Destroy(gameObject);
            }
        }

        private class Player : MonoBehaviour
        {
            public Action OnHit;

            private void OnTriggerEnter(Collider other)
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    OnHit?.Invoke();
                    Destroy(other.gameObject);
                }
            }
        }
        #endregion
    }
}