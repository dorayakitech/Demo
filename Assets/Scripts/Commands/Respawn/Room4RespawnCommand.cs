using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Room4RespawnCommand : ICommand
{
    [SerializeField, Required, SceneObjectsOnly]
    private List<HiddenBridge> _bridges;

    [SerializeField, Required, SceneObjectsOnly]
    private EnergyLaserTurret _turret;

    public void Execute<T>(T receiver)
    {
        foreach (var bridge in _bridges)
        {
            bridge.Hide();
        }

        _turret.Hide(true);
    }
}