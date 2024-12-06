using Sirenix.OdinInspector;
using UnityEngine;

[ShowOdinSerializedPropertiesInInspector]
public class PlayerAbilityCastState : PlayerBaseState
{
    [SerializeField, Required] [BoxGroup("Tasks"), LabelText("Play Animation")]
    private PlayerAbilityPlayCastAnimationCommand _playAnimationCommand;

    [SerializeField, Required] [BoxGroup("Tasks"), LabelText("Fade Out Animation")]
    private PlayerAbilityFadeOutAnimationCommand _fadeOutAnimationCommand;

    private void OnEnable()
    {
        _playAnimationCommand.Execute(this);
    }

    private void OnDisable()
    {
        _fadeOutAnimationCommand.Execute(this);
    }
}