using UnityEngine;

namespace Gameplay.LevelManagement
{
    public class LevelProvider : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private LevelCounter _levelCounter;

        public int CurrentLevel => _levelCounter.Level;

        #region MonoBehaviour

        private void OnValidate()
        {
            _levelCounter ??= FindObjectOfType<LevelCounter>();
        }

        #endregion
    }
}
