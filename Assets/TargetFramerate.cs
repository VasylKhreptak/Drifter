using UnityEngine;

public class TargetFramerate : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private int _targetFramerate = 120;

    #region MonoBehaviour

    private void Awake()
    {
        SetTargetFramerate(_targetFramerate);
    }

    #endregion

    public void SetTargetFramerate(int targetFramerate)
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFramerate;
    }
}
