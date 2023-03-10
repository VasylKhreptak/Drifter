using System;
using UnityEngine;

namespace Graphics.Gameplay
{
    public class DriftPointFill : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _transform;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [Header("Preferences")]
        [SerializeField] private AnimationCurve _fillCurve;
        [SerializeField, Range(0f, 1f)] private float _fillAmount;
        [SerializeField] private Gradient _gradient;

        public event Action<float> onValueChanged;

        private Vector3 _startScale;

        public float FillAmount => _fillAmount;

        #region MonoBehaviour

        private void OnValidate()
        {
            _transform ??= GetComponent<Transform>();
            _spriteRenderer ??= GetComponent<SpriteRenderer>();
        }

        private void Awake()
        {
            _startScale = _transform.localScale;
            _transform.localScale = Vector3.zero;
        }

        #endregion

        public void Set(float fillAmount)
        {
            fillAmount = Mathf.Clamp(fillAmount, 0f, 1f);
            float evaluatedFill = _fillCurve.Evaluate(fillAmount);

            UpdateScale(evaluatedFill);

            UpdateColor(evaluatedFill);

            _fillAmount = fillAmount;
            onValueChanged?.Invoke(fillAmount);
        }

        private void UpdateScale(float fillAmount)
        {
            Vector3 scale = Vector3.Lerp(Vector3.zero, _startScale, fillAmount);
            _transform.localScale = scale;
        }

        private void UpdateColor(float fillAmount)
        {
            Color color = _gradient.Evaluate(fillAmount);
            _spriteRenderer.color = color;
        }
    }
}
