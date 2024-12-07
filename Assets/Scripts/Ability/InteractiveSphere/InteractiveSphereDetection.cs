using Sirenix.OdinInspector;
using UnityEngine;

public class InteractiveSphereDetection : MonoBehaviour
{
    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Published"), LabelText("Switch Detected")]
    private SOGameObjectNotifiedEvent _switchDetectedEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Published"), LabelText("Switch Undetected")]
    private SOGameObjectNotifiedEvent _switchUndetectedEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(VariableNamesDefine.EnergyBallSwitchTag)) return;

        _switchDetectedEvent.Notify(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag(VariableNamesDefine.EnergyBallSwitchTag)) return;

        if (!GlobalVariablesManager.Instance.HasKey(VariableNamesDefine.TargetEnergyBallSwitch))
            GlobalVariablesManager.Instance.SetValue(VariableNamesDefine.TargetEnergyBallSwitch, other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(VariableNamesDefine.EnergyBallSwitchTag)) return;

        if (GlobalVariablesManager.Instance.GetValue(VariableNamesDefine.TargetEnergyBallSwitch,
                out GameObject storedTarget) && storedTarget == other.gameObject)
            GlobalVariablesManager.Instance.RemoveValue(VariableNamesDefine.TargetEnergyBallSwitch);

        _switchUndetectedEvent.Notify(other.gameObject);
    }
}