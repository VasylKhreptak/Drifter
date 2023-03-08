using Cinemachine;
using UnityEngine;

namespace Graphics.Camera
{
    [ExecuteInEditMode] [SaveDuringPlay]
    public class LockCameraX : CinemachineExtension
    {
        [Header("Preferences")]
        [SerializeField] private float _targetX;

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage != CinemachineCore.Stage.Body) return;
            
            Vector3 position = state.RawPosition;
            position.x = _targetX;
            state.RawPosition = position;
        }
    }
}
