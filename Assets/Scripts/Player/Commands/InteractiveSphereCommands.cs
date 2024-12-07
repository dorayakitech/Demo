using Sirenix.OdinInspector;
using UnityEngine;

public class InteractiveSphereAppearCommand : ICommand
{
    [SerializeField, Required, AssetsOnly] private SOEnergyBallAbilityConfig _config;

    public void Execute<T>(T receiver)
    {
        if (!GlobalVariablesManager.Instance.GetValue(VariableNamesDefine.PlayerLeftHand,
                out GameObject playerLeftHand))
        {
            Debug.LogError("Player Left Hand Not Found");
            return;
        }

        Object.Instantiate(_config.InteractiveSpherePrefab, playerLeftHand.transform.position, Quaternion.identity);
    }
}