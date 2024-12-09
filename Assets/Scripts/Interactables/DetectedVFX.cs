using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(FlashVFX))]
public class DetectedVFX : MonoBehaviour
{
    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Detected")]
    private SOGameObjectNotifiedEvent _detectedEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Undetected")]
    private SOGameObjectNotifiedEvent _undetectedEvent;

    private FlashVFX _flashVFX;

    private void Awake()
    {
        _flashVFX = GetComponent<FlashVFX>();
    }

    private void OnEnable()
    {
        _detectedEvent.Subscribe(OnDetected);
        _undetectedEvent.Subscribe(OnUndetected);
    }

    private void OnDisable()
    {
        _detectedEvent.Unsubscribe(OnDetected);
        _undetectedEvent.Unsubscribe(OnUndetected);
    }

    private void OnDetected(GameObject obj)
    {
        if (gameObject != obj) return;
        _flashVFX.StartFlash();
    }

    private void OnUndetected(GameObject obj)
    {
        if (gameObject != obj) return;
        _flashVFX.StopFlash();
    }
}