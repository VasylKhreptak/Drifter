using System;
using Graphics.Gameplay;
using UnityEngine;
using Color = CBA.Extensions.Color;

namespace Gameplay
{
    public class BonusEarner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _transform;
        [SerializeField] private DriftPointFill _fill;

        [Header("Preferences")]
        [SerializeField, Range(0f, 1f)] private float _earnThreshold = 0.5f;
        [SerializeField] private LayerMask _bonusLayerMask;
        [SerializeField] private float _maxBonusDistance = 5f;

        private bool _canEarn = true;

        public event Action<GameObject> onBonusEarned;

        private GameObject _currentBonus;
        
        #region MonoBehaviour

        private void OnValidate()
        {
            _transform ??= GetComponent<Transform>();
            Transform parent = _transform.parent;
            _fill ??= parent.GetComponentInChildren<DriftPointFill>();
        }

        private void OnEnable()
        {
            if (IsBonusNear(out GameObject bonus))
            {
                _currentBonus = bonus;
                
                AddPointFillListeners();
            }
        }

        private void OnDisable()
        {
            RemovePointFillListeners();

            _canEarn = true;
            _currentBonus = null;
        }

        #endregion

        private bool IsBonusNear(out GameObject bonus)
        {
            Collider[] colliders = new Collider[1];

            if (Physics.OverlapSphereNonAlloc(_transform.position, _maxBonusDistance, colliders, _bonusLayerMask) > 0)
            {
                bonus = colliders[0].gameObject;
                return true;
            }

            bonus = null;
            return false;
        }

        private void OnDrawGizmosSelected()
        {
            if (_transform == null) return;

            Gizmos.color = Color.WithAlpha(UnityEngine.Color.green, 0.4f);
            Gizmos.DrawSphere(_transform.position, _maxBonusDistance);
        }

        private void AddPointFillListeners()
        {
            _fill.onValueChanged += OnFillChanged;
        }

        private void RemovePointFillListeners()
        {
            _fill.onValueChanged -= OnFillChanged;
        }

        private void OnFillChanged(float value)
        {
            if(value >= _earnThreshold && _canEarn)
            {
                _canEarn = false;
                onBonusEarned?.Invoke(_currentBonus);
                _currentBonus = null;
            }
        }
    }
}
