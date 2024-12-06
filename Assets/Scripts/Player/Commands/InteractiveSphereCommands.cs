using Sirenix.OdinInspector;
using UnityEngine;

public class InteractiveSphereAppearCommand : ICommand
{
    [SerializeField, Required, AssetsOnly] private SOEnergyBallInteractiveSphereConfig _config;

    public void Execute<T>(T receiver)
    {
        if (!GlobalVariablesManager.Instance.GetValue(GlobalVariableNamesDefine.PlayerLeftHand,
                out GameObject playerLeftHand))
        {
            Debug.LogError("Player Left Hand Not Found");
            return;
        }

        Object.Instantiate(_config.Prefab, playerLeftHand.transform.position, Quaternion.identity);
    }
}