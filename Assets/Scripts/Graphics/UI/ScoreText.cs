using Gameplay.Score;
using TMPro;
using UnityEngine;

namespace Graphics.UI
{
    public class ScoreText : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text _tmp;
        [SerializeField] private ScoreBank _scoreBank;

        #region MonoBehaviour

        private void OnValidate()
        {
            _scoreBank ??= FindObjectOfType<ScoreBank>();
        }

        private void Awake()
        {
            _scoreBank.onScoreUpdated += UpdateText;
        }

        private void OnDestroy()
        {
            _scoreBank.onScoreUpdated -= UpdateText;
        }

        #endregion

        private void UpdateText(float score)
        {
            _tmp.text = ((int)score).ToString();
        }
    }
}
