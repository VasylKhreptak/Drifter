using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class CarMovement : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _transform;

        [Header("Preferences")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;

        private DriftPointSpawner _driftPointSpawner;

        private DriftPoint _currentDriftPoint;

        private Vector3 _followPoint;

        [Inject]
        private void Construct(DriftPointSpawner driftPointSpawner)
        {
            _driftPointSpawner = driftPointSpawner;
        }

        #region MonoBehaviour

        private void OnValidate()
        {
            _transform ??= GetComponent<Transform>();
        }

        private void OnEnable()
        {
            _driftPointSpawner.onSpawned += SetCurrentDriftPoint;
        }

        private void Update()
        {
            MoveForward();

            if (_currentDriftPoint == null) return;

            _followPoint = GetFollowPoint();

            Rotate(in _followPoint);
        }

        private void OnDisable()
        {
            _currentDriftPoint = null;

            _driftPointSpawner.onSpawned -= SetCurrentDriftPoint;
        }

        #endregion

        private void SetCurrentDriftPoint(DriftPoint driftPoint)
        {
            _currentDriftPoint = driftPoint;
        }

        private void MoveForward()
        {
            _transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
        }

        private void Rotate(in Vector3 targetPoint)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        }

        private Vector3 GetFollowPoint()
        {
            Vector3 direction = (_transform.position - _currentDriftPoint.Position).normalized;
            Vector3 left = Vector3.Cross(direction, Vector3.up).normalized;
            left *= _currentDriftPoint.DriftDirection == DriftDirection.Left ? 1 : -1;
            Vector3 targetPoint = _currentDriftPoint.Position + left * _currentDriftPoint.FollowPointDistance;
            return targetPoint;
        }

        private void OnDrawGizmos()
        {
            if (_transform == null || _currentDriftPoint == null) return;

            Vector3 point = GetFollowPoint();
            Gizmos.color = UnityEngine.Color.green;
            Gizmos.DrawSphere(point, 0.5f);
        }
    }
}
