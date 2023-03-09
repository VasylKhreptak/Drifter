using System;
using Gameplay.LevelManagement;
using UnityEngine;

namespace Zenject
{
    public class LevelProviderInstaller : MonoInstaller
    {
        [Header("References")]
        [SerializeField] private LevelProvider _levelProvider;

        #region MonoBehaviour

        private void OnValidate()
        {
            _levelProvider ??= FindObjectOfType<LevelProvider>();
        }

        #endregion

        public override void InstallBindings()
        {
            Container.BindInstance(_levelProvider).AsSingle();
        }
    }
}
