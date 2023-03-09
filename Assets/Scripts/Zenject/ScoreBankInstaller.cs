using Gameplay.Score;
using UnityEngine;

namespace Zenject
{
    public class ScoreBankInstaller : MonoInstaller
    {
        [Header("References")]
        [SerializeField] private ScoreBank _scoreBank;

        public override void InstallBindings()
        {
            Container.BindInstance(_scoreBank).AsSingle();
        }
    }
}
