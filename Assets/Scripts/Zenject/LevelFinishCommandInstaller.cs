using System;
using Actions;
using UnityEngine;

namespace Zenject
{
    public class LevelFinishCommandInstaller : MonoInstaller
    {
        [Header("References")]
        [SerializeField] private LevelFinishCommand _levelFinishCommand;

        #region MonoBehaviour

        private void OnValidate()
        {
            _levelFinishCommand ??= FindObjectOfType<LevelFinishCommand>();
        }

        #endregion
        
        public override void InstallBindings()
        {
            Container.BindInstance(_levelFinishCommand).AsSingle();
        }
    }
}
