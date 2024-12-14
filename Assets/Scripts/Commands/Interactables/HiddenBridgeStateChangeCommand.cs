using Sirenix.OdinInspector;
using UnityEngine;

public class HiddenBridgeStateChangeCommand : ICommand
{
    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Published"), LabelText("State Change")]
    private SOLockStateChangeEvent _stateChangeEvent;

    public void Execute<T>(T receiver)
    {
        if (receiver is not HiddenBridge hiddenBridge) return;

        _stateChangeEvent.Notify(hiddenBridge.LockNum, hiddenBridge.CurrentShowState);
    }
}