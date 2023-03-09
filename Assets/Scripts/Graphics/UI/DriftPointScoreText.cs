using Gameplay;
using Gameplay.Score;
using TMPro;
using UnityEngine;

namespace Graphics.UI
{
    public class DriftPointScoreText : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text _tmp;
        [SerializeField] private ScoreRecorder _scoreRecorder;

        #region MonoBehaviour

        private void OnValidate()
        {
            _tmp ??= GetComponent<TMP_Text>();
            Transform parent = transform.parent;
            _scoreRecorder ??= parent.GetComponentInChildren<ScoreRecorder>();
        }

        private void OnEnable()
        {
            _scoreRecorder.onScoreUpdated += UpdateText;
            _scoreRecorder.onRecordingCanceled += ResetText;
        }

        private void OnDisable()
        {
            ResetText();

            _scoreRecorder.onScoreUpdated -= UpdateText;
            _scoreRecorder.onRecordingCanceled -= ResetText;
        }

        #endregion

        private void UpdateText(float score)
        {
            _tmp.text = ((int)score).ToString();
        }

        private void ResetText()
        {
            UpdateText(0);
        }
    }
}
