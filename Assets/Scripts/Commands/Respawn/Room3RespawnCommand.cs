using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Room3RespawnCommand : ICommand
{
    [SerializeField, Required, SceneObjectsOnly]
    private List<HiddenBridge> _hiddenGroup;

    [SerializeField, Required, SceneObjectsOnly]
    private List<HiddenBridge> _showGroup;

    public void Execute<T>(T receiver)
    {
        foreach (var bridge in _hiddenGroup)
        {
            bridge.Hide();
        }

        foreach (var bridge in _showGroup)
        {
            bridge.Show();
        }
    }
}