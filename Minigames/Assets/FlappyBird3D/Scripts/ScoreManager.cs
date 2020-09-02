using System;
using TMPro;
using UnityEngine;

namespace FlappyBird3D
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("There can only be one Instance of ScoreManager");
                Application.Quit();
            }
            instance = this;
        }

        [SerializeField] private TextMeshProUGUI scoreText;
        private int _score = 0;

        private void Start()
        {
            scoreText.text = "Score: " + _score;
        }

        public void AddScore(int score)
        {
            _score += score;
            scoreText.text = "Score: " + _score;
        }

        public void ResetScore()
        {
            _score = 0;
            scoreText.text = "Score: " + _score;
        }
    }
}
