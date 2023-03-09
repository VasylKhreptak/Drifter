using Gameplay.Score;
using UnityEngine;
using Action = CBA.Actions.Core.Action;

namespace Actions
{
    public class ResetTotalScore : Action
    {
        [Header("References")]
        [SerializeField] private ScoreBank _scoreBank;

        #region MonoBehaviour

        private void OnValidate()
        {
            _scoreBank ??= FindObjectOfType<ScoreBank>();
        }

        #endregion

        public override void Do()
        {
            _scoreBank.ResetScore();
        }
    }
}
