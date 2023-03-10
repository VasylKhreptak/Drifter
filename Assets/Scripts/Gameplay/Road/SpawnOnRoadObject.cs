using Gameplay.LevelManagement;
using ObjectPooler;
using UnityEngine;
using Zenject;
using Action = CBA.Actions.Core.Action;
using Color = CBA.Extensions.Color;
using Random = UnityEngine.Random;

namespace Gameplay.Road
{
    public class SpawnOnRoadObject : Action
    {
        [Header("References")]
        [SerializeField] protected Transform _transform;

        [Header("Preferences")]
        [SerializeField] private Pool _pool;
        [SerializeField] private int _minLevel;
        [SerializeField] private int _maxLevel;
        [SerializeField] private float _minProbability = 0.4f;
        [SerializeField] private float _maxProbability = 0.9f;
        [SerializeField] private AnimationCurve _probabilityCurve;
        [SerializeField] private bool _randomizeYRotation = true;
        [SerializeField] private float _range;

        private ObjectPooler.ObjectPooler _objectPooler;
        private LevelProvider _levelProvider;

        [Inject]
        private void Construct(ObjectPooler.ObjectPooler objectPooler, LevelProvider levelProvider)
        {
            _objectPooler = objectPooler;
            _levelProvider = levelProvider;
        }
        #region MonoBehaviour

        private void OnValidate()
        {
            _transform ??= GetComponent<Transform>();
        }

        #endregion

        public override void Do()
        {
            TrySpawn();
        }

        private void TrySpawn()
        {
            if (_levelProvider.CurrentLevel >= _minLevel && Extensions.Mathf.Probability(GetProbability()))
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            _objectPooler.Spawn(_pool, GetPosition(), GetRotation());
        }

        private float GetProbability()
        {
            return Extensions.AnimationCurve.Evaluate(_probabilityCurve, _minLevel, _maxLevel,
                _levelProvider.CurrentLevel, _minProbability, _maxProbability);
        }

        protected virtual Vector3 GetPosition()
        {
            Vector2 insideUnitCircle = Random.insideUnitCircle;
            Vector3 direction = new Vector3(insideUnitCircle.x, 0f, insideUnitCircle.y);
            return _transform.position + direction * _range;
        }

        protected virtual Quaternion GetRotation()
        {
            if (_randomizeYRotation)
            {
                return Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            }

            return Quaternion.identity;
        }

        private void OnDrawGizmos()
        {
            if (_transform == null) return;

            Gizmos.color = Color.WithAlpha(UnityEngine.Color.red, 0.4f);
            Gizmos.DrawSphere(_transform.position, _range);
        }
    }
}
