using Sirenix.OdinInspector;
using UnityEngine;

public class EnergyBallSwitchDetection : MonoBehaviour
{
    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Switch Detected")]
    private SOGameObjectNotifiedEvent _switchDetectedEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Switch Undetected")]
    private SOGameObjectNotifiedEvent _switchUndetectedEvent;

    // TODO
    private Color _defaultColor;

    private void Awake()
    {
        _defaultColor = GetComponent<MeshRenderer>().materials[0].color;
    }

    private void OnEnable()
    {
        _switchDetectedEvent.Subscribe(OnDetected);
        _switchUndetectedEvent.Subscribe(OnUndetected);
    }

    private void OnDisable()
    {
        _switchDetectedEvent.Unsubscribe(OnDetected);
        _switchUndetectedEvent.Unsubscribe(OnUndetected);
    }

    private void OnDetected(GameObject obj)
    {
        if (gameObject != obj) return;
        GetComponent<MeshRenderer>().materials[0].color = Color.red;
    }

    private void OnUndetected(GameObject obj)
    {
        if (gameObject != obj) return;
        GetComponent<MeshRenderer>().materials[0].color = _defaultColor;
    }
}