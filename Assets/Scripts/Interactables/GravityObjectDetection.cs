using Sirenix.OdinInspector;
using UnityEngine;

public class GravityObjectDetection : MonoBehaviour
{
    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Switch Detected")]
    private SOGameObjectNotifiedEvent _detectedEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Switch Undetected")]
    private SOGameObjectNotifiedEvent _undetectedEvent;

    // TODO
    private Color _defaultColor;

    private void Awake()
    {
        _defaultColor = GetComponent<MeshRenderer>().materials[0].color;
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
        GetComponent<MeshRenderer>().materials[0].color = Color.yellow;
    }

    private void OnUndetected(GameObject obj)
    {
        if (gameObject != obj) return;
        GetComponent<MeshRenderer>().materials[0].color = _defaultColor;
    }
}