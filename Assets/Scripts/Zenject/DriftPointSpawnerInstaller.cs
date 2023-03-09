using Gameplay;
using UnityEngine;

namespace Zenject
{
    public class DriftPointSpawnerInstaller : MonoInstaller
    {
        [Header("References")]
        [SerializeField] private DriftPointSpawner _driftPointSpawner;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_driftPointSpawner).AsSingle();
        }
    }
}
