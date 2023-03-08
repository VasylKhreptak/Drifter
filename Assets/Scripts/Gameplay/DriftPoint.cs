using Extenisons;
using UnityEngine;
using Color = CBA.Extensions.Color;

namespace Gameplay
{
    public class DriftPoint : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _transform;

        [Header("Preferences")]
        [SerializeField] private float _followPointDistance = 10f;

        public float FollowPointDistance => _followPointDistance;

        private DriftDirection _driftDirection;

        public DriftDirection DriftDirection
        {
            get => _driftDirection;
            set => SetDriftDirection(value);
        }

        public Vector3 Position => _transform.position;

        #region MonoBehaviour

        private void OnValidate()
        {
            _transform ??= GetComponent<Transform>();
        }

        #endregion

        private void SetDriftDirection(DriftDirection driftDirection)
        {
            Vector3 localScale = _transform.localScale;
            localScale = new Vector3(localScale.x.WithSign(driftDirection == DriftDirection.Left ? 1 : -1), localScale.y, localScale.z);
            _transform.localScale = localScale;

            _driftDirection = driftDirection;
        }

        private void OnDrawGizmos()
        {
            if (_transform == null) return;

            Gizmos.color = Color.WithAlpha(UnityEngine.Color.red, 0.5f);
            Gizmos.DrawSphere(_transform.position, _followPointDistance);
        }
    }
}
