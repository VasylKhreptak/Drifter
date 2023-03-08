using UnityEngine;

namespace Zenject
{
    public class ObjectPoolerInstaller : MonoInstaller
    {
        [Header("References")]
        [SerializeField] private ObjectPooler.ObjectPooler _objectPooler;

        public override void InstallBindings()
        {
            Container.BindInstance(_objectPooler).AsSingle();
        }
    }
}
