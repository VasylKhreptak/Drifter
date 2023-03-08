using UnityEngine;

namespace Screen
{
    public class ScreenSleepDisabler : MonoBehaviour
    {
        #region MonoBehaviour

        private void Awake()
        {
            UnityEngine.Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        #endregion
    }
}
