using UnityEngine;

namespace Gameplay
{
    public class CarMovement : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _transform;
        [SerializeField] private DriftPointSpawner _driftPointSpawner;

        [Header("Preferences")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;

        private DriftPoint _currentDriftPoint;

        private Vector3 _targetPoint;

        #region MonoBehaviour

        private void OnValidate()
        {
            _transform ??= GetComponent<Transform>();
            _driftPointSpawner ??= FindObjectOfType<DriftPointSpawner>();
        }

        private void OnEnable()
        {
            _driftPointSpawner.onSpawned += SetCurrentDriftPoint;
        }

        private void Update()
        {
            if (_currentDriftPoint == null) return;

            _targetPoint = GetTargetPoint();

            Move(in _targetPoint);

            Rotate(in _targetPoint);
        }

        private void OnDisable()
        {
            _driftPointSpawner.onSpawned -= SetCurrentDriftPoint;
        }

        #endregion

        private void SetCurrentDriftPoint(DriftPoint driftPoint)
        {
            _currentDriftPoint = driftPoint;
        }

        private void Move(in Vector3 targetPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, _moveSpeed * Time.deltaTime);
        }

        private void Rotate(in Vector3 targetPoint)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        }

        private Vector3 GetTargetPoint()
        {
            Vector3 direction = (_transform.position - _currentDriftPoint.Position).normalized;
            Vector3 left = Vector3.Cross(direction, Vector3.up).normalized;
            left *= _currentDriftPoint.DriftDirection == DriftDirection.Left ? 1 : -1;
            Vector3 targetPoint = _currentDriftPoint.Position + left * _currentDriftPoint.Radius;
            return targetPoint;
        }

        private void OnDrawGizmos()
        {
            if (_transform == null || _currentDriftPoint == null) return;

            Vector3 point = GetTargetPoint();
            Gizmos.color = UnityEngine.Color.green;
            Gizmos.DrawSphere(point, 0.5f);
        }
    }
}
