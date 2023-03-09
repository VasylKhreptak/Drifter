using System;
using UnityEngine;

namespace Gameplay.Score
{
    public class ScoreBank : MonoBehaviour
    {
        private float _totalScore;

        public float Score => _totalScore;

        public event Action<float> onScoreUpdated;

        public void Add(float score)
        {
            if (score < 0)
            {
                score = 0;
            }

            _totalScore += score;
            onScoreUpdated?.Invoke(_totalScore);
        }

        public void ResetScore()
        {
            _totalScore = 0;
            onScoreUpdated?.Invoke(_totalScore);
        }
    }
}
