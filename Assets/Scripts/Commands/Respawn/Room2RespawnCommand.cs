using Sirenix.OdinInspector;
using UnityEngine;

public class Room2RespawnCommand : ICommand
{
    [SerializeField, Required, SceneObjectsOnly]
    private HiddenBridge _hiddenBridge;

    public void Execute<T>(T receiver)
    {
        _hiddenBridge.Hide();
    }
}