using System;
using System.Collections;
using CBA.Events.Core;
using UnityEngine;

namespace Graphics.Gameplay
{
    public class DriftPointFillController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private DriftPointFill _fill;

        [Header("Preferences")]
        [SerializeField] private float _fillSpeed;

        [Header("Events")]
        [SerializeField] private MonoEvent _triggerEnterEvent;
        [SerializeField] private MonoEvent _triggerExitEvent;

        private bool _canFill = true;

        private Coroutine _fillCoroutine;

        public event Action onStartedFilling;
        public event Action onStopedFilling;
        public event Action onFillFailed;

        #region MonoBehaviour

        private void OnValidate()
        {
            _fill ??= GetComponentInChildren<DriftPointFill>();
        }

        private void OnEnable()
        {
            _triggerEnterEvent.onMonoCall += StartFilling;
            _triggerExitEvent.onMonoCall += StopFilling;
        }

        private void OnDisable()
        {
            StopFilling();
            _fill.Set(0f);
            _canFill = true;

            _triggerEnterEvent.onMonoCall -= StartFilling;
            _triggerExitEvent.onMonoCall -= StopFilling;
        }

        #endregion

        private void StartFilling()
        {
            if (_fillCoroutine == null && _fill.FillAmount < 1f && _canFill)
            {
                onStartedFilling?.Invoke();

                _fillCoroutine = StartCoroutine(FillRoutine());
            }
        }

        private void StopFilling()
        {
            if (_fillCoroutine != null)
            {
                StopCoroutine(_fillCoroutine);
                _fillCoroutine = null;

                _canFill = false;

                onStopedFilling?.Invoke();
            }
        }

        private IEnumerator FillRoutine()
        {
            while (true)
            {
                IncreaseFill();

                yield return null;
            }
        }

        private void IncreaseFill()
        {
            float fillAmount = _fill.FillAmount + _fillSpeed * Time.deltaTime;

            if (_fill.FillAmount >= 1f)
            {
                onFillFailed?.Invoke();
                StopFilling();
            }
            else
            {
                _fill.Set(fillAmount);
            }
        }
    }
}
