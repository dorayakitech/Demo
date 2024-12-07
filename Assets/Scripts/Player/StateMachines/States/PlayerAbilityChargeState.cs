﻿public class PlayerAbilityChargeState : PlayerBaseState
{
    private SOAbilityConfig _config;

    private void OnEnable()
    {
        _config = GameManager.Instance.ActiveAbilityConfig;

        foreach (var task in _config.ChargeStepOnEnableTasks)
        {
            task.Execute(this);
        }
    }

    private void OnDisable()
    {
        foreach (var task in _config.ChargeStepOnDisableTasks)
        {
            task.Execute(this);
        }
    }
}