using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{
    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Change Active Ability")]
    private SOEvent _changeActiveAbilityEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Abilities"), LabelText("Available")]
    private List<SOAbilityConfig> _availableAbilities;

    [SerializeField, ReadOnly] [BoxGroup("Abilities"), LabelText("Active Index")]
    private int _activeAbilityIndex;

    public SOAbilityConfig ActiveAbilityConfig => _availableAbilities[_activeAbilityIndex];

    private bool _isNPCActive;
    public bool IsNPCActive => _isNPCActive;

    private void OnEnable()
    {
        _changeActiveAbilityEvent.Subscribe(OnChangeActiveAbility);
    }

    private void OnDisable()
    {
        _changeActiveAbilityEvent.Unsubscribe(OnChangeActiveAbility);
    }

    private void OnChangeActiveAbility()
    {
        _activeAbilityIndex += 1;
        if (_activeAbilityIndex >= _availableAbilities.Count)
            _activeAbilityIndex = 0;
    }

    public void EnableNPC()
    {
        _isNPCActive = true;
    }
}