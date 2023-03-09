using Gameplay.Score;
using UnityEngine;
using Zenject;

namespace Graphics.Gameplay.Score
{
    public class TotalScoreRecorder : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ScoreRecorder _scoreRecorder;

        private ScoreBank _scoreBank;

        [Inject]
        private void Construct(ScoreBank scoreBank)
        {
            _scoreBank = scoreBank;
        }

        #region MonoBehaviour

        private void OnValidate()
        {
            Transform parent = transform.parent;
            _scoreRecorder ??= parent.GetComponentInChildren<ScoreRecorder>();
        }

        private void OnEnable()
        {
            _scoreRecorder.onRecorded += AddScore;
        }

        private void OnDisable()
        {
            _scoreRecorder.onRecorded -= AddScore;
        }

        #endregion

        private void AddScore(float score)
        {
            _scoreBank.Add(score);
        }
    }
}
