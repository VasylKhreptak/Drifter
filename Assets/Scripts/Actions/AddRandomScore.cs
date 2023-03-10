using CBA.Actions.Core;
using Gameplay.Score;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Actions
{
    public class AddRandomScore : Action
    {
        [Header("Preferences")]
        [SerializeField, MinMaxSlider(60f, 150f)] private Vector2Int _scoreRange;

        private int Score => Random.Range(_scoreRange.x, _scoreRange.y);

        private ScoreBank _scoreBank;

        [Inject]
        private void Construct(ScoreBank scoreBank)
        {
            _scoreBank = scoreBank;
        }

        public override void Do()
        {
            _scoreBank.Add(Score);
        }
    }
}
