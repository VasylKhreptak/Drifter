using System;
using Cinemachine;
using UnityEngine;
using Action = CBA.Actions.Core.Action;

namespace Actions
{
    public class ResetCameraPosition : Action
    {
        [Header("References")]
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;

        private Vector3 _startPosition;

        #region MonoBheaviour

        private void OnValidate()
        {
            _virtualCamera ??= FindObjectOfType<CinemachineVirtualCamera>();
        }

        private void Awake()
        {
            _startPosition = _virtualCamera.transform.position;
        }

        #endregion

        public override void Do()
        {
            _virtualCamera.ForceCameraPosition(_startPosition, _virtualCamera.transform.rotation);
        }
    }
}
