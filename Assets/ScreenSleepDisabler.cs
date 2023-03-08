using UnityEngine;

public class ScreenSleepDisabler : MonoBehaviour
{
    #region MonoBehaviour

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    #endregion
}
