using Sirenix.OdinInspector;
using UnityEngine;

public abstract class CastAbilityCommand : ICommand
{
    public virtual void Execute<T>(T receiver)
    {
    }
}

public class CastEnergyBallCommand : CastAbilityCommand
{
    [SerializeField, Required, AssetsOnly] private SOEnergyBallAbilityConfig _config;

    public override void Execute<T>(T receiver)
    {
        if (!GlobalVariablesManager.Instance.GetValue(GlobalVariableNamesDefine.PlayerLeftHand,
                out GameObject playerLeftHand))
        {
            Debug.LogError("Player Left Hand Not Found");
            return;
        }

        // calculate energy ball rotation
        var forwardDir = -playerLeftHand.transform.right;
        forwardDir.y = 0.0f;
        forwardDir.Normalize();
        var targetRot = Quaternion.LookRotation(forwardDir);

        Object.Instantiate(_config.EnergyBallPrefab, playerLeftHand.transform.position,
            targetRot);
    }
}