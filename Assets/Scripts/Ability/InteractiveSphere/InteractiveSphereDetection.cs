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

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(InteractableItemTag)) return;

        _detectedEvent.Notify(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag(InteractableItemTag)) return;

        if (!GlobalVariablesManager.Instance.HasKey(TargetInteractableItemKey))
            GlobalVariablesManager.Instance.SetValue(TargetInteractableItemKey, other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(InteractableItemTag)) return;

        if (GlobalVariablesManager.Instance.GetValue(TargetInteractableItemKey, out GameObject storedTarget) &&
            storedTarget == other.gameObject)
            GlobalVariablesManager.Instance.RemoveValue(TargetInteractableItemKey);

        _undetectedEvent.Notify(other.gameObject);
    }
}

public enum InteractiveSphereDetectionType
{
    EnergyBallSwitch,
    GravityObject
}