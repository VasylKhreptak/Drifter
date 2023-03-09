using System;
using CBA.Events.Core;
using UnityEngine;

namespace Gameplay.LevelManagement
{
    public class LevelCounter : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private MonoEvent _levelFinishEvent;

        private int _level = 1;

        public int Level => _level;

        public event Action<int> onLevelChanged;

        #region MonoBehavoiur

        private void Awake()
        {
            _levelFinishEvent.onMonoCall += IncrementLevel;
        }

        private void OnDestroy()
        {
            _levelFinishEvent.onMonoCall -= IncrementLevel;
        }

        #endregion

        public void IncrementLevel()
        {
            _level++;
            onLevelChanged?.Invoke(_level);
        }

        public void SetLevel(int level)
        {
            if (level < 0)
            {
                level = 0;
            }

            _level = level;
            onLevelChanged?.Invoke(_level);
        }
    }
}
