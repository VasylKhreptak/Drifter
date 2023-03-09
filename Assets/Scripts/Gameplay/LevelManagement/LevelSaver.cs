using System;
using UnityEngine;

namespace Gameplay.LevelManagement
{
    public class LevelSaver : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private LevelCounter _levelCounter;

        private const string PLAYER_PREFS_KEY = "Level";

        #region MonoBehaviour

        private void OnValidate()
        {
            _levelCounter ??= FindObjectOfType<LevelCounter>();
        }

        private void Awake()
        {
            LoadLevel();
        }

        private void OnDestroy()
        {
            SaveLevel();
        }

        private void OnApplicationQuit()
        {
            SaveLevel();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            SaveLevel();
        }

        #endregion

        private void LoadLevel()
        {
            int level = PlayerPrefs.GetInt(PLAYER_PREFS_KEY, 1);
            _levelCounter.SetLevel(level);
        }

        private void SaveLevel()
        {
            PlayerPrefs.SetInt(PLAYER_PREFS_KEY, _levelCounter.Level);
        }
    }
}
