using Gameplay.LevelManagement;
using TMPro;
using UnityEngine;

namespace Graphics.UI
{
    public class LevelText : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text _tmp;
        [SerializeField] private LevelCounter _levelCounter;

        #region MonoBehaviour

        private void OnValidate()
        {
            _tmp ??= GetComponent<TMP_Text>();
            _levelCounter ??= FindObjectOfType<LevelCounter>();
        }

        private void Awake()
        {
            UpdateText(_levelCounter.Level);
            
            _levelCounter.onLevelChanged += UpdateText;
        }

        private void OnDestroy()
        {
            _levelCounter.onLevelChanged -= UpdateText;
        }

        #endregion

        private void UpdateText(int level)
        {
            _tmp.text = level.ToString();
        }
    }
}
