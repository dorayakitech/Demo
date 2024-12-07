using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField, Required, AssetsOnly] [BoxGroup("Events Subscribed"), LabelText("Switch Ability")]
    private SOEvent _switchAbilityEvent;

    [SerializeField, Required, AssetsOnly] [BoxGroup("Abilities"), LabelText("Available")]
    private List<SOAbilityConfig> _availableAbilities;

    [SerializeField, ReadOnly] [BoxGroup("Abilities"), LabelText("Active Index")]
    private int _activeAbilityIndex;

    public SOAbilityConfig ActiveAbilityConfig => _availableAbilities[_activeAbilityIndex];

    private void OnEnable()
    {
        _switchAbilityEvent.Subscribe(OnAbilitySwitched);
    }

    private void OnDisable()
    {
        _switchAbilityEvent.Unsubscribe(OnAbilitySwitched);
    }

    private void OnAbilitySwitched()
    {
        _activeAbilityIndex += 1;
        if (_activeAbilityIndex >= _availableAbilities.Count)
            _activeAbilityIndex = 0;
    }
}