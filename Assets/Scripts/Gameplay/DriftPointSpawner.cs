using System;
using ObjectPooler;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Gameplay
{
    public class DriftPointSpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private OnPointerDownEvent _pointerDownEvent;

        [Header("Preferences")]
        [SerializeField] private Pool _pool;
        [SerializeField] private Vector3 _spawnOffset;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private DriftDirection _startDirection = DriftDirection.Left;

        private DriftDirection _previousDriftDirection;

        private ObjectPooler.ObjectPooler _objectPooler;

        private Camera _camera;

        [Inject]
        private void Construct(ObjectPooler.ObjectPooler objectPooler)
        {
            _objectPooler = objectPooler;
        }

        public event Action<DriftPoint> onSpawned;

        #region MonoBehaviour

        private void OnValidate()
        {
            _pointerDownEvent ??= FindObjectOfType<OnPointerDownEvent>();
        }

        private void Awake()
        {
            _camera = Camera.main;

            _previousDriftDirection = _startDirection == DriftDirection.Left ? DriftDirection.Right : DriftDirection.Left;
        }

        private void OnEnable()
        {
            _pointerDownEvent.onPointerDown += SpawnDriftPoint;
        }

        private void OnDisable()
        {
            _pointerDownEvent.onPointerDown -= SpawnDriftPoint;
        }

        #endregion

        private void SpawnDriftPoint(PointerEventData eventData)
        {
            Ray ray = _camera.ScreenPointToRay(eventData.position);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _layerMask))
            {
                SpawnDriftPoint(hitInfo.point + _spawnOffset, Quaternion.LookRotation(hitInfo.normal));
            }
        }

        private void SpawnDriftPoint(Vector3 position, Quaternion rotation)
        {
            GameObject driftPointObject = _objectPooler.Spawn(_pool, position, rotation);

            if (!driftPointObject.TryGetComponent(out DriftPoint driftPoint)) return;

            driftPoint.DriftDirection = _previousDriftDirection == DriftDirection.Left ? DriftDirection.Right : DriftDirection.Left;
            _previousDriftDirection = driftPoint.DriftDirection;

            onSpawned?.Invoke(driftPoint);
        }
    }
}
