using System;
using System.Collections;
using Graphics.Gameplay;
using UnityEngine;

namespace Gameplay
{
    public class ScoreRecorder : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private DriftPointFillController _fillController;
        [SerializeField] private DriftPointFill _fill;

        [Header("Preferences")]
        [SerializeField] private AnimationCurve _scoreRecordCurve;
        [SerializeField] private float _minScore;
        [SerializeField] private float _maxScore;
        [SerializeField] private float _scoreAddDelay;

        private float _totalScore;

        private Coroutine _recordCoroutine;

        public event Action<float> onRecorded;
        public event Action<float> onScoreUpdated;
        public event Action onRecordingCanceled;

        #region MonoBehaviour

        private void OnValidate()
        {
            Transform parent = transform.parent;
            _fillController ??= parent.GetComponentInChildren<DriftPointFillController>();
            _fill ??= parent.GetComponentInChildren<DriftPointFill>();
        }

        private void OnEnable()
        {
            _fillController.onStartedFilling += StartRecording;
            _fillController.onStopedFilling += StopRecording;
            _fillController.onFillFailed += CancelRecording;
        }

        private void OnDisable()
        {
            CancelRecording();

            _fillController.onStartedFilling -= StartRecording;
            _fillController.onStopedFilling -= StopRecording;
            _fillController.onFillFailed -= CancelRecording;
        }

        #endregion

        private void StartRecording()
        {
            if (_recordCoroutine == null)
            {
                _recordCoroutine = StartCoroutine(RecordRoutine());
            }
        }

        private void StopRecording()
        {
            if (_recordCoroutine != null)
            {
                StopCoroutine(_recordCoroutine);
                _recordCoroutine = null;

                onRecorded?.Invoke(_totalScore);
            }
        }

        private void CancelRecording()
        {
            if (_recordCoroutine != null)
            {
                StopCoroutine(_recordCoroutine);
                _recordCoroutine = null;

                onRecordingCanceled?.Invoke();
            }

            _totalScore = 0;
        }

        private IEnumerator RecordRoutine()
        {
            while (true)
            {
                UpdateScore();

                yield return new WaitForSeconds(_scoreAddDelay);
            }
        }

        private void UpdateScore()
        {
            float score = Extensions.AnimationCurve.Evaluate(_scoreRecordCurve, _minScore, _maxScore, _fill.FillAmount);

            _totalScore += score;

            onScoreUpdated?.Invoke(_totalScore);
        }
    }
}
