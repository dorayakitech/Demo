using Sirenix.OdinInspector;
using UnityEngine;

public class InteractiveSphereDetection : MonoBehaviour
{
    [SerializeField, Required, EnumToggleButtons]
    private InteractiveSphereDetectionType _interactableItemType;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Published"), LabelText("Detected")]
    private SOGameObjectNotifiedEvent _detectedEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Published"), LabelText("Undetected")]
    private SOGameObjectNotifiedEvent _undetectedEvent;

    [SerializeField, AssetsOnly] [BoxGroup("Events Published"), LabelText("Set Target")]
    private SOGameObjectNotifiedEvent _setTargetEvent;

    [SerializeField, AssetsOnly] [BoxGroup("Events Published"), LabelText("Unset Target")]
    private SOGameObjectNotifiedEvent _unsetTargetEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Disappear")]
    private SOEvent _disappearEvent;

    private bool _isInteractiveSphereDisappearing;

    private string InteractableItemTag
    {
        get
        {
            return _interactableItemType switch
            {
                InteractiveSphereDetectionType.EnergyBallSwitch => VariableNamesDefine.EnergyBallSwitchTag,
                InteractiveSphereDetectionType.GravityObject => VariableNamesDefine.GravityObjectTag,
                _ => ""
            };
        }
    }

    private string TargetInteractableItemKey
    {
        get
        {
            return _interactableItemType switch
            {
                InteractiveSphereDetectionType.EnergyBallSwitch => VariableNamesDefine.TargetEnergyBallSwitch,
                InteractiveSphereDetectionType.GravityObject => VariableNamesDefine.TargetGravityObject,
                _ => ""
            };
        }
    }

    private void OnEnable()
    {
        _disappearEvent.Subscribe(OnInteractiveSphereDisappear);
    }

    private void OnDisable()
    {
        _disappearEvent.Unsubscribe(OnInteractiveSphereDisappear);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(InteractableItemTag)) return;

        Debug.Log("Interactive Sphere OnTriggerEnter: " + other.gameObject.name);
        _detectedEvent.Notify(other.gameObject);
        SetTarget(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag(InteractableItemTag)) return;

        SetTarget(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(InteractableItemTag)) return;

        Debug.Log("Interactive Sphere OnTriggerExit: " + other.gameObject.name);
        _undetectedEvent.Notify(other.gameObject);
        UnsetTarget(other.gameObject);
    }

    private void SetTarget(GameObject target)
    {
        if (GlobalVariablesManager.Instance.HasKey(TargetInteractableItemKey)) return;
        if (_isInteractiveSphereDisappearing) return;

        Debug.Log("Interactive Sphere Set Target: " + target.gameObject.name);
        GlobalVariablesManager.Instance.SetValue(TargetInteractableItemKey, target);
        _setTargetEvent?.Notify(target);
    }

    private void UnsetTarget(GameObject target)
    {
        if (!GlobalVariablesManager.Instance.GetValue(TargetInteractableItemKey, out GameObject storedTarget) ||
            storedTarget != target) return;

        Debug.Log("Interactive Sphere Unset Target: " + target.gameObject.name);
        GlobalVariablesManager.Instance.RemoveValue(TargetInteractableItemKey);
        _unsetTargetEvent?.Notify(target);
    }

    private void OnInteractiveSphereDisappear()
    {
        _isInteractiveSphereDisappearing = true;
    }
}

public enum InteractiveSphereDetectionType
{
    EnergyBallSwitch,
    GravityObject
}