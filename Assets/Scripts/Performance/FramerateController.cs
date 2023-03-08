using UnityEngine;

namespace Performance
{
    public class FramerateController : MonoBehaviour
    {
        #region MonoBehaviour

        private void Awake()
        {
            SetTargetFramerate(UnityEngine.Screen.currentResolution.refreshRate);
        }

        #endregion

        private void SetTargetFramerate(int targetFramerate)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = targetFramerate;
        }
    }
}
