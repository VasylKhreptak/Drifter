using Extenisons;
using Gameplay.LevelManagement;
using ObjectPooler;
using UnityEngine;
using Zenject;
using Action = CBA.Actions.Core.Action;

namespace Actions
{
    public class SpawnRoadSegment : Action
    {
        [Header("References")]
        [SerializeField] private Transform _transform;

        [Header("Road Preferences")]
        [SerializeField] private Pool _straightRoad;
        [SerializeField] private Pool _roadWithTurns;

        [Header("General Spawn Preferences")]
        [SerializeField] private Vector3 _spawnOffset;
        [SerializeField] private Vector3 _spawnRotation;

        [Header("Road With Turns Spawn Probability Preferences")]
        [SerializeField] private int _minLevel;
        [SerializeField] private int _maxLevel;
        [SerializeField, Range(0f, 1f)] private float _minProbability = 0.4f;
        [SerializeField, Range(0f, 1f)] private float _maxProbability = 0.9f;
        [SerializeField] private AnimationCurve _probabilityCurve;

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
            Spawn();
        }

        private void Spawn()
        {
            Pool road = GetRoadToSpawn();
            
            _objectPooler.Spawn(GetRoadToSpawn(), _transform.position + _spawnOffset, Quaternion.Euler(_spawnRotation));
        }

        private Pool GetRoadToSpawn()
        {
            int currentLevel = _levelProvider.CurrentLevel;

            if (currentLevel < _minLevel)
            {
                return _straightRoad;
            }

            if (Extensions.Mathf.Probability(GetTurnedRoadSpawnProbability()))
            {
                return _roadWithTurns;
            }

            return _straightRoad;
        }

        private float GetTurnedRoadSpawnProbability()
        {
            int currentLevel = _levelProvider.CurrentLevel;
            currentLevel = Mathf.Clamp(currentLevel, _minLevel, _maxLevel);

            float i = ((float)currentLevel).Remap(_minLevel, _maxLevel, 0f, 1f);
            return Extensions.AnimationCurve.Evaluate(_probabilityCurve, _minProbability, _maxProbability, i);
        }
    }
}
