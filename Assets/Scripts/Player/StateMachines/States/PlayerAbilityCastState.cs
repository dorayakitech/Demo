using Sirenix.OdinInspector;
using UnityEngine;

[ShowOdinSerializedPropertiesInInspector]
public class PlayerAbilityCastState : PlayerBaseState
{
    [SerializeField, Required] [BoxGroup("Tasks"), LabelText("Play Animation")]
    private PlayerAbilityPlayCastAnimationCommand _playAnimationCommand;

    [SerializeField, Required] [BoxGroup("Tasks"), LabelText("Fade Out Animation")]
    private PlayerAbilityFadeOutAnimationCommand _fadeOutAnimationCommand;

    [SerializeField, Required] [BoxGroup("Tasks"), LabelText("Cast Ability")]
    private CastAbilityCommand _castAbilityCommand;

    private void OnEnable()
    {
        _playAnimationCommand.Execute(this);
        _castAbilityCommand.Execute(this);
    }

    private void OnDisable()
    {
        _fadeOutAnimationCommand.Execute(this);
    }
}