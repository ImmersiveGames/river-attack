using UnityEngine;
using MyUtils;

public class CameraShake : MonoBehaviour
{
    private CameraMaster cameraMaster;
    [SerializeField]
    private float powerShake;
    [SerializeField]
    private float timeToShake;
    [SerializeField]
    private bool vibrateHandheld;
    [SerializeField]
    private readonly long milsecondsVibrate = 500;

    private void OnEnable()
    {
        SetInitialReferences();
        Shake();
    }

    private void SetInitialReferences()
    {
        cameraMaster = Camera.main.GetComponent<CameraMaster>();
    }

    private void Shake()
    {
        if (vibrateHandheld)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ToolsAndroid.Vibrate(milsecondsVibrate);
            Handheld.Vibrate();
#endif
        }
        cameraMaster.ShakeCam(powerShake, timeToShake);
    }
}
