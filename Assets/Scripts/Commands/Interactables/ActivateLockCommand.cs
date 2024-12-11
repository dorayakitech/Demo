using Sirenix.OdinInspector;
using UnityEngine;

public class ActivateLockCommand : ICommand
{
    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Published"), LabelText("Activate Lock")]
    private SOActivateLockEvent _activateLockEvent;

    [SerializeField, Required] private int _lockNum;

    public void Execute<T>(T receiver)
    {
        Debug.Log($"Activate Lock {_lockNum}！");
        _activateLockEvent.Notify(_lockNum);
    }
}