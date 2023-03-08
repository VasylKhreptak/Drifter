using UnityEngine;
using Zenject;

namespace ObjectPooler
{
    public class ZenjectedObjectPooler : ObjectPooler
    {
        private DiContainer _container;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }

        protected override GameObject InstantiateObject(GameObject prefab)
        {
            return _container.InstantiatePrefab(prefab);
        }
    }
}
